using UnityEngine;

public class Enemy : Entity
{
	[SerializeField]
	internal GameObject healthpickup;
	[SerializeField]
	internal float touchDamage;
	[SerializeField]
	internal float touchDamageCooldownDefault = 1;

	private float touchDamageCooldown;

	protected virtual void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && touchDamageCooldown <= 0)
		{
			print("e");
			PlayerController.instance.TakeDamage(touchDamage);
			touchDamageCooldown = touchDamageCooldownDefault;
		}
		else
		{
			touchDamageCooldown -= Time.deltaTime;
		}
	}

	protected virtual void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			touchDamageCooldown = 0;
		}
	}

	internal override void Die()
	{
		Destroy(gameObject);
		if (healthpickup &&
			PlayerController.instance.health <= PlayerController.instance.maxHealth * 0.5 &&
			GameObject.FindGameObjectsWithTag("Healthpickup").Length < 2)
			Instantiate(healthpickup, this.transform.position, Quaternion.identity);
		Room.Current.CheckEnemies();
	}
}
