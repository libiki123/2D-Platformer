using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead Data")]
public class DeadState_SO : ScriptableObject
{
	public GameObject deathChunkParticle;
	public GameObject deathBloodParticle;

}
