using UnityEngine;

public class SpiderMiniBoss : MiniBoss
{
	[SerializeField]
	internal float attackSpeed = 2;
	[SerializeField]
	internal float moveSpeed = 1.5f;
	[SerializeField]
	internal int bulletAmound = 10;
	[SerializeField]
	internal float startAngle = -90, endAngle = 90;
	[SerializeField]
	internal Bullet bullet;

	internal float moveWhait;
	internal Vector2 moveDir;
	internal Rigidbody2D rb;
	internal GameObject player;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating("Shoot", 0f, attackSpeed);
	}

	private void Update()
	{
		if (moveWhait <= 0)
		{
			Move();
			moveWhait = Random.Range(1f, 3f);
		}
		moveWhait -= Time.deltaTime;
		rb.position += moveDir * Time.deltaTime * moveSpeed;
	}

	internal void Move()
	{
		moveDir = new Vector2(Random.Range(-1f, 1f), 0f);
		moveDir.Normalize();
	}

	internal void Shoot()
	{
		float angleStep = (endAngle - startAngle) / bulletAmound;

		Vector2 toPlayer = player.transform.position - this.transform.position;

		for (int i = (bulletAmound / 2) * -1; i <= (bulletAmound / 2); i++)
		{
			Vector2 bulletDirection = Quaternion.Euler(0, 0, angleStep * i) * toPlayer;
			Instantiate(bullet).Init(bulletDirection, this.transform.position, Random.value * 180f);

		}
	}

	protected override void OnCollisionStay2D(Collision2D collision)
	{
		base.OnCollisionStay2D(collision);
		Move();
	}
}
