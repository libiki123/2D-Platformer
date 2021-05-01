using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
	public E1_PlayerDetectedState playerDetectedState { get; private set; }
	public E1_ChargeState chargeState { get; private set; }
	public E1_LookForPlayerState lookForPlayerState { get; private set; }
	public E1_MeleeAttackState meleeAttackState { get; private set; }
	public E1_StunState stunState { get; private set; }
	public E1_DeadState deadState { get; private set; }

    [SerializeField] private IdleState_SO idleStateData;
    [SerializeField] private MoveState_SO moveStateData;
    [SerializeField] private PlayerDetectedState_SO playerDetectedStateData;
	[SerializeField] private ChargeState_SO chargeStateData;
	[SerializeField] private LookForPlayerState_SO lookForPlayerStateData;
	[SerializeField] private MeleeAttackState_SO meleeAttackStateData;
	[SerializeField] private StunState_SO stunStateData;
	[SerializeField] private DeadState_SO deadStateData;

	[SerializeField] private Transform meleeAttackPos;

	public override void Start()
	{
		base.Start();

		moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
		idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
		playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
		chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
		lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
		meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPos, meleeAttackStateData, this);
		stunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);
		deadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);

		stateMachine.Initialize(moveState);

	}

	public override void Damage(AttackDetails attackDetails)
	{
		base.Damage(attackDetails);

		if (isStunned && stateMachine.currentState != stunState)
		{
			stateMachine.ChangeState(stunState);
		}
		else if (isDead)
		{
			stateMachine.ChangeState(deadState);
		}
	}

	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.DrawWireSphere(meleeAttackPos.position, meleeAttackStateData.attackRadius);
	}
}
