using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMoveObject : MonoBehaviour
{
    public float speed { get; set; }
    public Transform target { get; set; }

    void Start()
    {
        StartCoroutine(StandBy());
    }

    IEnumerator StandBy()
    {
        while(true)
        {
            if (target != null)
                break;

            yield return null;
        }

        transform.LookAt(target);

        while (true)
        {
            transform.position += transform.forward * Time.deltaTime * speed;

            yield return null;
        }
    }
}
