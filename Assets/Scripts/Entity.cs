using UnityEngine;

public class Entity : MonoBehaviour
{
	[SerializeField]
	internal float health;

	internal virtual void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Die();
		}
	}

	internal virtual void Die()
	{
		Destroy(gameObject);
	}
}
