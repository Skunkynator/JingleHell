using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomTrigger : MonoBehaviour
{
    private Room room;
    public UnityAction OnEnter;
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void setRoom(Room room)
    {
        this.room = room;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        CameraController.instance.currentPos = room.CameraPosition.position;
        OnEnter?.Invoke();
        room?.onEnter();
    }
}
