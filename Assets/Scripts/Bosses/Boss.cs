using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
	[SerializeField]
	float moveSpeed = 1.25f;
	[SerializeField]
	float maxHealth;
	[SerializeField]
	float attackSpeedDefault = 2;
	[SerializeField]
	int bulletAmount = 10;
	[SerializeField]
	float startAngle = -90, endAngle = 90;
	[SerializeField]
	Bullet bullet;
	[SerializeField]
	float timeOffset;
	[SerializeField]
	float spreadAngle;

	float attackSpeed;
	float moveWhait;
	bool phase2started = true;
	bool phase2ended = true;

	Vector2 moveDir;
	Rigidbody2D rb;
	GameObject player;
	IEnumerator currentAttack;
	IEnumerator attackControl;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		attackSpeed = attackSpeedDefault;
		health = maxHealth;
	}

	void OnEnable()
	{
		print(CharacterController.instance);
		if (CharacterController.instance)
		{
			if (CharacterController.instance.spiderGemCollected)
			{
				moveSpeed *= 0.75f;
			}
			if (CharacterController.instance.goblinGemCollected)
			{
				attackSpeed *= 1.25f;
			}
			if (CharacterController.instance.gostGemCollected)
			{
				maxHealth *= 0.8f;
				health = maxHealth;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (health > maxHealth * 0.67)
		{
			if (attackSpeed <= 0)
			{
				Phase1Attack();
				attackSpeed = attackSpeedDefault;
			}
			if (moveWhait <= 0)
			{
				SetMovement();
				moveWhait = Random.Range(1f, 3f);
			}
			Move();
			moveWhait -= Time.deltaTime;
		}
		else if (health > maxHealth * 0.32)
		{
			
			if (phase2started)
			{
				timeOffset = 0.5f;
				bulletAmount = 4;
				spreadAngle = 20;

				attackControl = attack();
				StartCoroutine(attackControl);
				phase2started = false;
			}
			
			if (moveWhait <= 0)
			{
				SetMovement();
				moveWhait = Random.Range(1f, 3f);
			}
			Move();
			moveWhait -= Time.deltaTime;
		}
		else
		{
			if (phase2ended)
			{
				StopCoroutine(currentAttack);
				StopCoroutine(attackControl);
				phase2ended = false;
			}

			if (attackSpeed <= 0)
			{
				moveSpeed = 0.75f;
				bulletAmount = 3;
				startAngle = -22.5f;
				endAngle = 22.5f;

				Phase1Attack();
				attackSpeed = attackSpeedDefault;
			}
			MoveToPlayer();
		}
		attackSpeed -= Time.deltaTime;
	}

	void SetMovement()
	{
		moveDir = new Vector2(Random.Range(-1f, 1f), 0f);
		moveDir.Normalize();
	}

	void Move()
	{
		rb.position += moveDir * Time.deltaTime * moveSpeed;
	}

	void MoveToPlayer()
	{
		moveDir = player.transform.position - transform.position;
		rb.position += moveDir.normalized * Time.deltaTime * moveSpeed;
	}

	void Phase1Attack()
	{
		float angleStep = (endAngle - startAngle) / bulletAmount;

		Vector2 toPlayer = player.transform.position - this.transform.position;

		for (int i = (bulletAmount / 2) * -1; i <= (bulletAmount / 2); i++)
		{
			Vector2 bulletDirection = Quaternion.Euler(0, 0, angleStep * i) * toPlayer;
			Instantiate(bullet).init(bulletDirection, this.transform.position, Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg);

		}
	}
	IEnumerator attack()
	{
		while (true)
		{
			currentAttack = BulletPatterns.timedSpread(bullet, bulletAmount, spreadAngle, transform, 0.5f, timeOffset);
			StartCoroutine(currentAttack);
			yield return new WaitForSeconds(timeOffset * (bulletAmount * 2 - 1));
		}
	}
}
