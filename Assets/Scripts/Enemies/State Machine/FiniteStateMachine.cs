﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
	public State currentState { get; private set; }

	public void Initialize(State startingState)
	{
		currentState = startingState;
		currentState.Enter();
	}

	public void ChangeState(State newState)
	{
		currentState.Exist();
		currentState = newState;
		currentState.Enter();
	}

}
