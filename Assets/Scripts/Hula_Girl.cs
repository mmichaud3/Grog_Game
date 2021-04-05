using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hula_Girl : MonoBehaviour
{
    [Header("Stats")]
    private bool hula = false;

    [Header("Sprites")]


    // components
    private Rigidbody2D rig;
    private SpriteRenderer sr;
    private Animator an;
    [SerializeField]
    private DialogueData dialogueData;
    [SerializeField]
    private Dialogue dialogue;


    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
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
        if (collision.GetComponent<Player>() != null && collision.GetComponent<Player>().hasHulaHoop == true)
        {

            ResponseDialogue();
            collision.GetComponent<Player>().RemoveItemFromInventory("Hula Hoop");
            hula = true;


            Invoke("ResetDialogue", 4);

        }

        else if (collision.GetComponent<Player>() != null && hula == false)
        {
            StartDialogue();

            Invoke("ResetDialogue", 4);

        }

    }
    void Move()
    {
        if (hula == true)
        {
            an.enabled = true;
        }
        else
        {
            an.enabled = false;
        }

    }
}
