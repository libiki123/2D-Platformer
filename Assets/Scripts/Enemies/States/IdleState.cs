using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : State
{
	protected IdleState_SO stateData;

	protected bool flipAfterIdle;
	protected bool isIdleTimeOver;
	protected bool isPlayerInMinAgroRange;

	protected float idleTIme;

	public IdelState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, IdleState_SO stateData) : base(entity, stateMachine, animBoolName)
	{
		this.stateData = stateData;
	}

	public override void DoChecks()
	{
		base.DoChecks();

		isPlayerInMinAgroRange = entity.CheckPlayerInMinArgoRange();
	}

	public override void Enter()
	{
		base.Enter();
		entity.SetVelocity(0f);
		isIdleTimeOver = false;
		
		SetRandomIdleTime();
	}

	public override void Exist()
	{
		base.Exist();
		if (flipAfterIdle)
			entity.Flip();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (Time.time >= startTime + idleTIme)
			isIdleTimeOver = true;
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public void SetFlipAfterIdle(bool flip)
	{
		flipAfterIdle = flip;
	}

	public void SetRandomIdleTime()
	{
		idleTIme = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);

	}
}
