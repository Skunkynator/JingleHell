using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGost : Enemy
{
    [SerializeField]
    private float moveSpeed = 3;
    [SerializeField]
    private float attackCooldownDefault = 1;
    private float attackCooldown;
    [SerializeField]
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 moveDir = Vector2.zero;

    void Awake()
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
        attackCooldown -= Time.deltaTime;
        Move(moveDir.normalized);
	}

    void Move(Vector2 moveDir)
    {
        rb.position += moveDir * Time.deltaTime * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackCooldown = attackCooldownDefault;
            CharacterController.instance.TakeDamage(25);
            moveDir = new Vector2(moveDir.x * -10 * moveSpeed, moveDir.y * -10 * moveSpeed);
            Move(moveDir.normalized);
        }

    }
}
