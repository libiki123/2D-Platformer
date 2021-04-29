
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
	[SerializeField] private float acitveTime= 0.1f;
	private float timeActivated;
	private float alpha;
	[SerializeField] private float alphasSet = 0.8f;
	[SerializeField] private float alphaDecay = 0.85f;

	private Transform player;

	private SpriteRenderer sr;
	private SpriteRenderer playerSr;

	private Color color;

	private void OnEnable()
	{
		sr = GetComponent<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerSr = player.GetComponent<SpriteRenderer>();

		alpha = alphasSet;
		sr.sprite = playerSr.sprite;
		transform.position = player.position;
		transform.rotation = player.rotation;
		timeActivated = Time.time;
	}

	private void Update()
	{
		alpha -= alphaDecay * Time.deltaTime;
		color = new Color(1f, 1f, 1f, alpha);
		sr.color = color;

		if(Time.time >= (timeActivated + acitveTime))
		{
			PlayerAfterImagePool.Instance.AddToPool(gameObject);
		}
	}
}
