using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject eButton, panel;

    [SerializeField]
    private VideoPlayer video;

    [SerializeField]
    private VideoClip[] clips;

    [SerializeField]
    private TextMeshProUGUI tutorial;

    private GameObject player;

    private bool isPlaying = false;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 2)
        {
            eButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !isPlaying)
            {
                isPlaying = true;
                StartCoroutine(PlayVideos());
            }
        }
        else 
        {
            eButton.SetActive(false);
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) > 6)
        {
            isPlaying = false;
            panel.SetActive(false);
            StopAllCoroutines();
        }

    }

    IEnumerator PlayVideos()
    {
        eButton.SetActive(false);
        panel.SetActive(true);
        for (int i = 0; i < 3; i++) 
        {
            video.clip = clips[i];
            video.Play();
            switch (i) 
            {
                case 0:
                    tutorial.text = "AD or Arrow Keys To Run";
                    break;
                case 1:
                    tutorial.text = "Space To Roll";
                    break;
                case 2:
                    tutorial.text = "Left Mouse Button To Attack";
                    break;
            }
            yield return new WaitForSeconds(3f);
        }
        isPlaying = false;
        panel.SetActive(false);

    }
}
