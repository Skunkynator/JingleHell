using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    Room left;
    [SerializeField]
    Room right;
    [SerializeField]
    Room top;
    [SerializeField]
    Room down;
    [SerializeField]
    RoomTrigger triggerLeft;
    [SerializeField]
    RoomTrigger triggerRight;
    [SerializeField]
    RoomTrigger triggerTop;
    [SerializeField]
    RoomTrigger triggerDown;
    [SerializeField]
    Transform cameraPosition;

    public Transform CameraPosition => cameraPosition;

    void Start()
    {
        triggerLeft?.setRoom(left);
        triggerTop?.setRoom(top);
        triggerDown?.setRoom(down);
        triggerRight?.setRoom(right);
    }
}
