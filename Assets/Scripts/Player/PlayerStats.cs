using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float currentHealth;

    [Header("Stats")]
    [SerializeField] private float maxHealth;

    [Header("Particles")]
    [SerializeField] private Transform deathChunkParticle;
    [SerializeField] private Transform deathBloodParticle;

	private void Start()
	{
        currentHealth = maxHealth;
	}

    public void DecreaseHealth(float amount)
	{
        currentHealth -= amount;

        if(currentHealth <= 0.0f)
		{
            Die();
		}
	}

    private void Die()
	{
        Instantiate(deathChunkParticle, transform.position, transform.rotation);
        Instantiate(deathBloodParticle, transform.position, transform.rotation);
        GameManager.Instance.Respawn();
        Destroy(gameObject);
    }
}
