﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "Data/State Data/Dodge Data")]
public class DodgeState_SO : ScriptableObject
{
	public float dodgeSpeed = 10f;
	public float dodgeTime = 0.2f;
	public float dodgeCooldown = 2f;
	public Vector2 dodgeAngle = new Vector2(1,2);
}
