﻿using UnityEngine;
using System.Collections;

#region PlayerState Enumerator
public enum PlayerStates
{
    PlayerIdle = 0,
    PlayerWalk,
    PlayerRun,
    PlayerCrouch
}
#endregion

public class PlayerControler : MonoBehaviour
{
    #region PlayerState
    [Header("Player States Enum")]
    [SerializeField]
    PlayerStates playerStates;
    #endregion
    //Player RigidBody use to gravity and walking
    #region Player
    [Header("Player")]
    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private Camera cam;
    #endregion
    //Keys input walking,runing, jumping, use
    #region GameObjects

    [SerializeField]
    private Rigidbody gameObj;

    #endregion

    #region Audio
    [Header("Audio")]
    [SerializeField]
    private MovmentSound movementSound;

    [SerializeField]
    private AudioSource playerSource;
    #endregion

    #region Vector3
    [Header("Vector3")]
    [SerializeField]
    private Vector3 mainCamPosition;
    #endregion

    #region Bool Variables

    [SerializeField]
    private bool canRun = true;

    [SerializeField]
    private bool canLift = true;

    [SerializeField]
    private bool canJump = true;

    [SerializeField]
    private bool playerCanStand = true;

    #endregion

    #region KeyClass
    [Header("KeyClass")]
    [SerializeField]
    private KeysInput keyInput;

    //Variables like walkSpeed, runSpeed
    #endregion

    #region Const Float

    private const float MINIMALROTATIONEY = -80.0f;

    private const float MAXIMUMROTATIONEY = 80.0f;

    private const float WALKSPEED = 3.0f;

    private const float CROUCHSPEED = 1.0f;

    private const float RUNSPEED = 5.0f;

    private const float MAXSTAMINA = 100.0f;

    private const float MINSTAMINA = 0.0f;

    private const float MAXDISTANCEOFRAY = 1.4f;

    private const float MAXDISTANCEOFGROUNDEDRAY = 1.2f;


    #endregion

    #region Player Float Variables
    [Header("Float Variables")]
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float stamina;

    [SerializeField]
    private float sensivity = 90.0f;

    [SerializeField]
    private float jumpGravity = 230.0f;

    private float rotationeY;

    [SerializeField]
    private float rotateX;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start()
    {
        stamina = MAXSTAMINA;
        rigidBody = GetComponent<Rigidbody>();
        keyInput = GetComponent<KeysInput>();
        cam = GetComponentInChildren<Camera>();
        //gameObj = GameObject.FindGameObjectWithTag("help").GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        mainCamPosition = cam.transform.localPosition;
        playerSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        movementSound = GetComponent<MovmentSound>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangePlayerStates();
        CheckPlayerState();
        LookSides();
        CheckIfPlayerIsUnderTheColider();
        Walking();
        Exhaustion();
        Rotatione();
        LiftObject();
        DoJump();
        keyInput.Inputs();
    }
    #endregion

    #region WalkMethods
    private void Walking()
    {
         Vector3 walkingX, walkingY;
         Vector3 Walks;
         walkingX = Vector3.right * walkSpeed * keyInput.GetWalkX() * Time.deltaTime;
         walkingY = Vector3.forward * walkSpeed * keyInput.GetWalkY() * Time.deltaTime;
         Walks = walkingX + walkingY;
         rigidBody.transform.Translate(Walks);
    }
    #endregion

    #region RotationeMethod
    private void Rotatione()
    {
        Vector3 rotationeX;
        if (keyInput.GetRightSide() || keyInput.GetLeftSide())
        {
            rotateX += keyInput.GetMouseX() * sensivity  * Time.deltaTime;
            rotateX = Mathf.Clamp(rotateX, MINIMALROTATIONEY, MAXIMUMROTATIONEY);
        }
        else
        {
            rotateX = 0.0f;
            rotationeX = Vector3.up * sensivity * keyInput.GetMouseX() * Time.deltaTime;
            rigidBody.transform.rotation *= Quaternion.Euler(rotationeX);
        }
        rotationeY -= keyInput.GetMouseY() * sensivity * Time.deltaTime;
        rotationeY = Mathf.Clamp(rotationeY, MINIMALROTATIONEY, MAXIMUMROTATIONEY);
        cam.transform.localRotation = Quaternion.Euler(rotationeY, rotateX, 0);
    }
    #endregion

    #region CheckPlayerStates
    private void CheckPlayerState()
    {
        switch(playerStates)
        {
            case PlayerStates.PlayerIdle:
                StandUpAnimation();
                movementSound.StopSound();
                break;
            case PlayerStates.PlayerWalk:
                StandUpAnimation();
                if (keyInput.GetOnGrounded())
                    movementSound.PlayWalkSound();
                else
                    movementSound.StopSound();
                walkSpeed = WALKSPEED;
                break;
            case PlayerStates.PlayerRun:
                StandUpAnimation();
                if (keyInput.GetOnGrounded())
                    movementSound.PlayRunSound();
                else
                    movementSound.StopSound();
                walkSpeed = RUNSPEED;
                break;
            case PlayerStates.PlayerCrouch:
                CrouchAnimation();
                movementSound.StopSound();
                walkSpeed = CROUCHSPEED;
                break;
            default:
                walkSpeed = 0.0f;
                break;
        }
    }
    #endregion

    #region Crouch And StandUp Animation
    private void CrouchAnimation()
    {
        Vector3 stand = new Vector3(rigidBody.transform.localScale.x, rigidBody.transform.localScale.y, rigidBody.transform.localScale.z);
        Vector3 crouch = new Vector3(rigidBody.transform.localScale.x, rigidBody.transform.localScale.y / 2.0f, rigidBody.transform.localScale.z);
        if (rigidBody.transform.localScale.y >= 0.5f)
            rigidBody.transform.localScale = Vector3.Lerp(stand, crouch, Time.deltaTime * 2.0f);
    }

    private void StandUpAnimation()
    {
        Vector3 crouch = new Vector3(rigidBody.transform.localScale.x, rigidBody.transform.localScale.y, rigidBody.transform.localScale.z);
        Vector3 stand = new Vector3(rigidBody.transform.localScale.x, rigidBody.transform.localScale.y * 2.0f, rigidBody.transform.localScale.z);
        if (rigidBody.transform.localScale.y <= 1.0f && playerCanStand)
            rigidBody.transform.localScale = Vector3.Lerp(crouch,stand, Time.deltaTime * 2.0f);
    }
    #endregion

    #region Check If Player Is unders something
    private void CheckIfPlayerIsUnderTheColider()
    {
        Ray ray = new Ray(rigidBody.transform.position, rigidBody.transform.up);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,2.0f))
        {
           playerStates = PlayerStates.PlayerCrouch;
           playerCanStand = false;
        }
        else
        {
            playerCanStand = true;
        }
    }
    #endregion

    #region LookSides
    private void LookSides()
    {
        var leftLook = keyInput.GetLeftSide() && (cam.transform.localPosition.x >= -0.5f);
        var backFromLeft = !keyInput.GetLeftSide() && (cam.transform.localPosition.x < 0.0f);
        var rightLook = keyInput.GetRightSide() && (cam.transform.localPosition.x <= 0.5f);
        var backFromRight = !keyInput.GetRightSide() && (cam.transform.localPosition.x > 0.0f);

        //Left Side
        if (leftLook)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x - 0.5f, cam.transform.localPosition.y, cam.transform.localPosition.z), Time.deltaTime * 2.0f);
        }
        else if(backFromLeft)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(mainCamPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z), Time.deltaTime * 3.0f);
        }
        //Right Side
        if (rightLook)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x + 0.5f, cam.transform.localPosition.y, cam.transform.localPosition.z), Time.deltaTime * 2.0f);
        }
        else if (backFromRight)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(mainCamPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z), Time.deltaTime * 3.0f);
        }
    }
    #endregion

    #region ChangePlayerStates
    private void ChangePlayerStates()
    {
        var playerIsWalking = (keyInput.GetWalkX() != 0 || keyInput.GetWalkY() != 0) && playerStates != PlayerStates.PlayerCrouch;
        var playerCanRun = (keyInput.GetIsRunning() && canRun == true) && playerStates != PlayerStates.PlayerCrouch;
        var playerCanCrouch = (keyInput.GetIsCrouching()) && playerStates != PlayerStates.PlayerRun;
        if (playerCanRun)
        {
            playerStates = PlayerStates.PlayerRun;
        }
        else if (playerCanCrouch)
        {
            playerStates = PlayerStates.PlayerCrouch;
        }
        else if (playerIsWalking)
        {
            playerStates = PlayerStates.PlayerWalk;
        }
        else
        {
            playerStates = PlayerStates.PlayerIdle;
        }
    }
    #endregion

    #region Jumping
    private void DoJump()
    {
        Vector3 jumper;
        PlayerIsOnGrounded();
        var playerCanJump = keyInput.GetIsJupming() && keyInput.GetOnGrounded() && canJump && playerStates != PlayerStates.PlayerCrouch;
        if (playerCanJump)
        {
            keyInput.SetOnGrounded(false);
            playerSource.PlayOneShot(movementSound.GetJumpSound());
            jumper = Vector3.up * jumpGravity * Time.deltaTime;
            rigidBody.AddForce(jumper, ForceMode.Impulse);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        canJump = false;
    }

    void OnCollisionStay(Collision collision)
    {
        canJump = true;
    }

    private void PlayerIsOnGrounded()
    {
        if (!keyInput.GetOnGrounded())
        {
            Ray rayToGround = new Ray(rigidBody.transform.position, -rigidBody.transform.up);
            RaycastHit hitCollider;
            Debug.DrawRay(rigidBody.transform.position, -rigidBody.transform.up, Color.green);
            if (Physics.Raycast(rayToGround, out hitCollider, MAXDISTANCEOFGROUNDEDRAY))
            {
                keyInput.SetOnGrounded(true);
            }
        }
    }

    #endregion

    #region exhaustion

    private void Exhaustion()
    {
        if (playerStates == PlayerStates.PlayerRun && stamina >= MINSTAMINA)
        {
            stamina -= 0.5f;
            if (stamina <= MINSTAMINA)
            {
                stamina = 0.0f;
                canRun = false;
            }
        }
        else if (stamina <= MAXSTAMINA)
        {
            stamina += 0.5f;
            if (stamina >= MAXSTAMINA)
            {
                stamina = 100.0f;
                canRun = true;
            }

        }
    }

    #endregion

    #region Lift GameObjects

    private void LiftObject()
    {
        RaycastHit hit;

        //Vector3 skierowany z kamery
        Vector3 forward = cam.transform.forward;

        //promien puszczony wprost z kamery na przod
        Ray ray = new Ray(cam.transform.position, forward);


        if (Physics.Raycast(ray, out hit, MAXDISTANCEOFRAY))
        {
            if (keyInput.GetIsLifted() == true)
            {
                
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Lifted") && canLift == true)
                {
                    gameObj = hit.rigidbody;
                    gameObj.transform.position = cam.transform.position + cam.transform.forward;
                    gameObj.transform.rotation = cam.transform.rotation;
                    Physics.IgnoreCollision(gameObj.transform.GetComponent<Collider>(), rigidBody.transform.GetComponent<Collider>(),true);


                    gameObj.useGravity = false;
                    //Freezowanie rotacji i pozycji tylko y i z
                    gameObj.constraints = RigidbodyConstraints.FreezeAll;
                }
                else
                {
                    if (gameObj != null)
                    {
                        Physics.IgnoreCollision(gameObj.transform.GetComponent<Collider>(), rigidBody.transform.GetComponent<Collider>(), false);
                        gameObj = null;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (gameObj != null)
                    {
                        keyInput.SetLiftetd(false);
                        gameObj.useGravity = true;
                        gameObj.constraints = RigidbodyConstraints.None;

                        canLift = false;

                        gameObj.AddForce(cam.transform.forward * 5, ForceMode.Impulse);
                        Physics.IgnoreCollision(gameObj.transform.GetComponent<Collider>(), rigidBody.transform.GetComponent<Collider>(), false);
                        gameObj = null;
                    }
                }
            }
            else
            {
                if (gameObj != null)
                {
                    keyInput.SetLiftetd(false);
                    gameObj.useGravity = true;
                    gameObj.constraints = RigidbodyConstraints.None;
                    Physics.IgnoreCollision(gameObj.transform.GetComponent<Collider>(), rigidBody.transform.GetComponent<Collider>(), false);
                    gameObj = null;
                }
            }

        }

        else
        {
            if (gameObj != null)
            {
                keyInput.SetLiftetd(false);
                gameObj.useGravity = true;
                gameObj.constraints = RigidbodyConstraints.None;
                gameObj = null;
            }
            else
            {
                canLift = true;
            }
        }
       
    }
    #endregion
}
