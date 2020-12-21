using UnityEngine;

public class TriggerMoveCamera : MonoBehaviour
{
	[SerializeField]
	internal new CameraController camera;
	[SerializeField]
	internal Transform cameraTargetPosition;

	protected void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log(collider.gameObject.tag);
		if (collider.gameObject.tag == "Player")
		{
			camera.currentPos = cameraTargetPosition.position;
		}
	}
}
