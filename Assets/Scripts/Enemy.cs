using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [SerializeField]
    float collisionAttackCooldownDefault = 0.5f;
    float collisionAttackCooldown;

    void Update()
	{
        collisionAttackCooldown -= Time.deltaTime;
	}

    protected override void Die() {
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterController.instance.TakeDamage(25);
            collisionAttackCooldown = collisionAttackCooldownDefault;
        }

    }
}
