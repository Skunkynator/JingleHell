using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private Room room;
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
    }
}
