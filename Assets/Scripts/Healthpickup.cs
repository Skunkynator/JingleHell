using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpickup : MonoBehaviour
{
    [SerializeField]
    internal float restoreHealth;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.instance.PickUpHealthpack(this.gameObject);
        }
    }
}
