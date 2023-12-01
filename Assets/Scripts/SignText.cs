using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignText : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject box;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        Pause.isPaused = true; //Pause the game while reading dialogue.
        box.SetActive(true); //Show the dialogue box.
        Time.timeScale = 0;
        Debug.Log("Reading sign");

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //Debug.Log(sentence);
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Pause.isPaused = false;
        box.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("End of text");
    }
}