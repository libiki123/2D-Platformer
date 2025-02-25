﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MeleeAttackState : MeleeAttackState
{
	Enemy1 enemy;

	public E1_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPos, MeleeAttackState_SO stateData, Enemy1 enemy) 
		: base(entity, stateMachine, animBoolName, attackPos, stateData)
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

	public override void FinishAttack()
	{
		base.FinishAttack();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (isAnimationFinished)
		{
			if (isPlayerInMinAgroRange)
			{
				stateMachine.ChangeState(enemy.playerDetectedState);
			}
			else
			{
				stateMachine.ChangeState(enemy.lookForPlayerState);
			}
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void TriggerAttack()
	{
		base.TriggerAttack();
	}
}
