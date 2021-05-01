using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
	protected MoveState_SO stateData;

	protected bool isDetectingWall;
	protected bool isDetectingLedge;
	protected bool isPlayerInMinAgroRange;
	protected bool isPlayerInMaxAgroRange;

	public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, MoveState_SO stateData) : base(entity, stateMachine, animBoolName)
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
		entity.SetVelocity(stateData.movementSpeed);

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


}
