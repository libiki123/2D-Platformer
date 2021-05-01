using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
	[Header("Comabat")]
    [SerializeField] private bool combatEnabled;        // whether we want to have combat enable
	[SerializeField] private float inputTimer;      // how long we holding the input

	[Header("Layers")]
	[SerializeField] private LayerMask whatIsDamageable;

	[Header("Stats")]
	[SerializeField] private Transform attack1HitboPos;
	[SerializeField] private float attack1Radius;
	[SerializeField] private float attack1Damage;
	[SerializeField] private float stunDamage;


	private bool gotInput;
	private bool isAttacking;
	private bool isFirstAttack;			
	private float lastInputTime = Mathf.NegativeInfinity;
	private AttackDetails attackDetails = new AttackDetails();

	private Animator anim;
	private PlayerController pc;
	private PlayerStats ps;

	private void Start()
	{
		anim = GetComponent<Animator>();
		pc = GetComponent<PlayerController>();
		ps = GetComponent<PlayerStats>();

		anim.SetBool("canAttack", combatEnabled);
	}

	private void Update()
	{
		CheckCombatInput();
		CheckAttacks();
	}

	private void CheckCombatInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (combatEnabled)
			{
				gotInput = true;
				lastInputTime = Time.time;
			}
		}
	}

	private void CheckAttacks()
	{
		if (gotInput)
		{
			if (!isAttacking)
			{
				gotInput = false;
				isAttacking = true;
				isFirstAttack = !isFirstAttack;     // use to alternate between 2 attack animation
				anim.SetBool("attack1", true);
				anim.SetBool("firstAttack", isFirstAttack);
				anim.SetBool("isAttacking", isAttacking);
			}
		}

		if(Time.time >= lastInputTime + inputTimer)
		{
			gotInput = false;
		}
	}

	private void CheckAttackHitBox()
	{
		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitboPos.position, attack1Radius, whatIsDamageable);

		attackDetails.damageAmount = attack1Damage;
		attackDetails.position = transform.position;
		attackDetails.stunDamageAmount = stunDamage;

		foreach (Collider2D collier in detectedObjects)
		{
			collier.transform.parent.SendMessage("Damage", attackDetails);
		}
	}

	private void FinishAttack1()
	{
		isAttacking = false;
		anim.SetBool("isAttacking", isAttacking);
		anim.SetBool("attack1", false);
	}

	private void Damage(AttackDetails attackerDetails) 
	{
		if (!pc.GetDashStatus())
		{
			int direction = attackerDetails.position.x < transform.position.x ? 1 : -1;

			ps.DecreaseHealth(attackerDetails.damageAmount);
			pc.Knockback(direction);

		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(attack1HitboPos.position, attack1Radius);
	}
}

