using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private Transform player;

    [SerializeField]
    private GameObject dialogueButton;
    [SerializeField]
    private AudioClip kingSound;
    [SerializeField]
    private float minSound = 0, maxSound = 0, vol = 0;

    public void TriggerDialogue() => FindObjectOfType<DialogueManager>().StartDialogue(dialogue, kingSound,minSound,maxSound,vol);

    private void Awake()
    {
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
