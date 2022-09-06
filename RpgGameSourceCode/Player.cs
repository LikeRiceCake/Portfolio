using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : PlayerStat
{
    enum Status { idle, move, attack, attack2, die }
    Status status;

    public DataIO dataio;

    public Boss bs;

    public Image joyImage;
    private Vector3 startPos;
    float stickDistance = 0;

    AudioSource audios;
    public AudioClip[] audiocs;

    Vector3 dir;
    float MoveDistance;

    //Vector3 offset;

    bool isDrag;
    public bool isAttack;

    public GameObject cam;

    Animator anim;

    public Image HpImage;
    public Image ExpImage;

    public Text levelText;

    void Start()
    {
        audios = GetComponent<AudioSource>();
        SetPlayerStat();
        //offset = cam.transform.position - transform.position;
        anim = GetComponent<Animator>();
        startPos = joyImage.transform.position;
        stickDistance = joyImage.rectTransform.sizeDelta.x / 1.2f;
        status = Status.idle;
    }

    private void FixedUpdate()
    {
        Setstatus(status);
    }

    private void LateUpdate()
    {
        //CameraMove();
    }

    private void OnTriggerEnter(Collider Attacked)
    {
        if (Attacked.transform.CompareTag("M_Head") && bs.isH_Attacking)
        {
            if (bs.Damage > (Defence / 2))
            {
                Hp -= (bs.Damage - (Defence / 2));
                HpImage.fillAmount = (float)Hp / (float)MaxHp;
                if (Hp <= 0)
                {
                    if (dataio.GetLevel > 10)
                    {
                        if (dataio.GetMoney - (dataio.GetLevel * 3000) >= 0)
                        {
                            dataio.GetMoney -= (dataio.GetLevel * 3000);
                        }
                        else
                        {
                            dataio.GetMoney = 0;
                        }
                        dataio.GetExp = 0;
                    }
                    ExpImage.fillAmount = (float)dataio.GetExp / (float)MaxExp;
                    dataio.Save();
                    SceneManager.LoadScene("Town");
                }
            }
            else if(bs.Damage < (Defence / 2))
            {
                Hp--;
                HpImage.fillAmount = (float)Hp / (float)MaxHp;
                if (Hp <= 0)
                {
                    if (dataio.GetLevel > 10)
                    {
                        if (dataio.GetMoney - (dataio.GetLevel * 3000) >= 0)
                        {
                            dataio.GetMoney -= (dataio.GetLevel * 3000);
                        }
                        else
                        {
                            dataio.GetMoney = 0;
                        }
                        dataio.GetExp = 0;
                    }
                    ExpImage.fillAmount = (float)dataio.GetExp / (float)MaxExp;
                    dataio.Save();
                    SceneManager.LoadScene("Town");
                }
            }
        }
        else if (Attacked.transform.CompareTag("M_RightHand") && bs.isRH_Attacking)
        {
            if (bs.Damage > (Defence / 2))
            {
                Hp -= (bs.Damage - (Defence / 2));
                HpImage.fillAmount = (float)Hp / (float)MaxHp;
                if (Hp <= 0)
                {
                    if(dataio.GetLevel > 10)
                    {
                        if (dataio.GetMoney - (dataio.GetLevel * 3000) >= 0)
                        {
                            dataio.GetMoney -= (dataio.GetLevel * 3000);
                        }
                        else
                        {
                            dataio.GetMoney = 0;
                        }
                        dataio.GetExp = 0;
                    }
                    ExpImage.fillAmount = (float)dataio.GetExp / (float)MaxExp;
                    dataio.Save();
                    SceneManager.LoadScene("Town");
                }
            }
            else if (bs.Damage < (Defence / 2))
            {
                Hp--;
                HpImage.fillAmount = (float)Hp / (float)MaxHp;
                if (Hp <= 0)
                {
                    if (dataio.GetLevel > 10)
                    {
                        if (dataio.GetMoney - (dataio.GetLevel * 3000) >= 0)
                        {
                            dataio.GetMoney -= (dataio.GetLevel * 3000);
                        }
                        else
                        {
                            dataio.GetMoney = 0;
                        }
                        dataio.GetExp = 0;
                    }
                    ExpImage.fillAmount = (float)dataio.GetExp / (float)MaxExp;
                    dataio.Save();
                    SceneManager.LoadScene("Town");
                }
            }
        }
    }

    public void DragOn()
    {
        if (joyImage == null)
        {
            return;
        }

        isDrag = true;

        Vector3 MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        dir = (MousePos - startPos).normalized;

        MoveDistance = Vector3.Distance(startPos, MousePos);

        if (MoveDistance > stickDistance)
        {
            MoveDistance = stickDistance;
            joyImage.rectTransform.position = startPos + (dir * stickDistance);
        }
        else
        {
            joyImage.transform.position = MousePos;
        }

        transform.Rotate(new Vector3(0f, (Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg)/* - 90f*/, 0f) * Time.deltaTime);
    }

    public void EndDrag()
    {
        if (joyImage != null)
        {
            joyImage.rectTransform.position = startPos;
            isDrag = false;
        }
    }

    public void DragRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, mouseX, 0));
    }

    void Setstatus(Status value)
    {
        status = value;

        switch (status)
        {
            case Status.idle:
                if (isDrag == true)
                {
                    anim.SetTrigger("Move");
                    status = Status.move;
                }
                break;
            case Status.move:
                transform.Translate(Vector3.forward * (MoveDistance / 30f) * Time.deltaTime);
                if (isDrag == false)
                {
                    anim.SetTrigger("Idle");
                    status = Status.idle;
                }
                break;
            case Status.attack:
                break;
            case Status.attack2:
                break;
        }
    }

    public void AttackButton()
    {
        if (!isAttack)
        {
            isAttack = true;
            anim.SetTrigger("Attack");
            status = Status.attack;
        }
        else if (isAttack)
        {
            isAttack = false;
            anim.SetTrigger("Attack2");
            status = Status.attack2;
        }
    }

    public void AttackSound1()
    {
        audios.clip = audiocs[0];
        audios.PlayOneShot(audios.clip);
    }

    public void AttackSound2()
    {
        audios.clip = audiocs[1];
        audios.PlayOneShot(audios.clip);
    }

    public void AttackEnd()
    {
        if (isAttack)
        {
            anim.SetTrigger("Idle");
            status = Status.idle;
            isAttack = false;
        }
        anim.SetBool("Attack", false);
    }

    public void AttackEnd2()
    {
        status = Status.idle;
    }

    //void CameraMove()
    //{
    //    cam.transform.localPosition = transform.localPosition + offset;
    //    cam.transform.rotation = transform.localRotation;
    //}

    public void SetPlayerStat()
    {
        dataio.Load();
        SetMaxHp();
        Hp = MaxHp;
        SetDamage();
        SetDefence();
        MaxExp = 100 + (dataio.GetLevel * 10);
        if (dataio.GetExp != 0)
        {
            ExpImage.fillAmount = (float)dataio.GetExp / (float)MaxExp;
        }
        else if (dataio.GetExp == 0)
        {
            ExpImage.fillAmount = 0;
        }
        levelText.text = "Level : " + dataio.GetLevel;
    }

    void SetMaxHp()
    {
        MaxHp = 20 + (dataio.GetLevel * 3) + (dataio.GetStat * 2);
    }

    void SetDamage()
    {
        if (dataio.GetWeapon != null)
        {
            Damage = 5 + (int)(dataio.GetLevel * 1.3) + (dataio.GetWeapon.Damage);
        }
        else if (dataio.GetWeapon == null)
        {
            Damage = 5 + (int)(dataio.GetLevel * 1.3);
        }
    }

    void SetDefence()
    {
        Defence = 3 + (int)(dataio.GetLevel * 1.1);
        SetHead();
        SetBody();
        SetLeg();
    }

    void SetHead()
    {
        if (dataio.GetHeadArmor != null)
        {
            Defence += (dataio.GetHeadArmor.Defence);
        }
    }

    void SetBody()
    {
        if (dataio.GetBodyArmor != null)
        {
            Defence += (dataio.GetBodyArmor.Defence);
        }
    }

    void SetLeg()
    {
        if (dataio.GetLegArmor != null)
        {
            Defence += (dataio.GetLegArmor.Defence);
        }
    }
}