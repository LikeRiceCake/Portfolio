using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    protected string Name;
    protected int Hp;
    public int Damage;
    protected int Defence;
    protected float Speed;
    protected string DieTextv;
    protected string Post;
    protected int Exp;
    protected int Money;

    protected virtual void Die()
    {
        DieTextv = Name + Post+ " 쓰러졌다!";
    }

    protected virtual void BossDie()
    {
        DieTextv = "보스몬스터" + Name + Post + "쓰러졌다!";
    }
}
