using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{
    [SerializeField]
    private GameObject credits, cloud;
    void Update()
    {
        if (PlayerPrefs.GetInt("GameWon") == 1) 
        {
            credits.SetActive(true);
            cloud.SetActive(false);
        }
    }
}
