using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour
{
    public GameObject fadeOut, arrow;
    private Transform player, cam;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("DNumb") >= 1)
            arrow.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
        if (cam.position.x < 10.9 && cam.position.x > 10.2 )
        {
            if (player.position.x > 27.1f)
            {
                fadeOut.SetActive(true);
            }
        }
        else 
        {
            if (player.position.x > 12.3f) 
            {
                fadeOut.SetActive(true);
            }
        }
    }
}
