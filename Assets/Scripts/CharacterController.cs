﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    GameMasterScript gameMaster => GameMasterScript.instance;

    UnityAction updateMovementVector;
    Vector2 movement;
    
    [SerializeField]
    internal float maxHealth;


    internal bool spiderGemCollected = false;
    internal bool gostGemCollected = false;
    internal bool goblinGemCollected = false;
    bool pausePressed = false;
    Rigidbody2D rb;
    public static CharacterController instance;
    
    public void autoMove(float time)
    {
        StartCoroutine(disableInput(time));
    }

    private IEnumerator disableInput(float time)
    {
        Debug.Log("Input Dis");
        updateMovementVector -= checkMoveInput;
        movement = movement.normalized;
        yield return new WaitForSeconds(time);
        updateMovementVector += checkMoveInput;
    }


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        health = maxHealth;
        healthbar.SetMaxHealt(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        updateMovementVector += checkMoveInput;
    }

    void checkMoveInput()
    {
        float hControl = Input.GetAxis("Horizontal");
        float vControl = Input.GetAxis("Vertical");
        movement = new Vector2(hControl, vControl);
    }

    // Update is called once per frame
    void Update()
    {
        updateMovementVector?.Invoke();
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !pausePressed)
		{
            gameMaster.TogglePauseMenu();
            pausePressed = true;
        }
        if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape))
        {
            pausePressed = false;
        }

        rb.position += movement * Time.deltaTime * speed;
        firingPoint.rotation = angle;
        firingPoint.position = rb.position;

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && cooldown <= 0 && !gameMaster.paused)
            shootBullet(direction, angle);
        cooldown -= Time.deltaTime;
    }

    void shootBullet(Vector3 direction, float angle)
    {
        if (cooldown <= 0)
        {
            Instantiate(charBullet).init(direction, firingPoint.transform.position + direction.normalized, angle);
            cooldown = cooldownDefault;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void PickUpHealthpack(GameObject healthpickup)
    {
        if (health >= maxHealth) { return; }

        health += Mathf.Clamp(healthpickup.GetComponent<Healthpickup>().restoreHealth, 0, maxHealth - health);
        healthbar.SetHealth(health);
        Destroy(healthpickup);
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

            health = maxHealth;
            healthbar.SetHealth(health);
            Destroy(collsion.collider.gameObject);
        }
    }

    public override void TakeDamage(float damage)
    {
        print("Hit");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        healthbar.SetHealth(health);
    }

    protected override void Die()
	{
        gameMaster.GameOver();
	}
}
