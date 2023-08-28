using PublicEnums;
using System.Collections;
using UnityEngine;

enum _EGumihoSkill70_
{
    egsShot_WILL_O_THE_WISP_Nine,
    egsSwingTail,
    egsSpreadSphere,
    egsSealSkill,
    egsMax
}

public class GumihoPattern70 : MonsterPattern
{
    //삭제
    GameObject T_C;

    const int WILL_O_THE_WISP_INS_NUM = 9;

    readonly int[] GUMIHOSKILL_70_DAMAGES = { 30, 15, 30 };

    readonly float[] GUMIHOSKILL_70_DESTROY_TIMES = { 5f, 0, 5f, 5f };

    const float WILL_O_THE_WISP_INS_DELAY = 0.2f;
    const float WILL_O_THE_WISP_SPEED = 6f;

    const float SPHERE_SPEED = 5f;

    const string SHOT_NINE_WILL_O_THE_WISP_SKILL_ANIM_NAME = "Shot_A_T1";
    const string SWING_TAIL_ANIM_NAME = "Tail_T2";
    const string SPREAD_SPHERE_ANIM_NAME = "Howling_AT3";
    const string SEAL_SKILL_ANIM_NAME = "Seal_AT4";

    //삭제
    public void Start()
    {
        //T_C = GameObject.Find("TailCollider");
        //T_C.SetActive(false);
    }

    public override string SetRandomSkill()
    {
        int rand = Random.Range((int)_EGumihoSkill70_.egsShot_WILL_O_THE_WISP_Nine, (int)_EGumihoSkill70_.egsMax);

        switch (rand)
        {
            case (int)_EGumihoSkill70_.egsShot_WILL_O_THE_WISP_Nine:
                return Shot_Nine_WILL_O_THE_WISP();
            case (int)_EGumihoSkill70_.egsSwingTail:
                return SwingTail();
            case (int)_EGumihoSkill70_.egsSpreadSphere:
                return SpreadSphere();
            case (int)_EGumihoSkill70_.egsSealSkill:
                return SealPlayerSkill();
        }

        return null;
    }

    string Shot_Nine_WILL_O_THE_WISP()
    {
        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/Shot_WILL_O_THE_WISP_Monster");

        myAnim.SetTrigger(SHOT_NINE_WILL_O_THE_WISP_SKILL_ANIM_NAME);

        StartCoroutine(InsAndShot_WISP_Nine());

        return SHOT_NINE_WILL_O_THE_WISP_SKILL_ANIM_NAME;
    }

    IEnumerator InsAndShot_WISP_Nine()
    {
        GameObject[] WILL_O_THE_WISPS = new GameObject[WILL_O_THE_WISP_INS_NUM];
        for (int i = 0; i < WILL_O_THE_WISP_INS_NUM; i++)
        {
            WILL_O_THE_WISPS[i] = Instantiate(skillPrefab, transform.Find("ShotPositions").GetChild(i).transform.position, Quaternion.identity);
            WILL_O_THE_WISPS[i].GetComponent<MonsterAttackCollider>().m_damage = GUMIHOSKILL_70_DAMAGES[(int)_EGumihoSkill70_.egsShot_WILL_O_THE_WISP_Nine];
            WILL_O_THE_WISPS[i].GetComponent<MonsterAttackCollider>().m_destroyTime = GUMIHOSKILL_70_DESTROY_TIMES[(int)_EGumihoSkill70_.egsShot_WILL_O_THE_WISP_Nine];
            yield return new WaitForSeconds(WILL_O_THE_WISP_INS_DELAY);
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f);

        for (int i = 0; i < WILL_O_THE_WISP_INS_NUM; i++)
        {
            Transform target = null;

            Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.efstSight));

            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col.CompareTag("AttackedPos"))
                        target = col.transform;
                }
            }

            WILL_O_THE_WISPS[i].GetComponent<ForwardMoveObject>().target = target;
            WILL_O_THE_WISPS[i].GetComponent<ForwardMoveObject>().m_speed = WILL_O_THE_WISP_SPEED;
        }
    }

    string SwingTail()
    {
        myAnim.SetTrigger(SWING_TAIL_ANIM_NAME);

        return SWING_TAIL_ANIM_NAME;
    }

    string SpreadSphere()
    {
        myAnim.SetTrigger(SPREAD_SPHERE_ANIM_NAME);

        StartCoroutine(InstantiateSphere());

        return SPREAD_SPHERE_ANIM_NAME;
    }

    IEnumerator InstantiateSphere()
    {
        while (true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName(SPREAD_SPHERE_ANIM_NAME))
                break;

            yield return null;
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f);

        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/Sphere");

        GameObject obj = Instantiate(skillPrefab, transform.Find("foxShield").position, Quaternion.identity);
        obj.GetComponent<MonsterAttackCollider>().m_damage = GUMIHOSKILL_70_DAMAGES[(int)_EGumihoSkill70_.egsSpreadSphere];
        obj.GetComponent<MonsterAttackCollider>().m_destroyTime = GUMIHOSKILL_70_DESTROY_TIMES[(int)_EGumihoSkill70_.egsSpreadSphere];
        obj.GetComponent<SpreadSkill>().m_spreadSpeed = SPHERE_SPEED;
    }

    string SealPlayerSkill()
    {
        myAnim.SetTrigger(SEAL_SKILL_ANIM_NAME);

        //GameObject.Find("Player").GetComponent<PlayerControl>().isSealed = true;
        StartCoroutine(SealDelay());

        return SEAL_SKILL_ANIM_NAME;
    }

    IEnumerator SealDelay()
    {
        yield return new WaitForSeconds(GUMIHOSKILL_70_DESTROY_TIMES[(int)_EGumihoSkill70_.egsSealSkill]);
        //GameObject.Find("Player").GetComponent<PlayerControl>().isSealed = false;
    }

    public override _EMonsterPattern_ GetNowPattern()
    {
        return _EMonsterPattern_.empGumiho70;
    }
}