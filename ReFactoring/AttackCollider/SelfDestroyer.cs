using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float m_destroyTime {get; set;}

    void Start()
    {
        Destroy(gameObject, m_destroyTime);
    }
}
