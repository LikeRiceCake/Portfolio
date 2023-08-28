using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Will_O_The_Wisp_Player : MonoBehaviour
{
    [SerializeField]
    float Speed;

    float _m_destroyTime;

    public float m_destroyTime {
        set
        {
            _m_destroyTime = value;
        }
    }

    void Start()
    {
        Speed = 5f;
        Destroy(gameObject, _m_destroyTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
}
