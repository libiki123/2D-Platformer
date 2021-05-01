using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{
	protected LookForPlayerState_SO stateData;

	protected bool turnImmediately;
	protected bool isPlayerInMinAgroRange;
	protected bool isAllTurnDone;
	protected bool isAllTurnTimeDone;

	protected float lastTurnTime;

	protected int amountOfTurnDone;

	public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, LookForPlayerState_SO stateData) : base(entity, stateMachine, animBoolName)
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

		isAllTurnDone = false;
		isAllTurnTimeDone = false;

		lastTurnTime = startTime;
		amountOfTurnDone = 0;

		entity.SetVelocity(0f);

	}

	public override void Exist()
	{
		base.Exist();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (turnImmediately)
		{
			entity.Flip();
			lastTurnTime = Time.time;
			amountOfTurnDone++;
			turnImmediately = false;
		}
		else if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnDone)		// it time to turn and there still turns left
		{
			entity.Flip();
			lastTurnTime = Time.time;
			amountOfTurnDone++;
		}

		if(amountOfTurnDone >= stateData.amountOfTurn)
		{
			isAllTurnDone = true;
		}

		if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnDone)
		{
			isAllTurnTimeDone = true;
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public void SetTurnImmediately(bool flip)
	{
		turnImmediately = flip;
	}
}
