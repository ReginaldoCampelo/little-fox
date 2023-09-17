using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Animator playerAnim;

    // movements
    private float moveInput;
    public bool isJump;
    public bool wasOnGround;
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    private int moreJumps;
    

    // ground circle collider
    private Collider2D[] colliders_1, colliders_2;
    private float groundCheckRadius = 0.036f;
    public Transform[] groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool onGround;

    // slopes system
    public PhysicsMaterial2D noFriction, friction;
    public float slopeCheckDistance;
    private float slopeAngle;
    private bool onSlope;

    // slide system
    [Header("Slide System ----------")]
    public Transform wallCheck;
    private bool isColliderWall;
    public float wallCheckDistance;
    public bool isSliding;
    private const float WallSlideSpeedCap = 0.5f;

    [Header("Wall fall speed")]
    public float wallSlideSpeed;

    [Header("Wall jump speed")]
    public float wallJumpForce;

    private bool onSliding;




    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        runSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem();
        checkGround();
        Animations();
        Slopes();
        Slide();
    }

    private void FixedUpdate()
    {
        if(!onSliding)
        Move();
    }

    private void Move()
    {
        if(onSlope && !isJump){
            rb2d.gravityScale = 20f;
            if(rb2d.velocity.y < -2f){
                rb2d.velocity = new Vector2(moveInput * moveSpeed, -9f);
            } else{
                rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
            }

        } else{
            rb2d.gravityScale = 3f;
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
        }
    }

    private void InputSystem()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if(moveInput != 0f && !onSliding)
        {
            transform.localScale = new Vector3(moveInput, 1f, 1f);
        }

        if (Input.GetKey(KeyCode.J))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = 5f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (onGround || (moreJumps < 1 && rb2d.velocity.y > 0)))
        {
            moreJumps++;
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isSliding)
        {
            moreJumps = 1000;
            rb2d.velocity = Vector2.zero;
            rb2d.velocity = new Vector2(wallJumpForce * -moveInput, wallJumpForce);
            onSliding = true;
            StartCoroutine(jumpSlide());
        }

        if(!isSliding && rb2d.velocity.y < 0)
        {
            onSliding = false;
        }
    }

    IEnumerator jumpSlide()
    {
        transform.localScale = new Vector3(-moveInput, 1f, 1f);
        yield return new WaitForSeconds(0.3f);
    }

    void checkGround()
    {
        colliders_1 = Physics2D.OverlapCircleAll(groundCheck[0].position, groundCheckRadius, groundMask);
        colliders_2 = Physics2D.OverlapCircleAll(groundCheck[1].position, groundCheckRadius, groundMask);

        if(onGround && !wasOnGround)
        {
            isJump = false;
        }

        wasOnGround = onGround;

        if(colliders_1.Length > 0 || colliders_2.Length > 0)
        {
            onGround = true;
            moreJumps = 0;
        } else
        {
            onGround = false;
        }
    }

    private void Jump()
    {
        isJump = true;
        rb2d.gravityScale = 3f;
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    private void Slopes()
    {
        RaycastHit2D hitSlope = Physics2D.Raycast(transform.position, Vector2.down, slopeCheckDistance, groundMask);

        if(hitSlope)
        {
            slopeAngle = Vector2.Angle(hitSlope.normal, Vector2.up);

            onSlope = slopeAngle != 0;

            if(onSlope && moveInput == 0)
            {
                rb2d.sharedMaterial = friction;
            } else
            {
                rb2d.sharedMaterial = noFriction;
            }
        }
    }

    private void Slide()
    {
        isColliderWall = Physics2D.Raycast(wallCheck.position, wallCheck.TransformDirection(Vector2.right), wallCheckDistance, groundMask);

        if(isColliderWall && rb2d.velocity.y < 0 && moveInput != 0)
        {
            isSliding = true;
        } else
        {
            isSliding = false;
        }

        if(isSliding && rb2d.velocity.y < -wallSlideSpeed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlideSpeed);
        }
    }

    private void Animations()
    {
        playerAnim.SetFloat("SpeedX", Mathf.Abs(moveInput));
        playerAnim.SetFloat("SpeedY", rb2d.velocity.y);
        playerAnim.SetBool("isOnGround", onGround);
        playerAnim.SetFloat("CurrentSpeed", moveSpeed);
        playerAnim.SetBool("isSliding", isSliding);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 8)
        {
            GameController.instance.RestartGame();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck[0].position, groundCheckRadius);
        Gizmos.DrawSphere(groundCheck[1].position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
