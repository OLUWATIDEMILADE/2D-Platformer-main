using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    private float jumpPower = 12f;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        //Check if the player is grounded here to ensure the player is not jumping mid air
        //Make the player jump
        CheckIfGrounded();
        PlayerJump();
    }

    void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {

        float h = Input.GetAxis("Horizontal"); //replace "0" as the value of float h with the correct axis of movement.
                                               //Note: The value of h must use the right and left or "a" and "d" keys to move the player
                                               //right and left.

        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);

            ChangeDirection(1);

        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);

            ChangeDirection(-1);

        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));

    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    //Checking if the player is on the ground
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded)
        {
            // and we jumped before
            if (jumped)
            {

                jumped = false;

                anim.SetBool("Jump", false);
            }
        }

    }

    //Make the player jump
    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);

                anim.SetBool("Jump", true);
            }
        }
    }

} // class





























