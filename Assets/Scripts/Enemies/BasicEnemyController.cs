using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
	private enum State { Moving, Knockback, Dead }

	private State cuurentState;
	private Vector2 movement;


	private EnemyCollision coll;
	private GameObject alive;
	private Rigidbody2D rbAlive;
	private Animator aliveAnim;

	private int facingDirection;
	private int damageDirection;
	private float currentHealth;
	private float knockbackStartTime;
	private float lastTouchDamageTime;

	private AttackDetails attackDetails = new AttackDetails();

	[Header("Stats")]
	[SerializeField] private float movementSpeed;
	[SerializeField] private float maxHealth;

	[Header("Knockback")]
	[SerializeField] private float knockbackDuration;
	[SerializeField] private Vector2 knockbackSpeed;

	[Header("Particles")]
	[SerializeField] private GameObject hitParticle;
	[SerializeField] private GameObject deathChunkParticle;
	[SerializeField] private GameObject deathBloodParticle;

	[Header("Damage")]
	[SerializeField] private float touchDamage;
	[SerializeField] private float touchDamageCooldown;


	private void Start()
	{
		coll = GetComponent<EnemyCollision>();
		alive = transform.Find("Alive").gameObject;
		rbAlive = alive.GetComponent<Rigidbody2D>();
		aliveAnim = alive.GetComponent<Animator>();

		currentHealth = maxHealth;
		facingDirection = 1;
	}

	private void Update()
	{
		switch (cuurentState)
		{
			case State.Moving:
				UpdateMovingState();
				break;
			case State.Knockback:
				UpdateKnockbackState();
				break;
			case State.Dead:
				UpdateDeadState();
				break;
		}
	}

	#region States
	//-------------------------- Moving State --------------------------------//

	private void EnterMovingState()
	{

	}

	private void UpdateMovingState()
	{
		CheckTouchDamage();

		if (!coll.groundDetected || coll.wallDetected)
		{
			Flip();
		}
		else
		{
			movement.Set(movementSpeed * facingDirection, rbAlive.velocity.y);
			rbAlive.velocity = movement;
		}
	}

	private void ExistMovingState()
	{

	}

	//-------------------------- Knockback State --------------------------------//

	private void EnterKnockbackState()
	{
		knockbackStartTime = Time.time;
		movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
		rbAlive.velocity = movement;

		aliveAnim.SetBool("knockback", true);
	}

	private void UpdateKnockbackState()
	{
		if(Time.time > knockbackStartTime + knockbackDuration)
		{
			SwitchState(State.Moving);
		}
	}

	private void ExistKnockbackState()
	{
		aliveAnim.SetBool("knockback", false);
	}

	//-------------------------- Dead State --------------------------------//

	private void EnterDeadState()
	{
		Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
		Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);

		Destroy(gameObject);
	}

	private void UpdateDeadState()
	{

	}

	private void ExistDeadState()
	{

	}

	#endregion

	//-------------------------- Others --------------------------------//

	private void Damage(AttackDetails attackerDetails)
	{
		currentHealth -= attackerDetails.damageAmount;        // Attack damage is alway in the index [0]

		damageDirection = attackerDetails.position.x > alive.transform.position.x ? -1 : 1;        // If player X pos > enemy x pos 

		Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360f)));

		if(currentHealth > 0.0f)
		{
			SwitchState(State.Knockback);
		}
		else if( currentHealth <= 0.0f)
		{
			SwitchState(State.Dead);
		}

	}

	private void CheckTouchDamage()
	{
		if(Time.time >= lastTouchDamageTime + touchDamageCooldown)
		{
			Collider2D hit = coll.IsTouchingPLayer();
			if (hit != null )
			{
				lastTouchDamageTime = Time.time;
				attackDetails.damageAmount = touchDamage;
				attackDetails.position = alive.transform.position;
				hit.SendMessage("Damage", attackDetails);
			}

		}
	}

	private void Flip()
	{
		facingDirection = -facingDirection;
		alive.transform.Rotate(0.0f, 180f, 0.0f);
	}

	private void SwitchState(State state)
	{
		switch (cuurentState)
		{
			case State.Moving:
				ExistMovingState();
				break;
			case State.Knockback:
				ExistKnockbackState();
				break;
			case State.Dead:
				ExistDeadState();
				break;
		}

		switch (state)
		{
			case State.Moving:
				EnterMovingState();
				break;
			case State.Knockback:
				EnterKnockbackState();
				break;
			case State.Dead:
				EnterDeadState();
				break;
		}

		cuurentState = state;
	}
}
