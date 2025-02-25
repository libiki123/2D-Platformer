﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
	protected StunState_SO stateData;

	protected bool isStunTimeOver;
	protected bool isGorunded;
	protected bool isMovementStopped;
	protected bool performCloseRangeAction;
	protected bool isPlayerInMinAgroRange;

	public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, StunState_SO stateData) : base(entity, stateMachine, animBoolName)
	{
		this.stateData = stateData;
	}

	public override void DoChecks()
	{
		base.DoChecks();

		isGorunded = entity.CheckGround();
		performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
		isPlayerInMinAgroRange = entity.CheckPlayerInMinArgoRange();
	}

	public override void Enter()
	{
		base.Enter();

		isStunTimeOver = false;
		isMovementStopped = false;

		entity.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);
	}

	public override void Exist()
	{
		base.Exist();

		entity.ResetStunResistance();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if(Time.time >= startTime + stateData.stunTime)
		{
			isStunTimeOver = true;
		}

		if(isGorunded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
		{
			isMovementStopped = true;
			entity.SetVelocity(0f);
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
