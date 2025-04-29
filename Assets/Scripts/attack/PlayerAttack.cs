using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private float timeBtwAttacks = 1f;
    
    
    
    private RaycastHit2D [] hits;

    private Animator _animator;

    private float attackTimerCounter;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isAttacking = Input.GetButton("Fire1");
        bool roll = Input.GetKeyDown(KeyCode.Q);
        
        
        if (isAttacking && attackTimerCounter >= timeBtwAttacks && !roll)
        {
            attackTimerCounter = 0f;
            
            Attack();
            _animator.SetBool("attack",true);
        }

        else 
        {
            _animator.SetBool("attack", false);
        }
        
        attackTimerCounter += Time.deltaTime;
        
    }

    private void Attack()
    {
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

            if (iDamageable != null)
            {
                iDamageable.Damage(damageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
    
    
}
