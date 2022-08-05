using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;

    private Transform player;

    [SerializeField]
    private GameObject dialogueButton;
    [SerializeField]
    private AudioClip kingSound;
    [SerializeField]
    private float minSound = 0, maxSound = 0, vol = 0;

    public void TriggerDialogue() => FindObjectOfType<DialogueManager>().StartDialogue(dialogue[PlayerPrefs.GetInt("DNumb")], kingSound,minSound,maxSound,vol);

    private void Awake()
    {
        if (PlayerPrefs.GetInt("DNumb") == 1)
        {
            if (PlayerPrefs.GetInt("Blood") > 700)
            {
                PlayerPrefs.SetInt("DNumb", PlayerPrefs.GetInt("DNumb") + 1);
            }
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {

        if (Mathf.Abs(player.position.x - transform.position.x) < 4)
        {
            dialogueButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();
            }
        }
        else 
        {
            dialogueButton.SetActive(false);
        }
    }
}
