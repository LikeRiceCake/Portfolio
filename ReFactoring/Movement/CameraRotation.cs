using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour, IInputMouseRotateObserver
{
    Transform target;

    [SerializeField]
    Vector3 offSet;

    Vector2 rot;

    const float MOUSE_SENSITIVITY = 2f;

    void Start()
    {
        GameObject.Find("Player").GetComponent<IInputMouseRotateSubject>().AddObserver(this);

        target = GameObject.Find("CamTarget").transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        transform.position = target.position + new Vector3(Mathf.Cos(-rot.x) * offSet.z, offSet.y, Mathf.Sin(-rot.x) * offSet.z);
        transform.LookAt(target);
    }

    public void ReactNotify(_EInputType_ type, _EInputDetailType_ dType, Vector2 value)
    {
        if (type == _EInputType_.eRotate)
        {
            if (dType == _EInputDetailType_.eRotate)
                rot += value * Time.deltaTime * MOUSE_SENSITIVITY;
        }
    }
}
