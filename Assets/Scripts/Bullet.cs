﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool initialised = false;
    Vector2 direction;
    bool enemyBullet;
    float speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!initialised) return;
        transform.position +=(Vector3)direction * Time.deltaTime * speed;
    }
    public void init(Vector2 direction, bool enemyBullet, Vector2 position)
    {
        if(initialised) return;
        initialised = !initialised;
        this.direction = direction.normalized;
        this.enemyBullet = enemyBullet;
        transform.position = position;
    }

}
