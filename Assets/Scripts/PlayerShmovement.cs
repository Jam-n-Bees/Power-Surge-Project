using UnityEngine;

public class PlayerShmovement : MonoBehaviour
{
    //MOVEMENT SECTION
    public float moveSpeed;
    
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    public float groundDrag;

    //DRAG SECTION
    //and not the one with all the pretty people
    public float playerHeight;
    //We use these masks essentially for important checks! (Touching a wall or floor, etc etc)
    public LayerMask Ground;
    private bool grounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //freezing rotation so it's not tumbling around nonstop
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        //checking if we are touching the ground
        //Essentially the Raycast is emmitting from the center of our player (which is 1 unit tall)
        //So the length of the raycast will be half our height, plus a little bit extra, because of physics engine jank,
        //We might not ALWAYS be exactly lined up with the ground for this to trigger.
        //Also gives a small about of leeway with regards to jumps and stuff
        RaycastHit hit;
        grounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f, Ground);
        Debug.DrawRay(transform.position, Vector3.down * 1.1f , Color.red);

        if(grounded)
        {
            rb.linearDamping = groundDrag;
            Debug.Log("touching ground");
        }
        else
        {
            rb.linearDamping = 0f;
            Debug.Log("Not Touching");
        }

    }

    void FixedUpdate()
    {
        //We put this function in fixed update, because it's applying a physics force to an object.
        MovePlayer();
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
}
