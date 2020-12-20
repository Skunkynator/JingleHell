using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GostMiniBoss : MiniBoss
{
    // Start is called before
    IEnumerator currentAttack;
    IEnumerator attackControl;
    [SerializeField]
    Bullet bullet;
    [SerializeField]
    float timeOffset;
    [SerializeField]
    int bulletAmount;
    [SerializeField]
    float spreadAngle;
    void Start()
    {
        attackControl = attack();
        StartCoroutine(attackControl);
    }

    IEnumerator attack()
    {


        while(true)
        {
            currentAttack = BulletPatterns.timedSpread(bullet, bulletAmount, spreadAngle, transform, 0.5f, timeOffset);
            StartCoroutine(currentAttack);
            yield return new WaitForSeconds(timeOffset * (bulletAmount * 2 - 1));
        }
    }

    override protected void Die()
    {
        StopCoroutine(currentAttack);
        StopCoroutine(attackControl);
        base.Die();
    }
}
