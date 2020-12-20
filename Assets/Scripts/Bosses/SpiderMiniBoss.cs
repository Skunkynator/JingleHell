using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMiniBoss : MiniBoss
{
    // Start is called before the first frame update

    [SerializeField]
    float attackSpeed = 2;
    [SerializeField]
    float moveSpeed = 1.5f;
    [SerializeField]
    int bulletAmound = 10;
    [SerializeField]
    float startAngle = -90, endAngle = 90;
    [SerializeField]
    Bullet bullet;

    float moveWhait;
    Vector2 moveDir;
    Rigidbody2D rb;
    GameObject player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Shoot", 0f, attackSpeed);
    }

    // Update is called once per frame
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
        moveDir = new Vector2(Random.Range(-1f, 1f), 0f);
        moveDir.Normalize();
    }

}
