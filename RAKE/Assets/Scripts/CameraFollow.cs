using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{

    public Transform PlayerrTransform;

    private Vector3 cmOffset;

    public float smoothness = 0.5f;

    public bool isCloud = false;

    // Start is called before the first frame update
    void Start()
    {
        cmOffset = transform.position - PlayerrTransform.position;
    }

    private void FixedUpdate()
    {
        if (PlayerrTransform.position.x > -10 || isCloud)
        {
            Vector3 newPos = PlayerrTransform.position + cmOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
        }
    }
}
