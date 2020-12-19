﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : Entity
{
    [SerializeField]
    float speed = 4;
    [SerializeField]
    Bullet charBullet;
    [SerializeField]
    float cooldownDefault = 0;
    [SerializeField]
    Rigidbody2D firingPoint;
    [SerializeField]
    Healthbar healthbar;
    float cooldown = 0;
    GameMasterScript gameMaster;


    bool spiderGemCollected = false;
    bool gostGemCollected = false;
    bool goblinGemCollected = false;
    Rigidbody2D rb;
    public static CharacterController instance;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        healthbar.SetMaxHealt(health);
        rb = GetComponent<Rigidbody2D>();
        gameMaster = GameMasterScript.instance;
    }

    // Update is called once per frame
    void Update()
    {
        float hControl = Input.GetAxis("Horizontal");
        float vControl = Input.GetAxis("Vertical");
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        
        rb.position += new Vector2(hControl, vControl) * Time.deltaTime * speed;
        firingPoint.rotation = angle;
        firingPoint.position = rb.position;

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && cooldown <= 0)
            shootBullet(direction, angle);
        cooldown -= Time.deltaTime;
    }

    void shootBullet(Vector3 direction, float angle)
    {
        if(cooldown <= 0)
		{
            Debug.Log(direction.normalized);

            Instantiate(charBullet).init(direction, firingPoint.transform.position + direction.normalized, angle);
            cooldown = cooldownDefault;
		}
        else
		{
            cooldown -= Time.deltaTime;
		}
    }

    void OnCollisionEnter2D(Collision2D collsion)
	{
        if (collsion.gameObject.tag == "SpiderGem" ||
            collsion.gameObject.tag == "GostGem" ||
            collsion.gameObject.tag == "GoblinGem")
        {
            switch (collsion.gameObject.tag)
			{
                case "SpiderGem":
                    spiderGemCollected = true;
                    break;
                case "GostGem":
                    gostGemCollected = true;
                    break;
                case "GoblinGem":
                    goblinGemCollected = true;
                    break;
            }

            Debug.Log(collsion.contacts.ToString());
            Destroy(collsion.collider.gameObject);
        }

    }

    public override void TakeDamage(float damage)
	{
        base.TakeDamage(damage);
        healthbar.SetHealth(health);
	}

    protected override void Die()
	{
        gameMaster.GameOver();
	}
}
