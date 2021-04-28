using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask whatIsGround;

    [Space]
    [Header("Collision")]
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform ledgeCheck;

    [Space]
    [Header("Collision")]
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float ledgeCheckDistance;

    [Space]
    [HideInInspector] public Vector2 ledgePosBot;

    [Space]
    public bool isGrounded;
    public bool isTouchingWall;
    public bool isTouchingLedge;
    public bool ledgeDetected;


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

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
