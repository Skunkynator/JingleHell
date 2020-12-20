using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : Enemy
{
    [SerializeField]
    int bulletAmound = 3;
    [SerializeField]
    float startAngle, endAngle;
    [SerializeField]
    Bullet bullet;

    void Awake()
    {
        InvokeRepeating("Shoot", 0f, 20f);
    }

    void Shoot()
	{
        float angleStep = (endAngle - startAngle) / bulletAmound;
        float angle = startAngle;

        print("shoot");
        for (int i = 0; i < bulletAmound + 1; i++)
		{
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

            Instantiate(bullet).init(new Vector2(bulDirX, bulDirY), this.transform.position, 0f);

            angle += angleStep;
        }
	}
}
