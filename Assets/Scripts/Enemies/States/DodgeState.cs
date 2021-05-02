using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
	protected DodgeState_SO stateData;

	protected bool performCloseRangeAction;
	protected bool isPlayerInMaxAgroRange;
	protected bool isGrounded;
	protected bool isDodgeOver;

	public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, DodgeState_SO stateData) : base(entity, stateMachine, animBoolName)
	{
		this.stateData = stateData;
	}

	public override void DoChecks()
	{
		base.DoChecks();

		performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
		isPlayerInMaxAgroRange = entity.CheckPlayerInMaxArgoRange();
		isGrounded = entity.CheckGround();

	}

	public override void Enter()
	{
		base.Enter();

		isDodgeOver = false;

		entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);
	}

	public override void Exist()
	{
		base.Exist();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if(Time.time >= startTime + stateData.dodgeTime && isGrounded)
		{
			isDodgeOver = true;
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
