using PublicEnums;
using PublicEnums.Monster.DetailType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum _EGumihoSkill100_
{
    egsShot_WILL_O_THE_WISP_Three,
    egsSwingClaw,
    egsBlowUp_WILL_O_THE_WISP_PlayerPlace,
    egsMax
}

public class GumihoPattern100 : MonsterPattern
{
    GameObject G_C;

    const int WILL_O_THE_WISP_INS_NUM = 3;

    readonly int[] GUMIHOSKILL_100_DAMAGES = { 20, 5, 30 };

    readonly float[] GUMIHOSKILL_100_DESTROY_TIMES = { 5f, 0.1f, 3f };

    const float WILL_O_THE_WISP_INS_DELAY = 0.7f;
    const float WILL_O_THE_WISP_SPEED = 5f;

    const float CLAW_CHASE_SPEED = 2.5f;
    const float CLAW_BACKSTEP_SPEED = 5f;

    const float BLOWUP_WILL_O_THE_WISP_INS_TIME = 1.5f;

    const string SHOT_THREE_WILL_O_THE_WISP_SKILL_ANIM_NAME = "Shot_AT1";
    const string SWINGCLAW_BACKSTEP_ANIM_NAME = "Claw_AT2";
    const string BLOWUP_WILL_O_THE_WISP_PLAYERPLACE_ANIM_NAME = "BlowUp_AT3";

    public void Start()
    {
        G_C = GameObject.Find("AT2");
        G_C.SetActive(false);
    }

    public override string SetRandomSkill()
    {
        int rand = Random.Range((int)_EGumihoSkill100_.egsShot_WILL_O_THE_WISP_Three, (int)_EGumihoSkill100_.egsMax);

        switch(rand)
        {
            case (int)_EGumihoSkill100_.egsShot_WILL_O_THE_WISP_Three:
                return Shot_Three_WILL_O_THE_WISP();
            case (int)_EGumihoSkill100_.egsSwingClaw:
                return SwingClaw();
            case (int)_EGumihoSkill100_.egsBlowUp_WILL_O_THE_WISP_PlayerPlace:
                return BlowUp_WILL_O_THE_WISP_PlayerPlace();
        }

        return null;
    }

    string Shot_Three_WILL_O_THE_WISP()
    {
        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/Shot_WILL_O_THE_WISP_Monster");

        myAnim.SetTrigger(SHOT_THREE_WILL_O_THE_WISP_SKILL_ANIM_NAME);

        StartCoroutine(InsAndShot_WISP_Three());

        return SHOT_THREE_WILL_O_THE_WISP_SKILL_ANIM_NAME;
    }

    IEnumerator InsAndShot_WISP_Three()
    {
        GameObject[] WILL_O_THE_WISPS = new GameObject[WILL_O_THE_WISP_INS_NUM];
        for (int i = 0; i < WILL_O_THE_WISP_INS_NUM; i++)
        {
            WILL_O_THE_WISPS[i] = Instantiate(skillPrefab, transform.Find("ShotPositions").GetChild(i).transform.position, Quaternion.identity);
            WILL_O_THE_WISPS[i].GetComponent<MonsterAttackCollider>().m_damage = GUMIHOSKILL_100_DAMAGES[(int)_EGumihoSkill100_.egsShot_WILL_O_THE_WISP_Three];
            WILL_O_THE_WISPS[i].GetComponent<SelfDestroyer>().m_destroyTime = GUMIHOSKILL_100_DESTROY_TIMES[(int)_EGumihoSkill100_.egsShot_WILL_O_THE_WISP_Three];
            yield return new WaitForSeconds(WILL_O_THE_WISP_INS_DELAY);
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f);

        for (int i = 0; i < WILL_O_THE_WISP_INS_NUM; i++)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.efstSight));

            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col.CompareTag("AttackedPos"))
                        WILL_O_THE_WISPS[i].transform.LookAt(col.transform.position - transform.position);
                }
            }

            WILL_O_THE_WISPS[i].GetComponent<ForwardMoveObject>().m_speed = WILL_O_THE_WISP_SPEED;
        }
    }

    string SwingClaw()
    {
        StartCoroutine(ChaseTarget());

        return SWINGCLAW_BACKSTEP_ANIM_NAME;
    }

    IEnumerator ChaseTarget()
    {
        Transform target = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.efstSight));

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

            if (Vector3.Distance(target.position, transform.position) <= myStat.GetFloatStat(_EFloatStatType_.efstAttackRange))
                break;

            transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));

            transform.position += transform.forward * myStat.GetFloatStat(_EFloatStatType_.efstSpeed) * Time.deltaTime * CLAW_CHASE_SPEED;

            yield return null;
        }

        myAnim.SetTrigger(SWINGCLAW_BACKSTEP_ANIM_NAME);

        StartCoroutine(MoveBack());
    }

    IEnumerator MoveBack()
    {
        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f);

        while (true)
        {
            if (!myAnim.GetCurrentAnimatorStateInfo(0).IsName(SWINGCLAW_BACKSTEP_ANIM_NAME))
                break;

            transform.position += -transform.forward * Time.deltaTime * CLAW_BACKSTEP_SPEED;

            yield return null;
        }
    }

    string BlowUp_WILL_O_THE_WISP_PlayerPlace()
    {
        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/BlowUp_WILL_O_THE_WISP");

        myAnim.SetTrigger(BLOWUP_WILL_O_THE_WISP_PLAYERPLACE_ANIM_NAME);

        StartCoroutine(InsBlouUp_WISP());

        return BLOWUP_WILL_O_THE_WISP_PLAYERPLACE_ANIM_NAME;
    }

    IEnumerator InsBlouUp_WISP()
    {
        while(true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName(BLOWUP_WILL_O_THE_WISP_PLAYERPLACE_ANIM_NAME))
                break;

            yield return null;
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f);

        Transform target = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.efstSight));

        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col.CompareTag("Player"))
                    target = col.transform;
            }
        }

        GameObject obj = Instantiate(skillPrefab, target.position, Quaternion.identity);
        obj.GetComponent<InstantiateObjectSkill>().m_damage = GUMIHOSKILL_100_DAMAGES[(int)_EGumihoSkill100_.egsBlowUp_WILL_O_THE_WISP_PlayerPlace];
        obj.GetComponent<InstantiateObjectSkill>().m_destroyTime = GUMIHOSKILL_100_DESTROY_TIMES[(int)_EGumihoSkill100_.egsBlowUp_WILL_O_THE_WISP_PlayerPlace];
        obj.GetComponent<InstantiateObjectSkill>().m_insTime = BLOWUP_WILL_O_THE_WISP_INS_TIME;
        obj.GetComponent<SelfDestroyer>().m_destroyTime = BLOWUP_WILL_O_THE_WISP_INS_TIME;
    }

    public override _EMonsterPattern_ GetNowPattern()
    {
        return _EMonsterPattern_.empGumiho100;
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
