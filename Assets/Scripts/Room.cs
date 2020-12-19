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
    [SerializeField]
    GameObject enemies;

    public Transform CameraPosition => cameraPosition;

    void Awake()
	{
        enemies.SetActive(false);
	}

    void Start()
    {
        triggerLeft?.setRoom(left);
        triggerTop?.setRoom(top);
        triggerDown?.setRoom(down);
        triggerRight?.setRoom(right);
        triggerLeft.OnEnter += onExit;
        triggerTop.OnEnter += onExit;
        triggerDown.OnEnter += onExit;
        triggerRight.OnEnter += onExit;
    }

    void onExit()
    {
        enemies.SetActive(false);
    }
    public void onEnter()
    {
        enemies.SetActive(true);
    }
}
