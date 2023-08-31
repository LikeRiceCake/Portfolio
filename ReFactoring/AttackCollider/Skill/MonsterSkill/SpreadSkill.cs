using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadSkill : MonoBehaviour
{
    public float m_spreadSpeed { get; set; }

    void Update()
    {
        transform.localScale += new Vector3(Time.deltaTime * m_spreadSpeed, Time.deltaTime * m_spreadSpeed, Time.deltaTime * m_spreadSpeed);
    }
}