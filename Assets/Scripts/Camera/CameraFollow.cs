using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCapture : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    public float smoothing = 5f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, offset + player.transform.position,smoothing*Time.deltaTime);
    }
}
