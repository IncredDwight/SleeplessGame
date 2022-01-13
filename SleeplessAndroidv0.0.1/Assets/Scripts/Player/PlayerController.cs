using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*public Rigidbody2D Rb2d { get; private set; }
    public float Direction { get; private set; }

    private Animator animator;
    private SpriteRenderer sprite;
    private float currentScale;

    private float movementSpeed;
    private Vector3 movement;

    private float jumpForce;
    private float jumpTime;
    private bool isJumping;

    private bool grounded;

    private bool isClimbing;
    private LayerMask isLadderHere;

    private bool flip;

    private void Awake()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        currentScale = transform.localScale.x;
        movementSpeed = 8.7f;
        jumpForce = 5.8f;
        jumpTime = 0.4f;
        isLadderHere = LayerMask.GetMask("Ladder");
    }

    private void Update()
    {
        GetInput();
        GroundCheck();
        LadderClimb();
    }

    private void Run()
    {
        Flip();
        Direction = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(Direction));
        movement.x = movementSpeed * Direction;
        movement.y = Rb2d.velocity.y;
        Rb2d.velocity = movement;
    }

    private void LadderClimb()
    {
        RaycastHit2D isLadder = Physics2D.Raycast(transform.position, Vector2.up, 10.0f, isLadderHere);
        isClimbing = (isLadder.collider != null) ? true : false;
        if (isClimbing)
        {
            float directionY = Input.GetAxisRaw("Vertical");
            Rb2d.velocity = new Vector2(Rb2d.velocity.x, directionY * movementSpeed);
            Rb2d.gravityScale = 0;
        }
        else
            Rb2d.gravityScale = 1;
    }

    private void Flip()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
        {
            transform.localScale = new Vector2(currentScale * -1, transform.localScale.y);
            flip = true;
        }
        else
        {
            flip = false;
            transform.localScale = new Vector2(currentScale, transform.localScale.y);
        }

        if (Direction == -1)
        {
            flip = false;
            transform.localScale = new Vector2(currentScale, transform.localScale.y);
        }
        else if (Direction == 1)
        {
            flip = true;
            transform.localScale = new Vector2(currentScale * -1, transform.localScale.y);
        }
    }

    private void Jump()
    {
        Rb2d.velocity = new Vector2(Rb2d.velocity.x ,Vector2.up.y * jumpForce);
    }

    private void GroundCheck()
    {
        grounded = movement.y == 0;
    }

    private void GetInput()
    {
        Run();
        if (Input.GetButtonDown("Jump") && grounded)
        {
            isJumping = true;
            jumpTime = 0.4f;
            Jump();
            animator.SetTrigger("TakeOff");
        }
        if (grounded)
            animator.SetBool("IsJumping", false);
        else
            animator.SetBool("IsJumping", true);

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTime > 0)
            {
                jumpTime -= Time.deltaTime;

                Jump();
            }
            else
                isJumping = false;
        }
        else
            isJumping = false;
    }*/
}
