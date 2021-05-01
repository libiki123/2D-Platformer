using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
	protected MeleeAttackState_SO stateData;
	protected AttackDetails attackDetails;
	public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPos, MeleeAttackState_SO stateData)
		: base(entity, stateMachine, animBoolName, attackPos)
	{
		this.stateData = stateData;
	}

	public override void Enter()
	{
		base.Enter();

		attackDetails.damageAmount = stateData.attackDamage;
		attackDetails.position = entity.aliveGO.transform.position;
	}


	public override void Exist()
	{
		base.Exist();
	}

	public override void FinishAttack()
	{
		base.FinishAttack();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void TriggerAttack()
	{
		base.TriggerAttack();

		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPos.position, stateData.attackRadius, entity.entityData.whatIsPlayer);

		foreach (Collider2D collider in detectedObjects)
		{
			collider.transform.SendMessage("Damage", attackDetails);
		}
	}
}
