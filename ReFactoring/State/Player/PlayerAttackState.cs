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

    public override void DoAction(_EStateType_ _type)
    {
        base.DoAction(_type);

        StartCoroutine(InputAttack());
        StartCoroutine(InputDash());
    }

    protected override void SetAttack(_EInputType_ _type, _EInputDetailType_ _dType)
    {
        if (_type == _EInputType_.eitAttack)
        {
            switch (_dType)
            {
                case _EInputDetailType_.eidtAttack_W:
                    inputQueue.Enqueue(new Weak(myAnim, weapon, myStat));
                    break;
                case _EInputDetailType_.eidtAttack_S:
                    inputQueue.Enqueue(new Strong(myAnim, weapon, myStat));
                    break;
            }

            if (excuteStack.Count == 0)
            {
                if (inputQueue.Peek().GetAttackType() == _EPlayerAttackType_.epatWeak)
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
                if (inputQueue.Peek().GetAttackType() == _EPlayerAttackType_.epatWeak)
                {
                    if (excuteStack.Count() >= 3
                        || excuteStack.Peek().GetAttackType() == _EPlayerAttackType_.epatStrong)
                        stateManager.SetActionType(_EStateType_.estIdle, _EObjectType_.eotPlayer);
                }
                else if (inputQueue.Peek().GetAttackType() == _EPlayerAttackType_.epatStrong)
                {
                    if (excuteStack.Peek().GetAttackType() == _EPlayerAttackType_.epatStrong)
                        stateManager.SetActionType(_EStateType_.estIdle, _EObjectType_.eotPlayer);
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
        stateManager.SetActionType(_EStateType_.estIdle, _EObjectType_.eotPlayer);
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