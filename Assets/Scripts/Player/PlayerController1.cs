using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
public class PlayerController1 : MonoBehaviour
{
    public static PlayerController1 instance;
    
    public float MoveSpeed;
    public float JumpForce;
    
    public float Attack2Cooldown = 2;
    public float AttackCooldown = 1;
    
    
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private float _jumpTimer;
    
    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;
    
    private float _attackTimer;
    private float _attack2Timer;
    //private float Timer = 0f;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
    }
    
    
    void Update()
    {
        bool isJumping = Input.GetButtonDown("Jump");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isAttacking = Input.GetButton("Fire1");
        bool roll = Input.GetKeyDown(KeyCode.Q);
        
        //Horizontal movement
        if (_attackTimer >= AttackCooldown)
        {
            transform.Translate(horizontal * Time.deltaTime * MoveSpeed, 1, 1);
        }
        
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.Translate(movement * (Time.deltaTime * MoveSpeed));
        
        
        // Jump force
        if (isJumping && _rigidBody.linearVelocity.y == 0 )
        {
            _rigidBody.AddForce(Vector2.up * JumpForce);
            _animator.SetBool("Jump", true);
        }

        if (_rigidBody.linearVelocity.y < 0)
        {
            _animator.SetBool("Jump", false);
            _animator.SetBool("Fall", true);
        }
        else
        {
            _animator.SetBool("Fall", false);
        }

        
        // roulade
        if (roll)
        {
            _animator.SetBool("roll", true);
            
            StartCoroutine(Invulnerability());
            
        }
        else
        {
            _animator.SetBool("roll", false);
        }
        
        //Animation update
        _animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
        
        
       
        
        //Flipping sprite
        if (horizontal > 0)
            _spriteRenderer.flipX = false;
        else if (horizontal < 0)
            _spriteRenderer.flipX = true;

        if (currentHealth <= 0)
        {
            _animator.SetBool("Death", true);
        }
        
    }
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 7, true);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(8,7 ,false);
    }
    
    public IEnumerator KnockBack(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            _rigidBody.AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }
    
}
