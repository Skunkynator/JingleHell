using UnityEngine;
public class Bullet : MonoBehaviour
{
	[SerializeField]
	internal float Damage = 25;
	[SerializeField]
	internal string tagShooter;
	[SerializeField]
	internal float speed = 4;
	[SerializeField]
	internal Rigidbody2D rbBullet;

	internal Vector2 direction;
	internal bool initialised = false;

	internal void Start()
	{
		rbBullet = GetComponent<Rigidbody2D>();
	}

	internal void Update()
	{
		if (!initialised) return;
		transform.position += (Vector3)direction * Time.deltaTime * speed;
	}

	internal void Init(Vector2 direction, Vector2 position, float rotation)
	{
		if (initialised) return;
		initialised = !initialised;
		rbBullet.rotation = rotation;
		this.direction = direction.normalized;
		transform.position = position;
	}

	protected void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag != tagShooter)
		{
			Entity entity = collision.collider.GetComponent<Entity>();

			if (entity != null)
			{
				entity.TakeDamage(Damage);
			}
			Destroy(this.gameObject);
		}
	}
}
