﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }
	public E2_PlayerDetectedState playerDetectedState { get; private set; }
	public E2_MeleeAttackState meleeAttackState { get; private set; }
	public E2_LookForPlayerState lookForPlayerState { get; private set; }
	public E2_StunState stunState { get; private set; }
	public E2_DeadState deadState { get; private set; }
	public E2_DodgeState dodgeState { get; private set; }
	public E2_RangedAttackState rangedAttackState { get; private set; }

    [SerializeField] private MoveState_SO moveStateData;
    [SerializeField] private IdleState_SO idleStateData;
    [SerializeField] private PlayerDetectedState_SO playerDetectedStateData;
    [SerializeField] private MeleeAttackState_SO meleeAttackStateData;
	[SerializeField] private LookForPlayerState_SO lookForPlayerStateData;
	[SerializeField] private StunState_SO stunData;
	[SerializeField] private DeadState_SO deadData;
	public DodgeState_SO dodgeData;
	[SerializeField] private RangedAttackState_SO rangedAttackData;

	[SerializeField] private Transform meleeAttackPos;
	[SerializeField] private Transform rangedAttackPos;

	public override void Start()
	{
		base.Start();

		moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
		idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
		playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
		meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPos, meleeAttackStateData, this);
		lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
		stunState = new E2_StunState(this, stateMachine, "stun", stunData, this);
		deadState = new E2_DeadState(this, stateMachine, "dead", deadData, this);
		dodgeState = new E2_DodgeState(this, stateMachine, "dodge", dodgeData, this);
		rangedAttackState = new E2_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPos, rangedAttackData, this);

		stateMachine.Initialize(moveState);
	}

	public override void Damage(AttackDetails attackDetails)
	{
		base.Damage(attackDetails);

		if(isStunned && stateMachine.currentState != stunState)
		{
			stateMachine.ChangeState(stunState);
		}
		else if (isDead)
		{
			stateMachine.ChangeState(deadState);
		}
		else if (CheckPlayerInMinArgoRange())
		{
			stateMachine.ChangeState(rangedAttackState);
		}
		else if (!CheckPlayerInMinArgoRange())
		{
			lookForPlayerState.SetTurnImmediately(true);
			stateMachine.ChangeState(lookForPlayerState);
		}
	}

	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.DrawWireSphere(meleeAttackPos.position, meleeAttackStateData.attackRadius);
	}
}
