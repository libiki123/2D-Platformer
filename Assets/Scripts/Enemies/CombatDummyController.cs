using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
	[Header("Stats")]
	[SerializeField] private float maxHealth;

	[Header("Knockback")]
	[SerializeField] private bool applyKnockback;

	[Space]
	[SerializeField] private float knockbackDuration;
	[SerializeField] private float knockbackSpeedX, knockbackSpeedY;
	[SerializeField] private float knockbackDeathSpeedX, knockbackDeathSpeedY;
	[SerializeField] private float deathTorque;         // use to make top part spin

	[Header("Hit Particle")]
	[SerializeField] private GameObject hitParticle;

	private float currentHealth;
	private float knockbackStart;

	private int playerFacingDicrection;
	private bool playerOnLeft;
	private bool knockback;


	private PlayerController pc;
	private GameObject aliveGO, brokenTopGO, brokenBotGO;
	private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
	private Animator aliveAnim;

	private void Start()
	{
		currentHealth = maxHealth;

		pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		aliveGO = transform.Find("Alive").gameObject;
		brokenTopGO = transform.Find("BrokenTop").gameObject;
		brokenBotGO = transform.Find("BrokenBot").gameObject;

		rbAlive = aliveGO.GetComponent<Rigidbody2D>();
		rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
		rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

		aliveAnim = aliveGO.GetComponent<Animator>();

		aliveGO.SetActive(true);
		brokenTopGO.SetActive(false);
		brokenBotGO.SetActive(false);
	}

	private void Update()
	{
		CheckKnockback();
	}

	private void Damage(AttackDetails attackerDetail)
	{
		currentHealth -= attackerDetail.damageAmount;
		playerFacingDicrection = attackerDetail.position.x < aliveGO.transform.position.x ? 1: -1;

		Instantiate(hitParticle, aliveAnim.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
		playerOnLeft = playerFacingDicrection == 1 ?  true : false;     // check whether player on left or right

		aliveAnim.SetBool("playerOnLeft", playerOnLeft);
		aliveAnim.SetTrigger("damage");

		if(applyKnockback && currentHealth > 0.0f)
		{
			Knockback();
		}

		if(currentHealth <= 0.0f)
		{
			Die();
		}
	}

	private void Knockback()
	{
		knockback = true;
		knockbackStart = Time.time;
		rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDicrection, knockbackSpeedY);

	}

	private void CheckKnockback()
	{
		if(Time.time >= knockbackStart + knockbackDuration && knockback)
		{
			knockback = false;
			rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
		}
	}

	private void Die()
	{
		aliveGO.SetActive(false);
		brokenTopGO.SetActive(true);
		brokenBotGO.SetActive(true);

		brokenTopGO.transform.position = aliveGO.transform.position;		// make sure everything aline
		brokenBotGO.transform.position = aliveGO.transform.position;

		rbBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDicrection, knockbackSpeedY);
		rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDicrection, knockbackDeathSpeedY);
		rbBrokenTop.AddTorque(deathTorque * -playerFacingDicrection, ForceMode2D.Impulse);
	}
}
