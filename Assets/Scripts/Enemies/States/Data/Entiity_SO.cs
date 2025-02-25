﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName ="Data/Entity Data/Base Data")]
public class Entiity_SO : ScriptableObject
{
    public float maxHealth = 30f;

    public float damageHopVelocity = 10f;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;

    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;

    public float closeRangeActionDistance = 1f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public GameObject hitParticle;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
