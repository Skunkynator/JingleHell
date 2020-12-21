using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	[SerializeField]
	internal Slider slider;

	internal void SetMaxHealt(float health)
	{
		slider.maxValue = health;
		slider.value = health;
		SetHealth(health);
	}

	internal void SetHealth(float health)
	{
		slider.value = Mathf.Clamp(health, slider.minValue, slider.maxValue);
	}
}
