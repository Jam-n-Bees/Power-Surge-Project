using UnityEngine;

public class PlayerShmovement : MonoBehaviour
{
    #region Movement Declarations
    //MOVEMENT SECTION
    public float moveSpeed;
    

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    public float groundDrag;
    #endregion

    #region Drag Declaratiosn
    //DRAG SECTION
    //and not the one with all the pretty people
    public float playerHeight;
    //We use these masks essentially for important checks! (Touching a wall or floor, etc etc)
    public LayerMask Ground;
    private bool grounded;
    #endregion




	public bool useGravity = true;


	void FixedUpdate() {

        //We put this function in fixed update, because it's applying a physics force to an object.
        MovePlayer();
        rb.AddForce(new Vector3(0, -4, 0) * 14, ForceMode.Acceleration);
	}





    void Start()
    {
        //freezing rotation so it's not tumbling around nonstop
        //and grabbing the rigidbody component to an assigned variable so it's easy to access
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }









    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                rb.AddForce(0, 1500, 0);
            }
             
        }





        
        PlayerInput();
        #region Long winded comment rambling
        //checking if we are touching the ground
        //Essentially the Raycast is emmitting from the center of our player (which is 1 unit tall)
        //So the length of the raycast will be half our height, plus a little bit extra, because of physics engine jank,
        //We might not ALWAYS be exactly lined up with the ground for this to trigger.
        //Also gives a small about of leeway with regards to jumps and stuff

        //The below comments were written after fixing everythign and getting it working.
        //Note : i feel like the TF2 dev who couldn't understand why the game needed a png of a coconut to run
        //this was a nightmare to figure out
        //Intellisense was borked and my visual studio code environment was very broken, which I didn't realise until like, 20 minutes ago
        //scrunkling through various unity threads where the code exists differently in different versions
        //syntax changing and then it not being clear about what is defined where, and how it actually works
        //finally after fixing my code environment, resetting some things in unity and here, i got intellisense back and managed to figure it out
        //i miss godot and my massive active community always there to lend a hand with dev questions
        //and the fact that godot has a built in code editor, with godot in mind so it's very well tailored for its tasks
        //the amount of restraint I'm using to not just flood this section with curse words (because this is a uni project) is monumental
        #endregion

        RaycastHit hit;
        grounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f, Ground);
        Debug.DrawRay(transform.position, Vector3.down * 1.1f, Color.red);


        //Essentially, if the object is touching the ground, apply linear drag, if not, don't apply it!
        //makes it so we're not super slippery.
        if (grounded)
        {
            rb.linearDamping = groundDrag;
            Debug.Log("touching ground");
        }
        else
        {
            rb.linearDamping = 0f;
            Debug.Log("Not Touching");
        }

        SpeedControl();
    }



    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
