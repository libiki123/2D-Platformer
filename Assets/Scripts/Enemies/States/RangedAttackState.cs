using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
	protected RangedAttackState_SO stateData;
	protected GameObject projectile;
	protected Projectile projectileScript;

	public RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPos, RangedAttackState_SO stateData) : base(entity, stateMachine, animBoolName, attackPos)
	{
		this.stateData = stateData;
	}

	public override void DoChecks()
	{
		base.DoChecks();
	}

	public override void Enter()
	{
		base.Enter();
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

		projectile = GameObject.Instantiate(stateData.projectile, attackPos.position, attackPos.rotation);
		projectileScript = projectile.GetComponent<Projectile>();
		projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);
	}
}
