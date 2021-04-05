using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeHippie : MonoBehaviour
{
    [Header("Stats")]
    private bool dancing = false;

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

   // private ShowItem item;
    public GameObject myPrefab;

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

    public void ResponseDialogue ()
    {
        dialogue.ResponseDialogue(dialogueData.dialogue);

    }

    public void ResetDialogue()
    {
        dialogue.ResetDialogue();
    }

    public void ShowBoombox()
    {
        // item.ShowObject();
        Instantiate(myPrefab, new Vector3(12, 1, 0), Quaternion.identity);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && collision.GetComponent<Player>().hasBoomBox == true)
        {
           
            ResponseDialogue();
            collision.GetComponent<Player>().RemoveItemFromInventory("Boombox");
            dancing = true;

            ShowBoombox();
            Invoke("ResetDialogue", 4);

        }
        
          else if ( collision.GetComponent<Player>() != null && dancing == false)
        {
            StartDialogue();

            Invoke("ResetDialogue", 4);

        } 
        
    }
    void Move()
    {
        if (dancing == true)
        {
            an.enabled = true;
        }
        else
        {
            an.enabled = false;
        }

    }

}
