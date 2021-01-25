using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackeyGuy : MonoBehaviour
{
    [Header("Stats")]
    private bool hacking = true;

    [Header("Sprites")]
   

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
        if (hacking == true)
        {
            an.Play("HackeyGuyLoop");
        }
        else
        {
            an.enabled = false;
        }

    }

}
