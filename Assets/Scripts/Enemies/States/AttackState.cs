using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPos;

	protected bool isAnimationFinished;
	protected bool isPlayerInMinAgroRange;

	public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPos) : base(entity, stateMachine, animBoolName)
	{
		this.attackPos = attackPos;
	}

	public override void DoChecks()
	{
		base.DoChecks();

		isPlayerInMinAgroRange = entity.CheckPlayerInMinArgoRange();
	}

	public override void Enter()
	{
		base.Enter();

		entity.atsm.attackState = this;
		isAnimationFinished = false;

		entity.SetVelocity(0f);
	}

	public override void Exist()
	{
		base.Exist();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public virtual void TriggerAttack()
	{

	}

	public virtual void FinishAttack()
	{
		isAnimationFinished = true;
	}
}
