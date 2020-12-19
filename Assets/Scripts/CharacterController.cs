using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : Entity
{
    public static CharacterController instance;
    [SerializeField]
    float speed = 4;
    Rigidbody2D rb;
    [SerializeField]
    Bullet charBullet;
    [SerializeField]
    float bulletsPS = 2;
    float cooldown = 0;
    [SerializeField]
    GameMasterScript gameMaster;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hControl = Input.GetAxis("Horizontal");
        float vControl = Input.GetAxis("Vertical");
        rb.position += new Vector2(hControl, vControl) * Time.deltaTime * speed;
        if(Input.GetKey(KeyCode.Space) && cooldown <= 0)
            shootBullet();
        cooldown -= Time.deltaTime;
    }

    void shootBullet()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Instantiate(charBullet).init(direction, false, transform.position);
        cooldown = 1/bulletsPS;
    }

    override protected void Die()
	{
        gameMaster.GameOver();
	}
}
