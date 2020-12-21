using System.Collections;
using UnityEngine;

public class GostMiniBoss : MiniBoss
{
	internal IEnumerator currentAttack;
	internal IEnumerator attackControl;

	[SerializeField]
	internal Bullet bullet;
	[SerializeField]
	internal float timeOffset;
	[SerializeField]
	internal int bulletAmount;
	[SerializeField]
	internal float spreadAngle;

	private void Start()
	{
		attackControl = Attack();
		StartCoroutine(attackControl);
	}

	internal override void Die()
	{
		StopCoroutine(currentAttack);
		StopCoroutine(attackControl);
		base.Die();
	}

	private IEnumerator Attack()
	{
		while (true)
		{
			currentAttack = BulletPatterns.TimedSpread(bullet, bulletAmount, spreadAngle, transform, 0.5f, timeOffset);
			StartCoroutine(currentAttack);
			yield return new WaitForSeconds(timeOffset * (bulletAmount * 2 - 1));
		}
	}
}
