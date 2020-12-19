using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
