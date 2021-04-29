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


	private bool gotInput;
	private bool isAttacking;
	private bool isFirstAttack;			
	private float lastInputTime = Mathf.NegativeInfinity;
	private float[] attackDetail = new float[2];

	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
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

		attackDetail[0] = attack1Damage;
		attackDetail[1] = transform.position.x;

		foreach (Collider2D collier in detectedObjects)
		{
			collier.transform.parent.SendMessage("Damage", attackDetail);
		}
	}

	private void FinishAttack1()
	{
		isAttacking = false;
		anim.SetBool("isAttacking", isAttacking);
		anim.SetBool("attack1", false);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(attack1HitboPos.position, attack1Radius);
	}
}
