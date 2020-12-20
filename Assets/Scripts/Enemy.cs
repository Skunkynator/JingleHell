using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [SerializeField]
    GameObject healthpickup;

    protected override void Die() {
        Destroy(gameObject);
        if (CharacterController.instance.health <= CharacterController.instance.maxHealth * 0.5 &&
            GameObject.FindGameObjectsWithTag("Healthpickup").Length < 2)
            Instantiate(healthpickup, this.transform.position, Quaternion.identity);
        Room.Current.toggleDoorsIf();
    }
}
