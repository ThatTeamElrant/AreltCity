﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    public float moveSpeed;
    private float currentMoveSpeed;
    public float diagonalMoveModifier;
    private Animator anim;
    private Rigidbody2D myRigidbody;

    private bool playerMoving;
    public Vector2 lastMove;
    public Joystick joystick;
    private static bool PlayerExists;
    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    // Start is called before the first frame update
    void Start()
    {        
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        if(!PlayerExists)
        {
            PlayerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else {
            Destroy(gameObject);
        }

    }

    public void AttackFunction()
    {

        attackTimeCounter = attackTime;
        attacking = true;
        myRigidbody.velocity = Vector2.zero;
        anim.SetBool("Attack", true);
    }

    // Update is called once per frame
    void Update()
    {
        playerMoving = false;

        if (!attacking)
        {

            if (joystick.Horizontal > 0.5f || joystick.Horizontal < -0.5f)
            {
                //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                myRigidbody.velocity = new Vector2(joystick.Horizontal * currentMoveSpeed, myRigidbody.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(joystick.Horizontal, 0f);
            }

            if (joystick.Vertical > 0.5f || joystick.Vertical < -0.5f)
            {
                //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, joystick.Vertical * currentMoveSpeed);
                playerMoving = true;
                lastMove = new Vector2(0f, joystick.Vertical);
            }

            if (joystick.Horizontal < 0.5f && (joystick.Horizontal) > -0.5f)
            {
                myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            };


            if (joystick.Vertical < 0.5f && (joystick.Vertical) > -0.5f)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AttackFunction();
            }



            if (Mathf.Abs(joystick.Horizontal) > 0.5f && Mathf.Abs(joystick.Vertical) > 0.5f){
                currentMoveSpeed = moveSpeed * diagonalMoveModifier;
            }
            else
            {
                currentMoveSpeed = moveSpeed;
            }


        }

        if(attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }

        if (attackTimeCounter < 0)
        {
            attacking = false;
            anim.SetBool("Attack", false);
        }

        anim.SetFloat("MoveX", joystick.Horizontal);
        anim.SetFloat("MoveY", joystick.Vertical);
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

    }
}
