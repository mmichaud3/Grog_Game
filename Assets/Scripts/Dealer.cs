using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{

    [Header("Sprites")]
    private Rigidbody2D rig;

    [SerializeField]
    private DialogueData dialogueData;
    [SerializeField]
    private Dialogue dialogue;


    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
     
    }


    public void StartDialogue()
    {
        dialogue.StartDialogue(dialogueData.dialogue);
    }

    public void ResponseDialogue()
    {
        dialogue.ResponseDialogue(dialogueData.dialogue);

    }

    public void ResetDialogue()
    {
        dialogue.ResetDialogue();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && collision.GetComponent<Player>().money >= 10)
        {
            StartDialogue();

            Invoke("ResetDialogue", 6);

        }

        else if (collision.GetComponent<Player>() != null && collision.GetComponent<Player>().money < 10)
        {
            ResponseDialogue();
            Invoke("ResetDialogue", 4);

        }

    }
}
