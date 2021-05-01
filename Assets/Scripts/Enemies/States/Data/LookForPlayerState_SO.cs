using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/Look For Player Data")]
public class LookForPlayerState_SO : ScriptableObject
{
    public int amountOfTurn = 2;
    public float timeBetweenTurns = 0.75f;
}
