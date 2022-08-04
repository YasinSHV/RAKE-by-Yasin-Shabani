using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject intractButton, holder;

    [SerializeField]
    private AudioSource open, error;
    
    private GameObject door, cam;

    private Transform player;

    private bool isOpening = false;
    private void Awake()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < 3)
        {
            intractButton.SetActive(true);
            if (!isOpening && Input.GetKeyDown(KeyCode.E)) 
            {
                if (PlayerPrefs.GetInt("Blood") >= 5000)
                {
                    cam.GetComponent<CameraFollow>().StartCoroutine(cam.GetComponent<CameraFollow>().Shake(2f,0.034f));
                    isOpening = true;
                    door.GetComponent<Animator>().SetBool("Open", true);
                    PlayerPrefs.SetInt("Blood", PlayerPrefs.GetInt("Blood") - 5000);
                    open.Play();
                    holder.SetActive(false);
                    intractButton.SetActive(false);
                }
                else 
                {
                    error.Play();
                }
            }
        }
        else
            intractButton.SetActive(false);
    }
}
