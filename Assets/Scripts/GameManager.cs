using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
	[Header("Prefabs")]
	[SerializeField] private GameObject player;         // player prefab

	[Header("Respawn")]
	[SerializeField] private Transform respawnPoint;
	[SerializeField] private float respawnTime;

	public static GameManager Instance;

	private float respawnTimeStart;
	private bool respawn;

	private CinemachineVirtualCamera cvc;

	private void Start()
	{
		Instance = this;
		cvc = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
	}

	private void Update()
	{
		CheckRespawn();
	}

	public void Respawn()
	{
		respawnTimeStart = Time.time;
		respawn = true;
	}

	private void CheckRespawn()
	{
		if(Time.time >= respawnTimeStart + respawnTime && respawn)
		{
			var playerTemp = Instantiate(player, respawnPoint);
			cvc.m_Follow = playerTemp.transform;
			respawn = false;
		}
	}
}
