using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
	static public CameraController instance { get; private set; }

	[SerializeField]
	internal float smoothTime = 0.3f;

	internal Vector3 currentPos;
	private Vector3 velocity;

	private void Start()
	{
		instance = this;
		currentPos = transform.position;
	}

	private void Update()
	{
		transform.position = Vector3.SmoothDamp(transform.position, currentPos, ref velocity, smoothTime);
	}
}
