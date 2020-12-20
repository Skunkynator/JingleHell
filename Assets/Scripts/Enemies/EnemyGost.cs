using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGost : Enemy
{
    [SerializeField]
    private float moveSpeed = 3;
    [SerializeField]

    private float attackCooldown;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 moveDir = Vector2.zero;

    void Start()
	{
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
	{
        if (attackCooldown <= 0) 
        {
            moveDir = player.transform.position - transform.position;
        }
        Move(moveDir.normalized);
        attackCooldown -= Time.deltaTime;

    }

    void Move(Vector2 moveDir)
    {
        rb.position += moveDir * Time.deltaTime * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);

        if (collision.gameObject.tag == "Player")
        {
            moveDir = new Vector2(moveDir.x * -10 * moveSpeed, moveDir.y * -10 * moveSpeed);
            Move(moveDir.normalized);
        }

        attackCooldown = base.touchDamageCooldownDefault;

    }
}
