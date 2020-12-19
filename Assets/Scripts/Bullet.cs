using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	float Damage = 25;

	[SerializeField]
	string tagShooter;

	[SerializeField]
	float speed = 4;

	[SerializeField]
	Rigidbody2D rbBullet;

	bool initialised = false;

	Vector2 direction;

	

	// Start is called before the first frame update
	void Start()
	{
		rbBullet = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!initialised) return;
		transform.position += (Vector3)direction * Time.deltaTime * speed;
	}

	public void init(Vector2 direction, Vector2 position, float angle)
	{
		if (initialised) return;
		initialised = !initialised;
		rbBullet.rotation = angle;
		this.direction = direction.normalized;
		transform.position = position;
	}

	public void OnCollisionEnter2D(Collision2D collision)
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
