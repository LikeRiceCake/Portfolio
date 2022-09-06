using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : Monster
{
    public CreateBoss cb;

    enum Status
    {
        Sleep, // Sleep
        Idle, // Idle 01
        ScreamToPlayer, // Scream
        Move, // Walk
        BasicAttack, // Basic Attack
        StrongAttack, // Claw Attack
        HeadAttack, // WalkBack -> Horn Attack
        Skill1, // Scream
        Die // Die
    }
    Status status;

    Animator anim;
    PlayerMove pm;

    AudioSource audios;

    int MaxHp;

    public bool isH_Attacking;
    public bool isRH_Attacking;
    public bool isS_Attacking;

    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
        Name = "사막의 지배자";
        MaxHp = 500;
        Hp = MaxHp;
        Damage = 100;
        Defence = 200;
        Speed = 1.5f;
        Post = "가";
        Exp = 5000;
        Money = 10000;
        isH_Attacking = false;
        isRH_Attacking = false;
        isS_Attacking = false;
        status = Status.Sleep;
        anim = GetComponent<Animator>();
        pm = cb.Playerobj.GetComponent<PlayerMove>();
    }

    protected override void Die()
    {
        base.Die();
        cb.Frame.gameObject.SetActive(true);
        cb.dieText.text = DieTextv;
    }

    private void Update()
    {
        SetStatus(status);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (status != Status.Die)
        {
            if (collision.collider.CompareTag("Sword") && pm.isAttack)
            {
                if(pm.Damage > Defence)
                {
                    Hp -= (pm.Damage - Defence);
                }
                if (Hp <= 0)
                {
                    pm.dataio.GetExp += Exp;
                    pm.dataio.GetMoney += Money;
                    pm.ExpImage.fillAmount = (float)pm.dataio.GetExp / (float)pm.MaxExp;
                    if (pm.MaxExp <= pm.dataio.GetExp)
                    {
                        pm.dataio.GetExp -= pm.MaxExp;
                        pm.dataio.GetLevel++;
                        pm.levelText.text = "Level : " + pm.dataio.GetLevel;
                        pm.ExpImage.fillAmount = (float)pm.dataio.GetExp / (float)pm.MaxExp;
                        pm.SetPlayerStat();
                    }
                    pm.dataio.Save();
                    status = Status.Die;
                    isH_Attacking = false;
                    isRH_Attacking = false;
                    isS_Attacking = false;
                    Die();
                    anim.SetTrigger("Die");
                    StartCoroutine(ToTown());
                }
            }
        }
    }

    void SetStatus(Status value)
    {
        switch (status)
        {
            case Status.Sleep: // 플레이어 발견(거리 내 접근)시에 한 번만 진행
                if (GetDistance() <= 5f)
                {
                    cb.audios.clip = cb.audioc;
                    cb.audios.Play();
                    audios.Play();
                    status = Status.ScreamToPlayer;
                    anim.SetTrigger("Scream");
                }
                break;
            case Status.ScreamToPlayer: // 플레이어 발견(거리 내 접근)시에 한 번만 진행
                break;
            case Status.Idle:
                transform.LookAt(cb.Playerobj.transform);
                if (GetDistance() <= 6f && Hp >= (MaxHp / 2))
                {
                    AttackSelect_HighHp();
                }
                else if (GetDistance() <= 6f && Hp < (MaxHp / 2))
                {
                    AttackSelect_LowHp();
                }
                if (GetDistance() > 6f)
                {
                    status = Status.Move;
                    anim.SetTrigger("Move");
                }
                break;
            case Status.Move:
                transform.LookAt(cb.Playerobj.transform);
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                if (GetDistance() <= 5f)
                {
                    status = Status.Idle;
                    anim.SetTrigger("Idle");
                }
                break;
            case Status.BasicAttack:
                break;
            case Status.StrongAttack:
                break;
            case Status.HeadAttack:
                break;
            case Status.Skill1:
                break;
            case Status.Die:
                break;
            default:
                break;
        }
    }

    float GetDistance()
    {
        return Vector3.Distance(transform.position, cb.Playerobj.transform.position);
    }

    void AttackSelect_HighHp()
    {
        int SelectSkill = Random.Range(1, 4);
        switch (SelectSkill)
        {
            case 1:
                status = Status.BasicAttack;
                anim.SetTrigger("BasicAttack");
                isH_Attacking = true;
                break;
            case 2:
                status = Status.StrongAttack;
                anim.SetTrigger("StrongAttack");
                isRH_Attacking = true;
                break;
            case 3:
                status = Status.HeadAttack;
                anim.SetTrigger("HeadAttack");
                break;
            default:
                break;
        }
    }

    void AttackSelect_LowHp()
    {
        int SelectSkill = Random.Range(1, 5);
        switch (SelectSkill)
        {
            case 1:
                status = Status.BasicAttack;
                anim.SetTrigger("BasicAttack");
                isH_Attacking = true;
                break;
            case 2:
                status = Status.StrongAttack;
                anim.SetTrigger("StrongAttack");
                isRH_Attacking = true;
                break;
            case 3:
                status = Status.HeadAttack;
                anim.SetTrigger("HeadAttack");
                break;
            case 4:
                status = Status.Skill1;
                anim.SetTrigger("Scream");
                isS_Attacking = true;
                break;
            default:
                break;
        }
    }

    public void SetIdle()
    {
        status = Status.Idle;
        isH_Attacking = false;
        isRH_Attacking = false;
        isS_Attacking = true;
    }

    public void HeadAttacking()
    {
        isH_Attacking = true;
    }

    public void ScreamSkill()
    {
        if(status == Status.Skill1)
        {
            audios.Play();
            if (GetDistance() < 10f)
            {
                if (Damage > (pm.Defence / 2))
                {
                    pm.Hp -= (Damage - (pm.Defence / 2));
                    pm.HpImage.fillAmount = (float)pm.Hp / (float)pm.MaxHp;
                    if (pm.Hp <= 0)
                    {
                        if (pm.dataio.GetLevel > 10)
                        {
                            if (pm.dataio.GetMoney - (pm.dataio.GetLevel * 3000) >= 0)
                            {
                                pm.dataio.GetMoney -= (pm.dataio.GetLevel * 3000);
                            }
                            else
                            {
                                pm.dataio.GetMoney = 0;
                            }
                            pm.dataio.GetExp = 0;
                        }
                        pm.ExpImage.fillAmount = (float)pm.dataio.GetExp / (float)pm.MaxExp;
                        pm.dataio.Save();
                        SceneManager.LoadScene("Town");
                    }
                }
                else if (Damage < (pm.Defence / 2))
                {
                    pm.Hp--;
                    pm.HpImage.fillAmount = (float)Hp / (float)MaxHp;
                    if (pm.Hp <= 0)
                    {
                        if (pm.dataio.GetLevel > 10)
                        {
                            if (pm.dataio.GetMoney - (pm.dataio.GetLevel * 3000) >= 0)
                            {
                                pm.dataio.GetMoney -= (pm.dataio.GetLevel * 3000);
                            }
                            else
                            {
                                pm.dataio.GetMoney = 0;
                            }
                            pm.dataio.GetExp = 0;
                        }
                        pm.ExpImage.fillAmount = (float)pm.dataio.GetExp / (float)pm.MaxExp;
                        pm.dataio.Save();
                        SceneManager.LoadScene("Town");
                    }
                }
            }
        }
    }

    IEnumerator ToTown()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Town");
    }

}
