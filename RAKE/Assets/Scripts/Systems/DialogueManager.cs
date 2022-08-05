using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Text nameText, dialogue;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audios;
    private float minSound = 0, maxSound = 0, soundVol = 0;


    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, AudioClip npcVoice, float min, float max, float vol) 
    {
        minSound = min;
        maxSound = max;
        soundVol = vol;

        audios.clip = npcVoice;
        sentences.Clear();
        nameText.text = dialogue.name;

        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }
        DisplayNext();
    }

    public void DisplayNext()
    {
        panel.SetActive(true);
        if (sentences.Count == 0) 
        {
            EndDialogue();
                return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue() 
    {
        panel.SetActive(false);
    }

    IEnumerator TypeSentence(string sentence) 
    {
        int i = 0;
        audios.volume = soundVol;
        dialogue.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            i++;
            audios.pitch = Random.Range(minSound, maxSound);
            if(i % 3 == 0)
            audios.Play();
            dialogue.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(0.3f);
    }
}
