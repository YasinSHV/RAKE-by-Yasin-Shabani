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


    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) 
    {
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
        dialogue.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
