using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float baseDamage;
    [SerializeField]
    float attackCooldown;

    void Awake()
    {
    }

    void OnEnable()
	{
        print(CharacterController.instance);
        if(CharacterController.instance)
		{
            if(CharacterController.instance.spiderGemCollected)
		    {
                moveSpeed *= 0.75f;
            }
            if(CharacterController.instance.goblinGemCollected)
		    {
                baseDamage *= 0.5f;
                attackCooldown *= 1.25f;

            }
            if(CharacterController.instance.gostGemCollected)
		    {
                health *= 0.8f;
		    }
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
