using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Will_O_The_Wisp_Player : MonoBehaviour
{
    [SerializeField]
    float Speed;

    float _destroyTime;

    public float destroyTime {
        set
        {
            _destroyTime = value;
        }
    }

    void Start()
    {
        Speed = 5f;
        Destroy(gameObject, _destroyTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
}
