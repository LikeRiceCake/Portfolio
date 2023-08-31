using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMoveObject : MonoBehaviour
{
    public float m_speed { get; set; }

    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * m_speed;
    }
}
