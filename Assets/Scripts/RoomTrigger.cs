using UnityEngine;
using UnityEngine.Events;

public class RoomTrigger : MonoBehaviour
{
	private Room room;
	internal UnityAction OnEnter;

	public void setRoom(Room room)
	{
		this.room = room;
	}

	protected void OnTriggerEnter2D(Collider2D collider)
	{
		CameraController.instance.currentPos = room.CameraPosition.position;
		OnEnter?.Invoke();
		room?.OnEnter();
	}
}
