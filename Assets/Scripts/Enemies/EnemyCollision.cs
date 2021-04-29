using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask whatIsGround;

    [Header("Collision")]
    public Transform groundCheck;
    public Transform wallCheck;

    [Header("Stats")]
    public float groundCheckDistance;
    public float wallCheckDistance;

    [Space]
    [HideInInspector] public bool groundDetected;
    [HideInInspector] public bool wallDetected;


    // Update is called once per frame
    void Update()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
