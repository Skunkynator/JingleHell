using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpickup : MonoBehaviour
{
    [SerializeField]
    public float restoreHealth;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            CharacterController.instance.PickUpHealthpack(this.gameObject);
        }
    }
}
