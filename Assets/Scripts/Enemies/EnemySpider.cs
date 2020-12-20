using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : Enemy
{
    [SerializeField]
    float attackSpeed = 2;
    [SerializeField]
    float moveSpeed = 3;
    [SerializeField]
    int bulletAmound = 3;
    [SerializeField]
    float startAngle, endAngle;
    [SerializeField]
    Bullet bullet;

    float moveWhait;
    Vector2 moveDir;
    Rigidbody2D rb;
    GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Shoot", 0f, attackSpeed);

    }

    void Update()
	{
        if (moveWhait <= 0)
        {
            Move();
            moveWhait = Random.Range(1f, 3f);
        }
        moveWhait -= Time.deltaTime;
        rb.position += moveDir * Time.deltaTime * moveSpeed;
	}

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
        Move();
    }

    void Shoot()
	{
        float angleStep = (endAngle - startAngle) / bulletAmound;

        Vector2 toPlayer = player.transform.position - this.transform.position;

        for (int i = (bulletAmound / 2) * -1; i <= (bulletAmound / 2); i++)
		{
            Vector2 bulletDirection = Quaternion.Euler(0, 0, angleStep * i) * toPlayer;
            Instantiate(bullet).init(bulletDirection, this.transform.position, Random.value * 180f);
            
        }
	}

    void Move()
    {
        moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        moveDir.Normalize();
    }
}
