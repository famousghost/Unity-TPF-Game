using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour
{

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
    private bool running = false;

    [SerializeField]
    private bool canRun = true;

    [SerializeField]
    private bool canLift = true;

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
    private float jumpGravity = 300.0f;

    private float rotationeY;
    #endregion

    #region System Functions
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
    void Update()
    {
        Walking();
        Exhaustion();
        Rotatione();
        LiftObject();
        DoJump();
        keyInput.Inputs();
    }
    #endregion

    #region WalkMethod
    private void Walking()
    {
        Vector3 walkingX, walkingY;
        Vector3 Walks;
        if (keyInput.GetIsRunning() == true && canRun == true)
        {
            walkSpeed = RUNSPEED;
            running = true;
        }
        else
        {
            walkSpeed = WALKSPEED;
            running = false;
        }
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
        if (keyInput.GetIsJupming() == true && keyInput.GetOnGrounded() == true)
        {
            jumper = Vector3.up * jumpGravity * Time.deltaTime;
            rigidBody.AddForce(jumper, ForceMode.Impulse);
        }
        keyInput.SetOnGrounded(false);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            keyInput.SetOnGrounded(true);
        }
    }

    #endregion

    #region exhaustion

    private void Exhaustion()
    {
        if (running == true && stamina >= MINSTAMINA && keyInput.GetWalkY() > 0)
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
            running = false;
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
                    Debug.Log("Mozna podniesc");
                    gameObj.transform.parent = cam.transform;

                    //gameObj.isKinematic = true;
                    gameObj.useGravity = false;
                    //Freezowanie rotacji i pozycji tylko y i z
                    gameObj.constraints = RigidbodyConstraints.FreezeRotation 
                        | RigidbodyConstraints.FreezePositionZ 
                        | RigidbodyConstraints.FreezePositionY;
                    gameObj = hit.rigidbody;
                }
                else
                {
                    gameObj.transform.parent = null;
                    gameObj = GameObject.FindGameObjectWithTag("help").GetComponent<Rigidbody>();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    keyInput.SetLiftetd(false);

                    gameObj.transform.parent = null;

                    //gameObj.isKinematic = false;
                    gameObj.useGravity = true;
                    gameObj.constraints = RigidbodyConstraints.None;

                    canLift = false;

                    gameObj.AddForce(cam.transform.forward * 5, ForceMode.Impulse);

                    gameObj = GameObject.FindGameObjectWithTag("help").GetComponent<Rigidbody>();

                }
            }
            else
            {
                keyInput.SetLiftetd(false);
                gameObj.transform.parent = null;
                //gameObj.isKinematic = false;
                gameObj.useGravity = true;
                gameObj.constraints = RigidbodyConstraints.None;
                gameObj = GameObject.FindGameObjectWithTag("help").GetComponent<Rigidbody>();
            }

        }

        else
        {
            keyInput.SetLiftetd(false);
            canLift = true;
            gameObj.transform.parent = null;
            //gameObj.isKinematic = false;
            gameObj.useGravity = true;
            gameObj.constraints = RigidbodyConstraints.None;
            gameObj = GameObject.FindGameObjectWithTag("help").GetComponent<Rigidbody>();
        }
       
    }
    #endregion
}
