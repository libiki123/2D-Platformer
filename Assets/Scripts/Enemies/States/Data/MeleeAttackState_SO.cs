﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack Data")]
public class MeleeAttackState_SO : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 10f;
}
