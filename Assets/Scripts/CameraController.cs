using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    static public CameraController instance;
    [SerializeField]
    float smoothTime = 0.3f;
    public Vector3 currentPos;
    private Vector3 velocity;
    void Start()
    {
        instance = this;
        currentPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, currentPos, ref velocity, smoothTime);
    }
}
