using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMiniboss : MiniBoss
{
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    float moveSpeed = 1.25f;
    [SerializeField]
    Bullet bullet;
    [SerializeField]
    float attackSpeed = 2;
    [SerializeField]
    int bulletAmound = 3;
    [SerializeField]
    float startAngle, endAngle;
    GameObject player;
    
    private Rigidbody2D rb;
    private Vector2 moveDir = Vector2.zero;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Shoot", 0f, attackSpeed);
    }

    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
	{
        moveDir = player.transform.position - transform.position;
        rb.position += moveDir.normalized * Time.deltaTime * moveSpeed;
    }

    void Shoot()
    {
        float angleStep = (endAngle - startAngle) / bulletAmound;

        Vector2 toPlayer = player.transform.position - this.transform.position;

        for (int i = (bulletAmound / 2) * -1; i <= (bulletAmound / 2); i++)
        {
            Vector2 bulletDirection = Quaternion.Euler(0, 0, angleStep * i) * toPlayer;
            Instantiate(bullet).init(bulletDirection, this.transform.position, Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg - 45f);

        }
    }
}
