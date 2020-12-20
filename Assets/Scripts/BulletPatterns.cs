using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletPatterns
{
    
    public static IEnumerator timedSpread(Bullet bullet, int amount, float angleBetween, Transform tOrigin, float offset, float timeOffset)
    {
        Vector2 origin;
        float currentAngle =- angleBetween * ((amount - 1)/ 2f);
        for(int i = 0; i < amount;i++)
        {
            origin = tOrigin.position;
            Vector2 direction = (Vector2)CharacterController.instance.transform.position - origin;
            Vector2 curDirection = Quaternion.Euler(0,0,currentAngle) * direction;
           Bullet.Instantiate(bullet).init(curDirection,origin + direction.normalized * offset,Mathf.Atan2(curDirection.y, curDirection.x) * Mathf.Rad2Deg - 45f);
           yield return new WaitForSeconds(timeOffset);
           currentAngle += angleBetween;
        }
        for(int i = 0; i < amount-1;i++)
        {
            origin = tOrigin.position;
            currentAngle -= angleBetween;
            Vector2 direction = (Vector2)CharacterController.instance.transform.position - origin;
            Vector2 curDirection = Quaternion.Euler(0,0,currentAngle) * direction;
           Bullet.Instantiate(bullet).init(curDirection,origin + direction.normalized * offset,Mathf.Atan2(curDirection.y, curDirection.x) * Mathf.Rad2Deg - 45f);
           yield return new WaitForSeconds(timeOffset);
        }

    
    }

    public static void spreadPlayer(Bullet bullet, int amount, float angleBetween, Vector2 origin, float offset)
    {
       spread(bullet, amount, angleBetween, origin, offset,
               (Vector2)CharacterController.instance.transform.position - origin); 
    }
    public static void spread(Bullet bullet, int amount, float angleBetween, Vector2 origin, float offset, Vector2 direction)
    {
        float currentAngle =- angleBetween * ((amount - 1)/ 2f);
        for(int i = 0; i < amount;i++)
        {
            Vector2 curDirection = Quaternion.Euler(0,0,currentAngle) * direction;
           Bullet.Instantiate(bullet).init(curDirection,origin + direction.normalized * offset,Mathf.Atan2(curDirection.y, curDirection.x) * Mathf.Rad2Deg - 45f);
        }
    }
}
