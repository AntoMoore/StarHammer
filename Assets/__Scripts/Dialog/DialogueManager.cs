using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // == member variables ==
    public delegate void ExitDialogueEvent();
    public static ExitDialogueEvent DialogueEvent;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    private Queue<string> sentences;

    // == member methods ==
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        //Debug.Log("Starting Conversation with: " + dialogue.name);
        nameText.text = dialogue.name;
        //clear previous sentences
        sentences.Clear();

        // loop through all sentences in NPC dialogue
        foreach(string s in dialogue.sentences)
        {
            // add to queue
            sentences.Enqueue(s);
        }

        // display next sentence
        this.DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //check if any remaining dialogue text in queue
        if(sentences.Count == 0)
        {
            this.EndDialogue();
            return;
        }

        // get next sentence from queue
        string nextSentence = sentences.Dequeue();
        // stop coroutines, in event player is trying to skip dialogue
        StopAllCoroutines();
        // start coroutine to show dialogue
        StartCoroutine(TypeSentence(nextSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        // set text to empty string
        dialogueText.text = "";
        // loop through sentence
        foreach(char letter in sentence.ToCharArray())
        {
            // print each character
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        // set animator to false
        animator.SetBool("isOpen", false);
        // publish event
        this.PublishExitDialogueEvent();
    }

    private void PublishExitDialogueEvent()
    {
        if(DialogueEvent != null)
        {
            DialogueEvent();
        }
    }
}
