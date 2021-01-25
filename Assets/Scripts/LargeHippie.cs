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
