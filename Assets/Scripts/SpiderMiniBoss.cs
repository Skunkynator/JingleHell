using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMiniBoss : Entity
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject drop;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected override void Die()
	{
        Instantiate(drop);
		base.Die();
	}
}
