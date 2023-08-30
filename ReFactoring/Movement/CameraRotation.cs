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
        transform.position = target.position + new Vector3(Mathf.Sin(rot.x) * offSet.z, offSet.y, Mathf.Cos(rot.x) * offSet.z);
        transform.LookAt(target);
    }

    public void ReactNotify(_EInputType_ _type, _EInputDetailType_ _dType, Vector2 _value)
    {
        if (_type == _EInputType_.eitRotate)
        {
            if (_dType == _EInputDetailType_.eidtRotate)
                rot += _value * Time.deltaTime * MOUSE_SENSITIVITY;
        }
    }
}