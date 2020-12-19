using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMoveCamera : MonoBehaviour
{
	// Start is called before the first frame update
	public Camera camera;
	public Transform cameraTargetPosition;

	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log(collider.gameObject.tag);
		if (collider.gameObject.tag == "Player")
		{
			camera.transform.position = cameraTargetPosition.position;
		}
	}
	
}