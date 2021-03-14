using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeHippie : MonoBehaviour
{
    [Header("Stats")]
    private bool dancing = true;

   [Header("Sprites")]
   public Sprite leftHand;
   public Sprite handsDown;
   public Sprite rightHand;
   public Sprite handsOut;

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

    public void ResetDialogue()
    {
        dialogue.ResetDialogue();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Fire1") && collision.GetComponent<Player>() != null)
        {
            StartDialogue();

            Invoke("ResetDialogue", 4);

        }
        
    }
    void Move()
    {
        if (dancing == true)
        {
            an.Play("LargeHippieLoop");
        }
        else
        {
            an.enabled = false;
        }

    }

}
