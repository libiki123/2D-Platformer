using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack Data")]
public class RangedAttackState_SO : ScriptableObject
{
	public GameObject projectile;
	public float projectileDamage = 10f;
	public float projectileSpeed = 12f;
	public float projectileTravelDistance = 5f;
}
