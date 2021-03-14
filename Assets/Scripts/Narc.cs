using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narc : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    private Vector2 facingDirection;
    public int xpToGive;

    [Header("Target")]
    public float chaseRange;
    public float attackRange;
    public bool escaped = false;
    private Player player;

    [Header("Sprites")]
    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;


    // components
    private Rigidbody2D rig;
    private SpriteRenderer sr;
    [SerializeField]
    private DialogueData dialogueData;
    [SerializeField]
    private Dialogue dialogue;
    private Vector2 spawnPosition;
    private Vector2[] movementDirections = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

   

    
    void Awake ()
    {
        // get the player target
        player = FindObjectOfType<Player>();

        // get the rigidbody component
        rig = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        spawnPosition = transform.position;
        Wander();
    }

    public void StartDialogue()
    {
        dialogue.StartDialogue(dialogueData.dialogue);
    }
    void Update()
    {
        
        float playerDist = Vector2.Distance(transform.position, player.transform.position);

        if(playerDist <= attackRange)
        {
            // attack the player
            rig.velocity = Vector2.zero;
        }
        else if(playerDist <= chaseRange)
        {
            Chase();
        }
        else
        {
            rig.velocity = Vector2.zero;
            if(escaped == true)
            {
                AddXpToPlayer();
                dialogue.ResetDialogue();
                Wander();
            }
            
            
            
         
        }
    }

    public IEnumerator MoveTo(Vector2 targetPosition, System.Action callback, float delay = 0f)
    {
        while (targetPosition != new Vector2(transform.position.x, transform.position.y))
        {
           
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);
        
            yield return null;
        }
        yield return new WaitForSeconds(delay);
        callback();
    }


    public void Wander()
    {
        Vector2 currentPosition = transform.position;
        int roll = Random.Range(0, 4);
       


        if (currentPosition == spawnPosition)
        {
            
            if (roll == 0)
            {
                sr.sprite = upSprite;
               
            } else if (roll == 1)
            {
                sr.sprite = rightSprite;
                
            } else if (roll == 2)
            {
                sr.sprite = downSprite;
                
            } else if (roll == 3)
            {
                sr.sprite = leftSprite;
               
            }
            Vector2 destination = currentPosition + movementDirections[roll];
            StartCoroutine(MoveTo(destination, Wander, Random.Range(2, 5)));
           
        }
        else
        {
            if (currentPosition.y > spawnPosition.y)
            {
                sr.sprite = downSprite;
            } else if (currentPosition.y < spawnPosition.y)
            {
                sr.sprite = upSprite;
            } else if ( currentPosition.x < spawnPosition.x)
            {
                sr.sprite = rightSprite;
            } else if (currentPosition.x > spawnPosition.x)
            {
                sr.sprite = leftSprite;
            }
           
            StartCoroutine(MoveTo(spawnPosition, Wander, Random.Range(2, 5)));
            
        }
    }
    void Chase()
    {

        StopAllCoroutines();
        StartDialogue();
        escaped = true;

        // calculate direction between us and player
        Vector2 dir = (player.transform.position - transform.position).normalized;

        

        //calculate facing direction
        if (dir.magnitude != 0)
            facingDirection = dir;

        UpdateSpriteDirection();
        rig.velocity = dir * moveSpeed;
        

    }



    public void UpdateSpriteDirection()
    {
         if (facingDirection.y > .4)
            sr.sprite = upSprite;
        else if (facingDirection.y < -.4)
            sr.sprite = downSprite;
        else if(facingDirection.x > 0 && (facingDirection.y > -.4 || facingDirection.y < .4)) 
            sr.sprite = rightSprite;
        else if (facingDirection.x < 0 && (facingDirection.y > -.4 || facingDirection.y < .4))
            sr.sprite = leftSprite;
       
        
        
        
    }

    public void AddXpToPlayer ()
    {
        player.AddXp(xpToGive);
        escaped = false;
    }
}
