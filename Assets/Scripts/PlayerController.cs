using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Entity
{
	public static PlayerController instance;

	[SerializeField]
	internal float maxHealth;
	[SerializeField]
	internal float speed = 4;
	[SerializeField]
	internal Bullet bullet;
	[SerializeField]
	internal Rigidbody2D firingPoint;
	[SerializeField]
	internal Healthbar healthbar;
	[SerializeField]
	internal float cooldownDefault = 0;

	internal GameMasterScript GameMaster => GameMasterScript.instance;
	internal bool spiderGemCollected = false;
	internal bool goblinGemCollected = false;
	internal bool gostGemCollected = false;
	internal bool pausePressed = false;
	internal Rigidbody2D rb;

	private UnityAction updateMovementVector;
	private float cooldown = 0;
	private Vector2 movement;

	private void Awake()
	{
		instance = this;
		health = maxHealth;
		healthbar.SetMaxHealt(maxHealth);
		rb = GetComponent<Rigidbody2D>();
		updateMovementVector += CheckMoveInput;
	}

	internal void Update()
	{
		updateMovementVector?.Invoke();
		Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

		if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !pausePressed)
		{
			GameMaster.TogglePauseMenu();
			pausePressed = true;
		}
		if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape))
		{
			pausePressed = false;
		}

		rb.position += movement * Time.deltaTime * speed;
		firingPoint.rotation = angle;
		firingPoint.position = rb.position;

		if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && cooldown <= 0 && !GameMaster.paused)
			ShootBullet(direction, angle);
		cooldown -= Time.deltaTime;
	}

	internal void CheckMoveInput()
	{
		float hControl = Input.GetAxis("Horizontal");
		float vControl = Input.GetAxis("Vertical");
		movement = new Vector2(hControl, vControl);
	}

	internal void AutoMove(float time)
	{
		StartCoroutine(DisableInput(time));
	}

	internal void ShootBullet(Vector3 direction, float angle)
	{
		if (cooldown <= 0)
		{
			Instantiate(bullet).Init(direction, firingPoint.transform.position + direction.normalized, angle);
			cooldown = cooldownDefault;
		}
		else
		{
			cooldown -= Time.deltaTime;
		}
	}

	internal void PickUpHealthpack(GameObject healthpickup)
	{
		if (health >= maxHealth) { return; }

		health += Mathf.Clamp(healthpickup.GetComponent<Healthpickup>().restoreHealth, 0, maxHealth - health);
		healthbar.SetHealth(health);
		Destroy(healthpickup);
	}

	internal void OnCollisionEnter2D(Collision2D collsion)
	{
		if (collsion.gameObject.tag == "SpiderGem" ||
			collsion.gameObject.tag == "GostGem" ||
			collsion.gameObject.tag == "GoblinGem")
		{
			switch (collsion.gameObject.tag)
			{
				case "SpiderGem":
					spiderGemCollected = true;
					break;
				case "GostGem":
					gostGemCollected = true;
					break;
				case "GoblinGem":
					goblinGemCollected = true;
					break;
			}

			health = maxHealth;
			healthbar.SetHealth(health);
			Destroy(collsion.collider.gameObject);
		}
	}

	internal override void TakeDamage(float damage)
	{
		print("Hit");
		health -= damage;
		if (health <= 0)
		{
			Die();
		}
		healthbar.SetHealth(health);
	}

	internal override void Die()
	{
		GameMaster.GameOver();
	}

	private IEnumerator DisableInput(float time)
	{
		updateMovementVector -= CheckMoveInput;
		movement = movement.normalized;
		yield return new WaitForSeconds(time);
		updateMovementVector += CheckMoveInput;
	}

}
