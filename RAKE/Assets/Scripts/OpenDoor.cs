using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject intractButton, holder, goIn;

    [SerializeField]
    private AudioSource open, error;
    
    private GameObject door, cam;

    private Transform player;

    private bool isOpening = false, isOpen = false;
    private void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        if (PlayerPrefs.GetInt("Door") == 1)
        {
            door.GetComponent<Animator>().SetBool("Open", true);
            holder.SetActive(false);
            intractButton.SetActive(false);
            isOpen = true;
            open.volume = 0.3f;
            open.Play();
        }
    }

    void Update()
    {
        if (!isOpen)
        {

            if (Mathf.Abs(transform.position.x - player.position.x) < 3)
            {
                intractButton.SetActive(true);
                if (!isOpening && Input.GetKeyDown(KeyCode.E))
                {
                    if (PlayerPrefs.GetInt("Blood") >= 5000)
                    {
                        isOpen = true;
                        PlayerPrefs.SetInt("Door", 1);
                        cam.GetComponent<CameraFollow>().StartCoroutine(cam.GetComponent<CameraFollow>().Shake(2f, 0.034f));
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
        else
        {

            if (Mathf.Abs(transform.position.x - player.position.x) < 3)
            {
                goIn.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    goIn.GetComponent<ButtonManager>().Boss();
                }
            }

        }
    }
}
