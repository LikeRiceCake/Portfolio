using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEngine.Rendering.DebugUI.Table;

public abstract class PlayerState : State
{
    protected GameManager gameMgr;

    Camera mainCam;

    CharacterController controller;

    Vector3 playerDir;
    Vector3[] playerDirs;
    Vector3 playerRot;

    const float GRAVITY = 0.918f;

    Vector3 vecGravity = new Vector3(0, GRAVITY, 0);

    float m_currentSpeed;

    float m_x;
    float m_y;

    Vector2 cameraRot;

    protected override void Awake()
    {
        base.Awake();

        gameMgr = GameObject.Find("Manager").GetComponent<GameManager>();

        mainCam = Camera.main;

        playerDirs = new Vector3[4];

        controller = GetComponent<CharacterController>();

        m_currentSpeed = myStat.GetFloatStat(_EFloatStatType_.efstSpeed);
    }

    public override void DoAction(_EStateType_ _sttype)
    {
        StartCoroutine(InputOption());
        StartCoroutine(InputLine());
        StartCoroutine(RotatePlayer());
        StartCoroutine(InputMouseRotate());

        StartCoroutine(ApplyGravity());
    }

    IEnumerator ApplyGravity()
    {
        while (true)
        {
            controller.Move(-vecGravity);

            yield return null;
        }
    }

    protected IEnumerator InputOption()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ((PlayerStateManager)stateManager).NotifyKeyClickInput(_EInputType_.eitOption, _EInputDetailType_.eidtOption);

            yield return null;
        }
    }

    protected IEnumerator InputLine()
    {
        while (true)
        {
            if (gameMgr.GetGameState(_EGameStateType_.egstIsLine) && Input.GetKeyDown(KeyCode.F))
                ((PlayerStateManager)stateManager).NotifyKeyClickInput(_EInputType_.eitDialogue, _EInputDetailType_.eidtDialogue);

            yield return null;
        }
    }

    protected IEnumerator InputMoveOn()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.W))
                    MovingSet(_EInputType_.eitMove, _EInputDetailType_.eidtDown_W);
                if (Input.GetKey(KeyCode.S))
                    MovingSet(_EInputType_.eitMove, _EInputDetailType_.eidtDown_S);
                if (Input.GetKey(KeyCode.A))
                    MovingSet(_EInputType_.eitMove, _EInputDetailType_.eidtDown_A);
                if (Input.GetKey(KeyCode.D))
                    MovingSet(_EInputType_.eitMove, _EInputDetailType_.eidtDown_D);

                if (stateManager.currentState != _EStateType_.estMove)
                    stateManager.SetActionType(_EStateType_.estMove, _EObjectType_.eotPlayer);
            }

            yield return null;
        }
    }

    protected IEnumerator InputMoveOff()
    {
        while (true)
        {
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                if (Input.GetKeyUp(KeyCode.W))
                    MovingSet(_EInputType_.eitMove, _EInputDetailType_.eidtUp_W);
                if (Input.GetKeyUp(KeyCode.A))
                    MovingSet(_EInputType_.eitMove, _EInputDetailType_.eidtUp_A);
                if (Input.GetKeyUp(KeyCode.S))
                    MovingSet(_EInputType_.eitMove, _EInputDetailType_.eidtUp_S);
                if (Input.GetKeyUp(KeyCode.D))
                    MovingSet(_EInputType_.eitMove, _EInputDetailType_.eidtUp_D);
            }

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && stateManager.currentState != _EStateType_.estIdle)
                stateManager.SetActionType(_EStateType_.estIdle, _EObjectType_.eotPlayer);

            yield return null;
        }
    }

    protected IEnumerator InputAttack()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (stateManager.currentState != _EStateType_.estAttack)
                    stateManager.SetActionType(_EStateType_.estAttack, _EObjectType_.eotPlayer);

                SetAttack(_EInputType_.eitAttack, _EInputDetailType_.eidtAttack_W);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                SetAttack(_EInputType_.eitAttack, _EInputDetailType_.eidtAttack_S);

            yield return null;
        }
    }

    protected IEnumerator InputSkill()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
                ((PlayerStateManager)stateManager).NotifyKeyClickInput(_EInputType_.eitSkill, _EInputDetailType_.eidtUseSkill);
            else if (Input.GetKeyDown(KeyCode.Q))
                ((PlayerStateManager)stateManager).NotifyKeyClickInput(_EInputType_.eitSkill, _EInputDetailType_.eidtChangeSkill);

            yield return null;
        }
    }

    protected IEnumerator InputDash()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && GetComponent<DashCoolDown>() == null)
                stateManager.SetActionType(_EStateType_.estDash, _EObjectType_.eotPlayer);

            yield return null;
        }
    }

    IEnumerator InputMouseRotate()
    {
        while (true)
        {
            m_x = Input.GetAxis("Mouse X");
            m_y = Input.GetAxis("Mouse Y");

            cameraRot.x = m_x;
            cameraRot.y = m_y;

            ((PlayerStateManager)stateManager).NotifyMouseRotate(_EInputType_.eitRotate, _EInputDetailType_.eidtRotate, cameraRot);

            yield return null;
        }
    }

    protected void ResetDir()
    {
        for (int i = 0; i < playerDirs?.Length; i++)
            playerDirs[i] = Vector3.zero;

        playerDir = Vector3.zero;
    }

    void MovingSet(_EInputType_ _type, _EInputDetailType_ _dType)
    {
        if (_type == _EInputType_.eitMove)
        {
            if (_dType == _EInputDetailType_.eidtDown_W)
                playerDirs[0] = mainCam.transform.forward;
            if (_dType == _EInputDetailType_.eidtDown_S)
                playerDirs[1] = -mainCam.transform.forward;
            if (_dType == _EInputDetailType_.eidtDown_A)
                playerDirs[2] = -mainCam.transform.right;
            if (_dType == _EInputDetailType_.eidtDown_D)
                playerDirs[3] = mainCam.transform.right;

            if (_dType == _EInputDetailType_.eidtUp_W)
                playerDirs[0] = Vector3.zero;
            if (_dType == _EInputDetailType_.eidtUp_S)
                playerDirs[1] = Vector3.zero;
            if (_dType == _EInputDetailType_.eidtUp_A)
                playerDirs[2] = Vector3.zero;
            if (_dType == _EInputDetailType_.eidtUp_D)
                playerDirs[3] = Vector3.zero;

            playerRot = playerDir = playerDirs[0] + playerDirs[1] + playerDirs[2] + playerDirs[3];

            playerRot = playerRot.normalized;
        }
    }

    protected IEnumerator MovePlayer()
    {
        while (true)
        {
            controller.Move(playerDir.normalized * m_currentSpeed * Time.deltaTime);

            yield return null;
        }
    }

    IEnumerator RotatePlayer()
    {
        while (true)
        {
            transform.LookAt(transform.position + playerRot);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));

            yield return null;
        }
    }

    protected virtual void SetAttack(_EInputType_ _type, _EInputDetailType_ _dType)
    {
        return;
    }
}
