using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [SerializeField]
    GameObject healthpickup;
    [SerializeField]
    float touchDamage;
    [SerializeField]
    internal float touchDamageCooldownDefault = 1;
    internal float touchDamageCooldown;

  
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && touchDamageCooldown <= 0)
		{
            print("e");
            CharacterController.instance.TakeDamage(touchDamage);
            touchDamageCooldown = touchDamageCooldownDefault;
        }
        else
		{
            touchDamageCooldown -= Time.deltaTime;
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchDamageCooldown = 0;
        }
    }




    protected override void Die()
    {
        Destroy(gameObject);
        if (CharacterController.instance.health <= CharacterController.instance.maxHealth * 0.5 &&
            GameObject.FindGameObjectsWithTag("Healthpickup").Length < 2)
            Instantiate(healthpickup, this.transform.position, Quaternion.identity, this.transform.parent);
        //Instantiate(healthpickup, this.transform.position, Quaternion.identity);
        Room.Current.checkEnemies();
    }

}
