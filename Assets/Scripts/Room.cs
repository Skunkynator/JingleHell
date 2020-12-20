using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    static private Room current;
    static public Room Current => current;
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
    [SerializeField]
    GameObject doors;

    public Transform CameraPosition => cameraPosition;

    void Awake()
    {
        doors.SetActive(false);
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
        if(enemies.transform.childCount != 0)
            StartCoroutine(closeDoors());
        current = this;
    }
    public void checkEnemies()
    {
        if(enemies.transform.childCount == 0)
            doors.SetActive(false);
    }

    private IEnumerator closeDoors()
    {
        yield return new WaitForSeconds(0.5f);
        doors.SetActive(true);
    }
}
