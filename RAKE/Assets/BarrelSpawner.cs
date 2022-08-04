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


    void Update()
    {
        if (timer > maxTime)
        {
            Instantiate(barrel);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
