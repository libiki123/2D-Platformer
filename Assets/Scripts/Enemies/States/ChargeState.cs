using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
	ChargeState_SO stateData;

	protected bool isPlayerInMinAgroRange;
	protected bool isDetectingLedge;
	protected bool isDetectingWall;
	protected bool isChargeTimeOver;
	protected bool performCloseRangeAction;

	public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, ChargeState_SO stateData) : base(entity, stateMachine, animBoolName)
	{
		this.stateData = stateData;
	}
	public override void DoChecks()
	{
		base.DoChecks();

		isDetectingLedge = entity.CheckLedge();
		isDetectingWall = entity.CheckWall();
		isPlayerInMinAgroRange = entity.CheckPlayerInMinArgoRange();
	}

	public override void Enter()
	{
		base.Enter();

		isChargeTimeOver = false;
		entity.SetVelocity(stateData.chargeSpeed);
	}

	public override void Exist()
	{
		base.Exist();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if(Time.time >= startTime + stateData.chargeTime)
		{
			isChargeTimeOver = true;
		}

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();

	}
}
