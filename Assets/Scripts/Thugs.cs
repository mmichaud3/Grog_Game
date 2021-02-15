using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thugs : MonoBehaviour
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


    void Awake()
    {
        // get the player target
        player = FindObjectOfType<Player>();

        // get the rigidbody component
        rig = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float playerDist = Vector2.Distance(transform.position, player.transform.position);

        if (playerDist <= attackRange)
        {
            // attack the player
            rig.velocity = Vector2.zero;
        }
        else if (playerDist <= chaseRange)
        {
            Chase();
        }
        else
        {
            rig.velocity = Vector2.zero;
            if (escaped == true)
                AddXpToPlayer();
        }
    }

    void Chase()
    {

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
        else if (facingDirection.x > 0 && (facingDirection.y > -.4 || facingDirection.y < .4))
            sr.sprite = rightSprite;
        else if (facingDirection.x < 0 && (facingDirection.y > -.4 || facingDirection.y < .4))
            sr.sprite = leftSprite;




    }

    public void AddXpToPlayer()
    {
        player.AddXp(xpToGive);
        escaped = false;
    }
}
