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

    float currentSpeed;

    float x;
    float y;

    Vector2 cameraRot;

    protected override void Awake()
    {
        base.Awake();

        gameMgr = GameObject.Find("Manager").GetComponent<GameManager>();

        mainCam = Camera.main;

        playerDirs = new Vector3[4];

        controller = GetComponent<CharacterController>();

        currentSpeed = myStat.GetFloatStat(_EFloatStatType_.eSpeed);
    }

    public override void DoAction(_EStateType_ state)
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
                ((PlayerStateManager)stateManager).NotifyKeyClickInput(_EInputType_.eOption, _EInputDetailType_.eOption);

            yield return null;
        }
    }

    protected IEnumerator InputLine()
    {
        while (true)
        {
            if (gameMgr.GetGameState(_EGameStateType_.eisLine) && Input.GetKeyDown(KeyCode.F))
                ((PlayerStateManager)stateManager).NotifyKeyClickInput(_EInputType_.eDialogue, _EInputDetailType_.eDialogue);

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
                    MovingSet(_EInputType_.eMove, _EInputDetailType_.eDown_W);
                if (Input.GetKey(KeyCode.S))
                    MovingSet(_EInputType_.eMove, _EInputDetailType_.eDown_S);
                if (Input.GetKey(KeyCode.A))
                    MovingSet(_EInputType_.eMove, _EInputDetailType_.eDown_A);
                if (Input.GetKey(KeyCode.D))
                    MovingSet(_EInputType_.eMove, _EInputDetailType_.eDown_D);

                if (stateManager.currentState != _EStateType_.eMove)
                    stateManager.SetActionType(_EStateType_.eMove, _EObjectType_.ePlayer);
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
                    MovingSet(_EInputType_.eMove, _EInputDetailType_.eUp_W);
                if (Input.GetKeyUp(KeyCode.A))
                    MovingSet(_EInputType_.eMove, _EInputDetailType_.eUp_A);
                if (Input.GetKeyUp(KeyCode.S))
                    MovingSet(_EInputType_.eMove, _EInputDetailType_.eUp_S);
                if (Input.GetKeyUp(KeyCode.D))
                    MovingSet(_EInputType_.eMove, _EInputDetailType_.eUp_D);
            }

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && stateManager.currentState != _EStateType_.eIdle)
                stateManager.SetActionType(_EStateType_.eIdle, _EObjectType_.ePlayer);

            yield return null;
        }
    }

    protected IEnumerator InputAttack()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (stateManager.currentState != _EStateType_.eAttack)
                    stateManager.SetActionType(_EStateType_.eAttack, _EObjectType_.ePlayer);

                SetAttack(_EInputType_.eAttack, _EInputDetailType_.eAttack_W);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                SetAttack(_EInputType_.eAttack, _EInputDetailType_.eAttack_S);

            yield return null;
        }
    }

    protected IEnumerator InputSkill()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
                ((PlayerStateManager)stateManager).NotifyKeyClickInput(_EInputType_.eSkill, _EInputDetailType_.eUseSkill);
            else if (Input.GetKeyDown(KeyCode.Q))
                ((PlayerStateManager)stateManager).NotifyKeyClickInput(_EInputType_.eSkill, _EInputDetailType_.eChangeSkill);

            yield return null;
        }
    }

    protected IEnumerator InputDash()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && GetComponent<DashCoolDown>() == null)
                stateManager.SetActionType(_EStateType_.eDash, _EObjectType_.ePlayer);

            yield return null;
        }
    }

    IEnumerator InputMouseRotate()
    {
        while (true)
        {
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");

            cameraRot.x = x;
            cameraRot.y = y;

            ((PlayerStateManager)stateManager).NotifyMouseRotate(_EInputType_.eRotate, _EInputDetailType_.eRotate, cameraRot);

            yield return null;
        }
    }

    protected void ResetDir()
    {
        for (int i = 0; i < playerDirs?.Length; i++)
            playerDirs[i] = Vector3.zero;

        playerDir = Vector3.zero;
    }

    void MovingSet(_EInputType_ type, _EInputDetailType_ dType)
    {
        if (type == _EInputType_.eMove)
        {
            if (dType == _EInputDetailType_.eDown_W)
                playerDirs[0] = mainCam.transform.forward;
            if (dType == _EInputDetailType_.eDown_S)
                playerDirs[1] = -mainCam.transform.forward;
            if (dType == _EInputDetailType_.eDown_A)
                playerDirs[2] = -mainCam.transform.right;
            if (dType == _EInputDetailType_.eDown_D)
                playerDirs[3] = mainCam.transform.right;

            if (dType == _EInputDetailType_.eUp_W)
                playerDirs[0] = Vector3.zero;
            if (dType == _EInputDetailType_.eUp_S)
                playerDirs[1] = Vector3.zero;
            if (dType == _EInputDetailType_.eUp_A)
                playerDirs[2] = Vector3.zero;
            if (dType == _EInputDetailType_.eUp_D)
                playerDirs[3] = Vector3.zero;

            playerRot = playerDir = playerDirs[0] + playerDirs[1] + playerDirs[2] + playerDirs[3];

            playerRot = playerRot.normalized;
        }
    }

    protected IEnumerator MovePlayer()
    {
        while (true)
        {
            controller.Move(playerDir.normalized * currentSpeed * Time.deltaTime);

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

    protected virtual void SetAttack(_EInputType_ type, _EInputDetailType_ dType)
    {
        return;
    }
}
