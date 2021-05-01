using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
	protected PlayerDetectedState_SO stateData;

	protected bool isDetectingLedge;
	protected bool isPlayerInMinAgroRange;
	protected bool isPlayerInMaxAgroRange;
	protected bool performLongRangeAction;
	protected bool performCloseRangeAction;

	public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedState_SO stateData) : base(entity, stateMachine, animBoolName)
	{
		this.stateData = stateData;
	}
	public override void DoChecks()
	{
		base.DoChecks();

		isDetectingLedge = entity.CheckLedge();
		isPlayerInMinAgroRange = entity.CheckPlayerInMinArgoRange();
		isPlayerInMaxAgroRange = entity.CheckPlayerInMaxArgoRange();

		performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
	}

	public override void Enter()
	{
		base.Enter();
		performLongRangeAction = false;
		performCloseRangeAction = false;
		entity.SetVelocity(0f);
	}

	public override void Exist()
	{
		base.Exist();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if(Time.time >= startTime + stateData.longRangeActionTime)
		{
			performLongRangeAction = true;
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();


	}
}
