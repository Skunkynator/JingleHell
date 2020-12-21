using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Enemy
{
    [SerializeField]
    GameObject drop;

    internal override void Die()
    {
        Instantiate(drop);
	    base.Die();
    }
}
