using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadSkill : MonoBehaviour
{
    public float spreadSpeed { get; set; }

    void Update()
    {
        SkillSpread();
    }

    void SkillSpread()
    {
        transform.localScale += new Vector3(Time.deltaTime * spreadSpeed, Time.deltaTime * spreadSpeed, Time.deltaTime * spreadSpeed);
    }
}