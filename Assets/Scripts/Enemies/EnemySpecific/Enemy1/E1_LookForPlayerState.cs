using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_LookForPlayerState : LookForPlayerState
{
	Enemy1 enemy;
	public E1_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, LookForPlayerState_SO stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
	{
		this.enemy = enemy;
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

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (isPlayerInMinAgroRange)
		{
			stateMachine.ChangeState(enemy.playerDetectedState);
		}
		else if (isAllTurnTimeDone)
		{
			stateMachine.ChangeState(enemy.moveState);
		}

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
