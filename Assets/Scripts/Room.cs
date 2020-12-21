using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
	static private Room current;
	static public Room Current => current;

	[SerializeField]
	internal Room left;
	[SerializeField]
	internal Room right;
	[SerializeField]
	internal Room top;
	[SerializeField]
	internal Room down;
	[SerializeField]
	internal RoomTrigger triggerLeft;
	[SerializeField]
	internal RoomTrigger triggerRight;
	[SerializeField]
	internal RoomTrigger triggerTop;
	[SerializeField]
	internal RoomTrigger triggerDown;
	[SerializeField]
	internal Transform cameraPosition;
	[SerializeField]
	internal GameObject enemies;
	[SerializeField]
	internal GameObject doors;

	public Transform CameraPosition => cameraPosition;

	private void Awake()
	{
		doors.SetActive(false);
		enemies.SetActive(false);
	}

	private void Start()
	{
		triggerLeft?.setRoom(left);
		triggerTop?.setRoom(top);
		triggerDown?.setRoom(down);
		triggerRight?.setRoom(right);
		triggerLeft.OnEnter += OnExit;
		triggerTop.OnEnter += OnExit;
		triggerDown.OnEnter += OnExit;
		triggerRight.OnEnter += OnExit;
	}

	internal void OnExit()
	{
		enemies.SetActive(false);
	}

	internal void OnEnter()
	{
		if (!enemies.activeInHierarchy)
			PlayerController.instance.AutoMove(0.25f);
		enemies.SetActive(true);
		StartCoroutine(CheckForDoors());
		current = this;
	}

	internal void CheckEnemies()
	{
		StartCoroutine(CheckForDoors());
	}

	private IEnumerator CheckForDoors()
	{
		yield return new WaitForSeconds(0.25f);
		if (enemies.transform.childCount == 0)
			doors.SetActive(false);
		else
			doors.SetActive(true);
	}
}
