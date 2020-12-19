using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMoveCamera : MonoBehaviour
{
	// Start is called before the first frame update
    [SerializeField]
    private CameraController camera;
    [SerializeField]
    private Transform cameraTargetPosition;

    void OnTriggerEnter2D(Collider2D collider)
    {
	Debug.Log(collider.gameObject.tag);
	if (collider.gameObject.tag == "Player")
	{
	    camera.currentPos = cameraTargetPosition.position;
	}
    }
	
}