using PublicEnums;
using PublicEnums.Monster.DetailType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum _EGumihoSkill_100_
{
    eShot_WILL_O_THE_WISP_Three,
    eClawAttack_BackStep,
    eBlowUp_WILL_O_THE_WISP_PlayerPlace,
    eMax
}

public class GumihoPattern_100 : MonsterPattern
{
    GameObject G_C;

    const int WILL_O_THE_WISP_CNT = 3;

    readonly int[] GUMIHOSKILL_100_DAMAGE = { 20, 5, 30 };

    readonly float[] GUMIHOSKILL_100_DESTROYTIME = { 5f, 0.1f, 3f };

    const float WILL_O_THE_WISP_INS_DELAY = 0.7f;
    const float WILL_O_THE_WISP_SPEED = 5f;

    const float CLAW_CHASE_SPEED = 2.5f;
    const float CLAW_BACKSTEP_SPEED = 5f;

    const float BLOWUP_WILL_O_THE_WISP_INS_TIME = 1.5f;

    const string SHOT_THREE_WILL_O_THE_WISP_SKILL = "Shot_AT1";
    const string CLAWATTACK_BACKSTEP = "Claw_AT2";
    const string BLOWUP_WILL_O_THE_WISP_PLAYERPLACE = "BlowUp_AT3";

    public void Start()
    {
        G_C = GameObject.Find("AT2");
        G_C.SetActive(false);
    }

    public override string SetRandomSkill()
    {
        int rand = Random.Range((int)_EGumihoSkill_100_.eShot_WILL_O_THE_WISP_Three, (int)_EGumihoSkill_100_.eMax);

        switch(rand)
        {
            case (int)_EGumihoSkill_100_.eShot_WILL_O_THE_WISP_Three:
                return Shot_Three_WILL_O_THE_WISP();
            case (int)_EGumihoSkill_100_.eClawAttack_BackStep:
                return ClawAttack_BackStep();
            case (int)_EGumihoSkill_100_.eBlowUp_WILL_O_THE_WISP_PlayerPlace:
                return BlowUp_WILL_O_THE_WISP_PlayerPlace();
        }

        return null;
    }

    string Shot_Three_WILL_O_THE_WISP()
    {
        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/Shot_WILL_O_THE_WISP_Monster");

        myAnim.SetTrigger(SHOT_THREE_WILL_O_THE_WISP_SKILL);

        StartCoroutine(Shot_WISP_Three());

        return SHOT_THREE_WILL_O_THE_WISP_SKILL;
    }

    IEnumerator Shot_WISP_Three()
    {
        GameObject[] WILL_O_THE_WISPs = new GameObject[WILL_O_THE_WISP_CNT];
        for (int i = 0; i < WILL_O_THE_WISP_CNT; i++)
        {
            WILL_O_THE_WISPs[i] = Instantiate(skillPrefab, transform.Find("ShotPositions").GetChild(i).transform.position, Quaternion.identity);
            WILL_O_THE_WISPs[i].GetComponent<MonsterAttackCollider>().damage = GUMIHOSKILL_100_DAMAGE[(int)_EGumihoSkill_100_.eShot_WILL_O_THE_WISP_Three];
            WILL_O_THE_WISPs[i].GetComponent<MonsterAttackCollider>().destroyTime = GUMIHOSKILL_100_DESTROYTIME[(int)_EGumihoSkill_100_.eShot_WILL_O_THE_WISP_Three];
            yield return new WaitForSeconds(WILL_O_THE_WISP_INS_DELAY);
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f);

        for (int i = 0; i < WILL_O_THE_WISP_CNT; i++)
        {
            Transform target = null;

            Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.eSight));

            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col.CompareTag("AttackedPos"))
                        target = col.transform;
                }
            }

            WILL_O_THE_WISPs[i].GetComponent<ForwardMoveObject>().target = target;
            WILL_O_THE_WISPs[i].GetComponent<ForwardMoveObject>().speed = WILL_O_THE_WISP_SPEED;
        }
    }

    string ClawAttack_BackStep()
    {
        StartCoroutine(ChaseTarget());

        return CLAWATTACK_BACKSTEP;
    }

    IEnumerator ChaseTarget()
    {
        Transform target = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.eSight));

        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col.CompareTag("Player"))
                    target = col.transform;
            }
        }

        while (true)
        {
            transform.LookAt(target);

            if (Vector3.Distance(target.position, transform.position) <= myStat.GetFloatStat(_EFloatStatType_.eAttackRange))
                break;

            transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));

            transform.position += transform.forward * myStat.GetFloatStat(_EFloatStatType_.eSpeed) * Time.deltaTime * CLAW_CHASE_SPEED;

            yield return null;
        }

        myAnim.SetTrigger(CLAWATTACK_BACKSTEP);

        StartCoroutine(BackMoving());
    }

    IEnumerator BackMoving()
    {
        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f);

        while (true)
        {
            if (!myAnim.GetCurrentAnimatorStateInfo(0).IsName(CLAWATTACK_BACKSTEP))
                break;

            transform.position += -transform.forward * Time.deltaTime * CLAW_BACKSTEP_SPEED;

            yield return null;
        }
    }

    string BlowUp_WILL_O_THE_WISP_PlayerPlace()
    {
        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/BlowUp_WILL_O_THE_WISP");

        myAnim.SetTrigger(BLOWUP_WILL_O_THE_WISP_PLAYERPLACE);

        StartCoroutine(BlouUp_WISP());

        return BLOWUP_WILL_O_THE_WISP_PLAYERPLACE;
    }

    IEnumerator BlouUp_WISP()
    {
        while(true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName(BLOWUP_WILL_O_THE_WISP_PLAYERPLACE))
                break;

            yield return null;
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f);

        Transform target = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.eSight));

        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col.CompareTag("Player"))
                    target = col.transform;
            }
        }

        GameObject obj = Instantiate(skillPrefab, target.position, Quaternion.identity);
        obj.GetComponent<InstantiateObjectSkill>().damage = GUMIHOSKILL_100_DAMAGE[(int)_EGumihoSkill_100_.eBlowUp_WILL_O_THE_WISP_PlayerPlace];
        obj.GetComponent<InstantiateObjectSkill>().destroyTime = GUMIHOSKILL_100_DESTROYTIME[(int)_EGumihoSkill_100_.eBlowUp_WILL_O_THE_WISP_PlayerPlace];
        obj.GetComponent<InstantiateObjectSkill>().insTime = BLOWUP_WILL_O_THE_WISP_INS_TIME;
    }

    public override _EMonsterPattern_ GetNowPattern()
    {
        return _EMonsterPattern_.eGumiho_100;
    }

    public void skillstart()
    {
        G_C.SetActive(true);
    }

    public void endskill()
    {
        G_C.SetActive(false);
    }
}
