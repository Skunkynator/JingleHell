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

    private Collider2D myCollider;
    private Rigidbody2D rb;
    private Vector2 moveDir = Vector2.zero;
    
    UnityAction updateMovement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        updateMovement += checkMovement;
    }

    // Update is called once per frame
    void Update()
    {
        updateMovement?.Invoke();
    }
    void checkMovement()
    {
        updateMovement -= checkMovement;
        updateMovement += move;
        if(CheckForPlayer())
        {
            moveDir = CharacterController.instance.transform.position - transform.position;
            StartCoroutine(waitForCheck(0.1f));
        }
        else
        {
            moveDir += new Vector2(Random.Range(-1f, 1f),Random.Range(-1f, 1f)) * 0.25f;
            StartCoroutine(waitForCheck(Random.Range(0.1f, 0.5f)));
        }
        moveDir.Normalize();
    }
    void move()
    {
        rb.position += moveDir * Time.deltaTime * moveSpeed;
    }
    IEnumerator waitForCheck(float moveTime)
    {
        yield return new WaitForSeconds(moveTime);
        updateMovement -= move;
        updateMovement += checkMovement;
    }
    bool CheckForPlayer()
    {
        Vector2 direction = CharacterController.instance.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction, direction.magnitude , mask);
        
        return hit.collider == null;  
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
