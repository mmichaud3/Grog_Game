﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackeyGuy : MonoBehaviour
{
    [Header("Stats")]
    private bool hacking = false;

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
        if (collision.GetComponent<Player>() != null && collision.GetComponent<Player>().hasHackySack == true)
        {

            ResponseDialogue();
            collision.GetComponent<Player>().RemoveItemFromInventory("Hacky Sack");
            hacking = true;
            

            Invoke("ResetDialogue", 4);

        }

        else if (collision.GetComponent<Player>() != null && hacking == false)
        {
            StartDialogue();

            Invoke("ResetDialogue", 4);

        }

    }
    void Move()
    {
        if (hacking == true)
        {
            an.enabled = true;
        }
        else
        {
            an.enabled = false;
        }

    }

}
