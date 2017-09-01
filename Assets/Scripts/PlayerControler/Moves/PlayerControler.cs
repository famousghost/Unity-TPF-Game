using UnityEngine;
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

    #region Bool Variables

    [SerializeField]
    private bool canRun = true;

    [SerializeField]
    private bool canLift = true;

    [SerializeField]
    private bool canJump = true;

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
    #endregion

    #region System Methods
    // Use this for initialization
    void Start()
    {
        stamina = MAXSTAMINA;
        rigidBody = GetComponent<Rigidbody>();
        keyInput = GetComponent<KeysInput>();
        cam = GetComponentInChildren<Camera>();
        gameObj = GameObject.FindGameObjectWithTag("help").GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangePlayerStates();
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
        CheckPlayerState();
        walkingX = Vector3.right * walkSpeed * keyInput.GetWalkX() * Time.deltaTime;
        walkingY = Vector3.forward * walkSpeed * keyInput.GetWalkY() * Time.deltaTime;
        Walks = walkingX + walkingY;
        rigidBody.transform.Translate(Walks);
    }
    #endregion

    #region CheckPlayerStates
    private void CheckPlayerState()
    {
        switch(playerStates)
        {
            case PlayerStates.PlayerWalk:
                walkSpeed = WALKSPEED;
                break;
            case PlayerStates.PlayerRun:
                walkSpeed = RUNSPEED;
                break;
            case PlayerStates.PlayerCrouch:
                break;
            case PlayerStates.PlayerIdle:
                walkSpeed = 0.0f;
                break;
            default:
                walkSpeed = 0.0f;
                break;
        }
    }
    #endregion

    #region ChangePlayerStates
    private void ChangePlayerStates()
    {
        var playerIsWalking = (keyInput.GetWalkX() != 0 || keyInput.GetWalkY() != 0);
        var playerCanRun = (keyInput.GetIsRunning() == true && canRun == true) && playerIsWalking;
        if (playerCanRun)
        {
            playerStates = PlayerStates.PlayerRun;
        }
        else if(playerIsWalking)
        {
            playerStates = PlayerStates.PlayerWalk;
        }
        else
        {
            playerStates = PlayerStates.PlayerIdle;
        }
    }
    #endregion

    #region RotationeMethod
    private void Rotatione()
    {
        Vector3 rotationeX;
        rotationeX = Vector3.up * sensivity * keyInput.GetMouseX() * Time.deltaTime;
        rigidBody.transform.rotation *= Quaternion.Euler(rotationeX);
        rotationeY -= keyInput.GetMouseY() * sensivity * Time.deltaTime;
        rotationeY = Mathf.Clamp(rotationeY, MINIMALROTATIONEY, MAXIMUMROTATIONEY);
        cam.transform.localRotation = Quaternion.Euler(rotationeY,0, 0);
    }
    #endregion

    #region Jumping
    private void DoJump()
    {
        Vector3 jumper;
        PlayerIsOnGrounded();
        var playerCanJump = keyInput.GetIsJupming() && keyInput.GetOnGrounded() && canJump;
        if (playerCanJump)
        {
            keyInput.SetOnGrounded(false);
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
        Ray rayToGround = new Ray(rigidBody.transform.position, -rigidBody.transform.up);
        RaycastHit hitCollider;
        Debug.DrawRay(rigidBody.transform.position, -rigidBody.transform.up, Color.green);
        if (Physics.Raycast(rayToGround,out hitCollider,MAXDISTANCEOFGROUNDEDRAY))
        {
            keyInput.SetOnGrounded(true);
        }
    }

    #endregion

    #region exhaustion

    private void Exhaustion()
    {
        if (playerStates == PlayerStates.PlayerRun && stamina >= MINSTAMINA && keyInput.GetWalkY() > 0)
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
            playerStates = PlayerStates.PlayerWalk;
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

        //rysowanie promienia z kamery
        Debug.DrawRay(cam.transform.position, forward * 20, Color.green);


        if (Physics.Raycast(ray, out hit, MAXDISTANCEOFRAY))
        {
            //rysowanie promienia z kamery
            Debug.DrawRay(cam.transform.position, forward * 20, Color.red);
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
                    Physics.IgnoreCollision(gameObj.transform.GetComponent<Collider>(), rigidBody.transform.GetComponent<Collider>(), false);
                    gameObj = GameObject.FindGameObjectWithTag("help").GetComponent<Rigidbody>();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    keyInput.SetLiftetd(false);
                    gameObj.useGravity = true;
                    gameObj.constraints = RigidbodyConstraints.None;

                    canLift = false;

                    gameObj.AddForce(cam.transform.forward * 5, ForceMode.Impulse);
                    Physics.IgnoreCollision(gameObj.transform.GetComponent<Collider>(), rigidBody.transform.GetComponent<Collider>(), false);
                    gameObj = GameObject.FindGameObjectWithTag("help").GetComponent<Rigidbody>();
                }
            }
            else
            {
                keyInput.SetLiftetd(false);
                gameObj.useGravity = true;
                gameObj.constraints = RigidbodyConstraints.None;
                Physics.IgnoreCollision(gameObj.transform.GetComponent<Collider>(), rigidBody.transform.GetComponent<Collider>(), false);
                gameObj = GameObject.FindGameObjectWithTag("help").GetComponent<Rigidbody>();
            }

        }

        else
        {
            keyInput.SetLiftetd(false);
            canLift = true;
            gameObj.useGravity = true;
            gameObj.constraints = RigidbodyConstraints.None;
            gameObj = GameObject.FindGameObjectWithTag("help").GetComponent<Rigidbody>();
        }
       
    }
    #endregion
}
