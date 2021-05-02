using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_LookForPlayerState : LookForPlayerState
{
    private Enemy2 enemy;

	public E2_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, LookForPlayerState_SO stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
