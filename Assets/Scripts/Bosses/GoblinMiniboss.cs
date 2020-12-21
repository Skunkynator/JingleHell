using UnityEngine;

public class GoblinMiniboss : MiniBoss
{
	[SerializeField]
	internal LayerMask mask;
	[SerializeField]
	internal float moveSpeed = 1.25f;
	[SerializeField]
	internal Bullet bullet;
	[SerializeField]
	internal float attackSpeed = 2;
	[SerializeField]
	internal int bulletAmound = 3;
	[SerializeField]
	internal float startAngle, endAngle;

	private GameObject player;
	private Rigidbody2D rb;
	private Vector2 moveDir = Vector2.zero;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating("Shoot", 0f, attackSpeed);
	}

	private void Update()
	{
		MoveToPlayer();
	}

	internal void MoveToPlayer()
	{
		moveDir = player.transform.position - transform.position;
		rb.position += moveDir.normalized * Time.deltaTime * moveSpeed;
	}

	internal void Shoot()
	{
		float angleStep = (endAngle - startAngle) / bulletAmound;

		Vector2 toPlayer = player.transform.position - this.transform.position;

		for (int i = (bulletAmound / 2) * -1; i <= (bulletAmound / 2); i++)
		{
			Vector2 bulletDirection = Quaternion.Euler(0, 0, angleStep * i) * toPlayer;
			Instantiate(bullet).Init(bulletDirection, this.transform.position, Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg - 45f);

		}
	}
}
