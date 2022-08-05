using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPosX;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject cam;


    private void Start()
    {
        startPosX = transform.position.x;
    }

    private void Update()
    {
        float distance = cam.transform.position.x * speed;
        transform.position = new Vector3(startPosX - distance, transform.position.y, transform.position.z);
    }
}
