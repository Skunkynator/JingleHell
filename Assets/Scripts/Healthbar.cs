using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	[SerializeField]
	Slider slider;
	
	public void SetMaxHealt(float health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

	public void SetHealth(float health)
	{
		slider.value = Mathf.Clamp(health, slider.minValue, slider.maxValue);
	}
}
