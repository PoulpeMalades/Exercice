using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    GameObject attackBox;
    Animator anim;
    GameObject player;
    public float attackDistance = 3f;
    public float seekDistance = 5f;
    public float speed = 10f;
    public float attackCooldown = 2;
    public bool dying = false;

    private float _attackTimer;
    
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        attackBox = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
    }
    
    
    private void Update()
    {
        _attackTimer += Time.deltaTime;
        
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < attackDistance && _attackTimer >= attackCooldown)
            {
                anim.SetBool("IsAttacking",true);
                Invoke("Attack", 0.6f );
                _attackTimer = 0;
            }
            if (distance <= seekDistance && distance >= attackDistance)
            {
                MoveTowardPlayer();
                anim.SetBool("IsRunning", true);
                anim.SetBool("IsAttacking", false);
            }
            else
            {
                anim.SetBool("IsRunning", false);
            }
        }
    }

    void MoveTowardPlayer()
    {
        if (!dying)
        {
            int direction = player.transform.position.x < transform.position.x ? -1 : 1;
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(6, 6, 1);
                //_spriteRenderer.flipX = false;
            }
            else
            {
                transform.localScale = new Vector3(-6, 6, 1);
                //_spriteRenderer.flipX = true;
            }
            transform.Translate(transform.right * direction * speed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (!dying)
        { 
            int direction = player.transform.position.x < transform.position.x ? -1 : 1;
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(6, 6, 1);
                //_spriteRenderer.flipX = false;
            }
            else
            {
                transform.localScale = new Vector3(-6, 6, 1);
                //_spriteRenderer.flipX = true;
            }
            attackBox.SetActive(true);
            Invoke("EndAttack", 0.1f);
        }
    }

    void EndAttack()
    {
        attackBox.SetActive(false);
        anim.SetBool("IsAttacking", false);
    }
}