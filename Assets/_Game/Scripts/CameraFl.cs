using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 20;
    void Start()
    {
        target = FindObjectOfType<Player>().transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
}
