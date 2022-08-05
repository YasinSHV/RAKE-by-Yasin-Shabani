using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-100,-900), Random.Range(100, 400)));
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0,0,1) * 300 * Time.deltaTime);
    }

}
