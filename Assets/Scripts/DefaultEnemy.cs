using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class DefaultEnemy : Enemy
{
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private float moveSpeed = 2;
    [SerializeField]
    private float shotsPerSecond = 1;
    [SerializeField]
    private Bullet enemyBullet;

    private Collider2D myCollider;
    private Rigidbody2D rb;
    private Vector2 moveDir = Vector2.zero;
    private bool seeingPlayer = false;
    private float attackCooldown = 0;

    private Vector3 playerPos => CharacterController.instance.transform.position;

    UnityAction updateMovement;
    UnityAction updateAttack;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        updateMovement += checkMovement;
    }
    void Update()
    {
        updateMovement?.Invoke();
        updateAttack?.Invoke();
        attackCooldown -= Time.deltaTime;
    }
    void onPlayerSeen()
    {
        seeingPlayer = true;
        updateAttack += AttackPlayer;
    }
    void onPlayerHidden()
    {
        seeingPlayer = false;
        updateAttack -= AttackPlayer;
    }
    void AttackPlayer()
    {
        if(attackCooldown > 0) return;
        Vector3 direction = playerPos - transform.position;
        Instantiate(enemyBullet).init(direction, true, transform.position + direction.normalized * 0.3f);
        attackCooldown = 1 / shotsPerSecond;
    }
    void checkMovement()
    {
        updateMovement -= checkMovement;
        updateMovement += move;
        if(CheckForPlayer())
        {
            moveDir = playerPos - transform.position;
            StartCoroutine(waitForCheck(0.1f));
            if(!seeingPlayer) onPlayerSeen();
        }
        else
        {
            moveDir += new Vector2(Random.Range(-1f, 1f),Random.Range(-1f, 1f)) * 0.25f;
            StartCoroutine(waitForCheck(Random.Range(0.1f, 0.5f)));
            if(seeingPlayer) onPlayerHidden();
        }
        moveDir.Normalize();
    }
    void move()
    {
        rb.position += moveDir * Time.deltaTime * moveSpeed;
    }

    bool CheckForPlayer()
    {
        Vector2 direction = playerPos - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction, direction.magnitude , mask);
        
        return hit.collider == null;  
    }
    
    IEnumerator waitForCheck(float moveTime)
    {
        yield return new WaitForSeconds(moveTime);
        updateMovement -= move;
        updateMovement += checkMovement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(myCollider.IsTouchingLayers(mask))
        {
            Vector2 normal = collision.GetContact(0).normal;
            moveDir = Vector2.Reflect(moveDir,normal) - moveDir * normal * 0.2f;
        }
    }
}
