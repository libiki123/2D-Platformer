using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerCollision coll;

    private float movementInputDirection;
    private int facingDirection = 1;
    private bool isFacingRight = true;
    private int lastWallJumpDirection;

    private Vector2 ledgePos1;
    private Vector2 ledgePos2;

    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;

    private int amountOfJumpsLeft;
    private float dashTimeLeft;
    
    private bool isWalking;
    private bool isWallSliding;
    private bool isAttemptingToJump;
    private bool isDashing;

    private bool canNormalJump;
    private bool canWallJump;
    private bool canMove;
    private bool canFlip;
    private bool canClimbLedge = false;

    private bool checkJumpMultiplier;
    private bool hasWallJumped;
    private float lastImgXpos;          // last dashin img
    private float lastDash = -100f;

    

    [Space]
    [Header("Movement")]
    public float movementSpeed = 10.0f;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;

    [Header("Jump")]
    public int amountOfJumps = 1;
    public float jumpForce = 16.0f;
    public float variableJumpHeightMultiplier = 0.5f;       // decrease jump force
    public float wallJumpForce;
    public Vector2 wallJumpDirection;               // angle to apply force

    [Header("Slide")]
    public float wallSlideSpeed;

    [Header("Dash")]
    public float dashTime;
    public float dashSpeed;
    public float dashCooldown;
    public float distanceBetweenImgs;       // distance between after imgs

    [Header("Ledge")]
    public float lefgeClimbXOffset1 = 0.5f;
    public float lefgeClimbYOffset1 = 0f;
    public float lefgeClimbXOffset2 = 0f;
    public float lefgeClimbYOffset2 = 0f;

    [Header("Timers")]
    public float jumpTimerSet = 0.15f;
    public float turnTimerSet = 0.1f;
    public float wallJumpTimerSet = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<PlayerCollision>();
        amountOfJumpsLeft = amountOfJumps;
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();

        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        CheckLedgeClimb();
        CheckDash();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }


    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (coll.isGrounded || (amountOfJumpsLeft > 0 && !coll.isTouchingWall))
            {
                NormalJump();
            }
            else        // if player attemp to jump too early before char hit the ground 
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && coll.isTouchingWall)       // check if the char moving next to a wall
        {
            if (!coll.isGrounded && movementInputDirection != facingDirection)  // if char in the air and move to the opposite direction it facing
            {
                canMove = false;            // stop char movement           //Make it easier to mett the condition of wall jump
                canFlip = false;

                turnTimer = turnTimerSet;   // start timer
            }
        }

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;        // stop movement within this timer

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))    // after a normal jump check when the player let go if ths Jump key
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

		if (Input.GetButtonDown("Dash"))
		{
            if(Time.time >= (lastDash + dashCooldown))
                AttemptToDash();
		}


    }

    private void CheckIfCanJump()           // First check before starting any type of jump
    {
        if (coll.isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (coll.isTouchingWall)
        {
            checkJumpMultiplier = false;
            canWallJump = true;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }

    }

    private void CheckIfWallSliding()       // Check whether player currently wall sliding
    {
        if (coll.isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0 && !canClimbLedge)  // check y so char not sliding until it start to fall
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }


    private void CheckJump()
    {
        if (jumpTimer > 0)  // do a jump for the player (as soon as condition meet) if they press Jump too early before char land on ground
        {
            //WallJump
            if (!coll.isGrounded && coll.isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                
                WallJump();
            }
            else if (coll.isGrounded)
            {
                NormalJump();
            }
        }

        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (wallJumpTimer > 0)      // while leaving the wall
        {
            if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)      // after wall jump and attempt to move back to the same wall
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);     // cut y velocity to prevent climbing up using 1 wall ( back and forward wall jump)
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    private void CheckLedgeClimb()
    {
        if (coll.ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(coll.ledgePosBot.x + coll.wallCheckDistance) - lefgeClimbXOffset1, Mathf.Floor(coll.ledgePosBot.y) + lefgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(coll.ledgePosBot.x + coll.wallCheckDistance) + lefgeClimbXOffset2, Mathf.Floor(coll.ledgePosBot.y) + lefgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(coll.ledgePosBot.x - coll.wallCheckDistance) + lefgeClimbXOffset1, Mathf.Floor(coll.ledgePosBot.y) + lefgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(coll.ledgePosBot.x - coll.wallCheckDistance) - lefgeClimbXOffset2, Mathf.Floor(coll.ledgePosBot.y) + lefgeClimbYOffset2);
            }

            canMove = false;
            canFlip = false;

            anim.SetBool("canClimbLedge", canClimbLedge);
        }

		if (canClimbLedge)
		{
			transform.position = ledgePos1;
		}
	}

    private void AttemptToDash()
	{
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImgXpos = transform.position.x;
	}

    private void CheckDash()
	{
		if (isDashing)
		{
            if(dashTimeLeft > 0)
			{
                canMove = false;
                canFlip = false;

                rb.velocity = new Vector2(dashSpeed * facingDirection, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if(Mathf.Abs(transform.position.x - lastImgXpos) > distanceBetweenImgs)
			    {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImgXpos = transform.position.x;
			    }
			}

            if(dashTimeLeft <= 0 || coll.isTouchingWall)
			{
                isDashing = false;
                canMove = true;
                canFlip = true;
			}
		}
	}


    #region Jumps

    private void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            amountOfJumpsLeft--;
            jumpTimer = 0;

            isAttemptingToJump = false;     // not attemp early jump
            checkJumpMultiplier = true;     // attemp small jump
        }
    }

    private void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);     // remove y velocity
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);        // Add force of 45* angle
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);

            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;

            jumpTimer = 0;
            turnTimer = 0;
            wallJumpTimer = wallJumpTimerSet;   // start timer

            isWallSliding = false;
            isAttemptingToJump = false;     // not attemp early jump
            checkJumpMultiplier = true;     // attemp small jump
            canMove = true;                 // allow free movement
            canFlip = true;
            hasWallJumped = true;
            
            lastWallJumpDirection = -facingDirection;   // char face opposite of the wall while sliding

        }
    }

    #endregion

  
    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void ApplyMovement()
    {

        if (!coll.isGrounded && !isWallSliding && movementInputDirection == 0)          // If falling
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);        // restrict movement while falling
        }
        else if (canMove)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }


        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);      // drag the char down base on -wallSlideSpeed (minus for downward)
            }
        }
    }

    #region Animations
    private void Flip()
    {
        if (!isWallSliding && canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    public void FinishLedgeClimb()
	{
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        coll.ledgeDetected = false;
        anim.SetBool("canClimbLedge", canClimbLedge);
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", coll.isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

	#endregion

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(ledgePos1, ledgePos2);
	}
}