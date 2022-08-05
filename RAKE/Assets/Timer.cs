using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    GameObject volume;

    float timer = 60;

    void Update()
    {
        timer -= Time.deltaTime;
        text.text = timer.ToString("f2");
        if (timer < 0)
            volume.SetActive(true);

    }
}
