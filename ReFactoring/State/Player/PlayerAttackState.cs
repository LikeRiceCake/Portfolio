using FIMSpace.Basics;
using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    Queue<AttackType> inputQueue = new Queue<AttackType>();
    Stack<AttackType> excuteStack = new Stack<AttackType>();

    PlayerAttackCollider weapon;

    protected override void Awake()
    {
        base.Awake();

        weapon = GetComponentInChildren<PlayerAttackCollider>();
    }

    public override void DoAction(_EStateType_ state)
    {
        base.DoAction(state);

        StartCoroutine(InputAttack());
        StartCoroutine(InputDash());
    }

    protected override void SetAttack(_EInputType_ type, _EInputDetailType_ dType)
    {
        if (type == _EInputType_.eAttack)
        {
            switch (dType)
            {
                case _EInputDetailType_.eAttack_W:
                    inputQueue.Enqueue(new Weak(myAnim, weapon, myStat));
                    break;
                case _EInputDetailType_.eAttack_S:
                    inputQueue.Enqueue(new Strong(myAnim, weapon, myStat));
                    break;
            }

            if (excuteStack.Count == 0)
            {
                if (inputQueue.Peek().GetAttackType() == _EPlayerAttackType_.eWeak)
                {
                    excuteStack.Push(inputQueue.Dequeue());
                    excuteStack.Peek().Attack();
                    StartCoroutine(excuteStack.Peek().SettingDamage());
                    StartCoroutine(ExcuteAttack());
                }
                else
                    inputQueue.Dequeue();
            }
        }
    }

    IEnumerator ExcuteAttack()
    {
        while(true)
        {
            yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);

            if (inputQueue.Count != 0)
            {
                if (inputQueue.Peek().GetAttackType() == _EPlayerAttackType_.eWeak)
                {
                    if (excuteStack.Count() >= 3
                        || excuteStack.Peek().GetAttackType() == _EPlayerAttackType_.eStrong)
                        stateManager.SetActionType(_EStateType_.eIdle, _EObjectType_.ePlayer);
                }
                else if (inputQueue.Peek().GetAttackType() == _EPlayerAttackType_.eStrong)
                {
                    if (excuteStack.Peek().GetAttackType() == _EPlayerAttackType_.eStrong)
                        stateManager.SetActionType(_EStateType_.eIdle, _EObjectType_.ePlayer);
                }

                excuteStack.Push(inputQueue.Dequeue());
                excuteStack.Peek().Attack();

                yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9);

                yield return new WaitUntil(() => !myAnim.IsInTransition(0));

                StartCoroutine(excuteStack.Peek().SettingDamage());
            }
            else
            {
                yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9);

                yield return new WaitUntil(() => !myAnim.IsInTransition(0));

                break;
            }
                
        }
        stateManager.SetActionType(_EStateType_.eIdle, _EObjectType_.ePlayer);
    }

    public override void ExitState()
    {
        ResetComobo();
        myAnim.ResetTrigger("Weak");
        myAnim.ResetTrigger("Strong");
    }

    void ResetComobo()
    {
        inputQueue.Clear();
        excuteStack.Clear();
    }
}