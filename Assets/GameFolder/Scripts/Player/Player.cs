using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public Animator playerAnim;
    [SerializeField] private LayerMask groundMask;
    private float moveInput;
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    [SerializeField] private bool onGround;
    private bool wasOnGround;
    private bool isJump;

    // ground circle collider
    private Collider2D[] colliders_1, colliders_2;
    private float groundCheckRadius = 0.036f;
    public Transform[] groundCheck;

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
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
    }

    private void InputSystem()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if(moveInput != 0f)
        {
            transform.localScale = new Vector3(moveInput, 1f, 1f);
        }

        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.J))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = 5f;
        }
    }

    void checkGround()
    {
        colliders_1 = Physics2D.OverlapCircleAll(groundCheck[0].position, groundCheckRadius, groundMask);
        colliders_2 = Physics2D.OverlapCircleAll(groundCheck[1].position, groundCheckRadius, groundMask);

        if(colliders_1.Length > 0 || colliders_2.Length > 0)
        {
            onGround = true;
        } else
        {
            onGround = false;
        }
    }

    private void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    private void Animations()
    {
        playerAnim.SetFloat("SpeedX", Mathf.Abs(moveInput));
        playerAnim.SetFloat("SpeedY", rb2d.velocity.y);
        playerAnim.SetBool("isOnGround", onGround);
        playerAnim.SetFloat("CurrentSpeed", moveSpeed);
    }
}
