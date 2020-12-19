using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected float health;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
	    {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
