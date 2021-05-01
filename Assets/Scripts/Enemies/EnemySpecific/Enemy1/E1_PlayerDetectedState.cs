using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
	private Enemy1 enemy;

	public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedState_SO playerDetectedData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, playerDetectedData)
	{
		this.enemy = enemy;
	}

	public override void Enter()
	{
		base.Enter();

		entity.SetVelocity(0f);
	}

	public override void Exist()
	{
		base.Exist();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (performCloseRangeAction)
		{
			stateMachine.ChangeState(enemy.meleeAttackState);
		}
		else if (performLongRangeAction)
		{
			stateMachine.ChangeState(enemy.chargeState);
		}
		else if (!isPlayerInMaxAgroRange)
		{
			stateMachine.ChangeState(enemy.lookForPlayerState);
		}
		else if (!isDetectingLedge)
		{
			enemy.Flip();
			stateMachine.ChangeState(enemy.moveState);
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
