using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private float jumpPower = 12f;

    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    // Radius for ground check
    private float groundCheckRadius = 0.2f;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is grounded
        CheckIfGrounded();

        // Make the player jump
        PlayerJump();
    }

    void FixedUpdate()
    {
        // Move the player
        PlayerWalk();
    }

    // Handle Player Walking
    void PlayerWalk()
    {
        // Get horizontal input
        float h = Input.GetAxis("Horizontal");

        // Move right
        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
        }
        // Move left
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        // Idle
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        // Update animation state
        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    // Change Player Facing Direction
    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    // Check if Player is on the Ground
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayer);

        // Reset jump animation if grounded
        if (isGrounded && jumped)
        {
            jumped = false;
            anim.SetBool("Jump", false);
        }
    }

    // Make the Player Jump
    void PlayerJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
            anim.SetBool("Jump", true);
        }
    }

    // Optional: Visualize Ground Check Radius in Scene View
    void OnDrawGizmos()
    {
        if (groundCheckPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
        }
    }
}
