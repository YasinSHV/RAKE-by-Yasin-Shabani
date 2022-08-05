using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject barrel;
    [SerializeField]
    private float maxTime = 2f;

    private float timer = 0;

    private void Awake()
    {
        timer = maxTime;
    }
    void Update()
    {
        if (timer > maxTime)
        {
            Instantiate(barrel, transform.position, Quaternion.identity);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
