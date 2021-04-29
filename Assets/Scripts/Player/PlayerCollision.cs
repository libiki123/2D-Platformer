using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask whatIsGround;

    [Header("Collision")]
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform ledgeCheck;

    [Header("Stats")]
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float ledgeCheckDistance;

    [Space]
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isTouchingWall;
    [HideInInspector] public bool isTouchingLedge;
    [HideInInspector] public bool ledgeDetected;
    [HideInInspector] public Vector2 ledgePosBot;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, ledgeCheckDistance, whatIsGround);

        if(isTouchingWall && !isTouchingLedge && !ledgeDetected)
		{
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
		}
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
