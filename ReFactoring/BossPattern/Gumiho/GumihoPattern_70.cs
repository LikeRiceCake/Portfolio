using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum _EGumihoSkill_70_
{
    eShot_WILL_O_THE_WISP_Nine,
    eTailAttack,
    eSpreadSphere,
    eHowling_SkillSeal,
    eMax
}

public class GumihoPattern_70 : MonsterPattern
{
    GameObject T_C;

    const int WILL_O_THE_WISP_CNT = 9;

    readonly int[] GUMIHOSKILL_70_DAMAGE = { 30, 15, 30 };

    readonly float[] GUMIHOSKILL_70_DESTROYTIME = { 5f, 0, 5f, 5f };

    const float WILL_O_THE_WISP_INS_DELAY = 0.2f;
    const float WILL_O_THE_WISP_SPEED = 6f;

    const float SPHERE_SPEED = 5f;

    const string SHOT_NINE_WILL_O_THE_WISP_SKILL = "Shot_A_T1";
    const string TAIL_ATTACK = "Tail_T2";
    const string SPREAD_SPHERE = "Howling_AT3";
    const string SEAL_SKILL = "Seal_AT4";

    public void Start()
    {
        //T_C = GameObject.Find("TailCollider");
        //T_C.SetActive(false);
    }

    public override string SetRandomSkill()
    {
        int rand = Random.Range((int)_EGumihoSkill_70_.eShot_WILL_O_THE_WISP_Nine, (int)_EGumihoSkill_70_.eMax);

        switch (rand)
        {
            case (int)_EGumihoSkill_70_.eShot_WILL_O_THE_WISP_Nine:
                return Shot_Nine_WILL_O_THE_WISP();
            case (int)_EGumihoSkill_70_.eTailAttack:
                return TailAttack();
            case (int)_EGumihoSkill_70_.eSpreadSphere:
                return SpreadSphere();
            case (int)_EGumihoSkill_70_.eHowling_SkillSeal:
                return Howling_SkillSeal();
        }

        return null;
    }

    string Shot_Nine_WILL_O_THE_WISP()
    {
        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/Shot_WILL_O_THE_WISP_Monster");

        myAnim.SetTrigger(SHOT_NINE_WILL_O_THE_WISP_SKILL);

        StartCoroutine(Shot_WISP_Nine());

        return SHOT_NINE_WILL_O_THE_WISP_SKILL;
    }

    IEnumerator Shot_WISP_Nine()
    {
        GameObject[] WILL_O_THE_WISPs = new GameObject[WILL_O_THE_WISP_CNT];
        for (int i = 0; i < WILL_O_THE_WISP_CNT; i++)
        {
            WILL_O_THE_WISPs[i] = Instantiate(skillPrefab, transform.Find("ShotPositions").GetChild(i).transform.position, Quaternion.identity);
            WILL_O_THE_WISPs[i].GetComponent<MonsterAttackCollider>().damage = GUMIHOSKILL_70_DAMAGE[(int)_EGumihoSkill_70_.eShot_WILL_O_THE_WISP_Nine];
            WILL_O_THE_WISPs[i].GetComponent<MonsterAttackCollider>().destroyTime = GUMIHOSKILL_70_DESTROYTIME[(int)_EGumihoSkill_70_.eShot_WILL_O_THE_WISP_Nine];
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

    string TailAttack()
    {
        myAnim.SetTrigger(TAIL_ATTACK);

        return TAIL_ATTACK;
    }

    string SpreadSphere()
    {
        myAnim.SetTrigger(SPREAD_SPHERE);

        StartCoroutine(InstantiateSphere());

        return SPREAD_SPHERE;
    }

    IEnumerator InstantiateSphere()
    {
        while (true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName(SPREAD_SPHERE))
                break;

            yield return null;
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f);

        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/Sphere");

        GameObject obj = Instantiate(skillPrefab, transform.Find("foxShield").position, Quaternion.identity);
        obj.GetComponent<MonsterAttackCollider>().damage = GUMIHOSKILL_70_DAMAGE[(int)_EGumihoSkill_70_.eSpreadSphere];
        obj.GetComponent<MonsterAttackCollider>().destroyTime = GUMIHOSKILL_70_DESTROYTIME[(int)_EGumihoSkill_70_.eSpreadSphere];
        obj.GetComponent<SpreadSkill>().spreadSpeed = SPHERE_SPEED;
    }

    string Howling_SkillSeal()
    {
        myAnim.SetTrigger(SEAL_SKILL);

        //GameObject.Find("Player").GetComponent<PlayerControl>().isSealed = true;
        StartCoroutine(SealDelay());

        return SEAL_SKILL;
    }

    IEnumerator SealDelay()
    {
        yield return new WaitForSeconds(GUMIHOSKILL_70_DESTROYTIME[(int)_EGumihoSkill_70_.eHowling_SkillSeal]);
        //GameObject.Find("Player").GetComponent<PlayerControl>().isSealed = false;
    }

    public override _EMonsterPattern_ GetNowPattern()
    {
        return _EMonsterPattern_.eGumiho_70;
    }
}