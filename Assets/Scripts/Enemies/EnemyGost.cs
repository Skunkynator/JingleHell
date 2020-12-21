using UnityEngine;

public class EnemyGost : Enemy
{
	[SerializeField]
	internal float moveSpeed = 3;

	private float attackCooldown;
	private GameObject player;
	private Rigidbody2D rb;
	private Vector2 moveDir = Vector2.zero;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		if (attackCooldown <= 0)
		{
			moveDir = player.transform.position - transform.position;
		}
		Move(moveDir.normalized);
		attackCooldown -= Time.deltaTime;
	}

	internal void Move(Vector2 moveDir)
	{
		rb.position += moveDir * Time.deltaTime * moveSpeed;
	}

	internal void OnCollisionEnter2D(Collision2D collision)
	{
		base.OnCollisionStay2D(collision);

		if (collision.gameObject.tag == "Player")
		{
			moveDir = new Vector2(moveDir.x * -10 * moveSpeed, moveDir.y * -10 * moveSpeed);
			Move(moveDir.normalized);
		}

		attackCooldown = base.touchDamageCooldownDefault;
	}
}
