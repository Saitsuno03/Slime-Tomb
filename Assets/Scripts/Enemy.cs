using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rbs;
    private Animator animem;
    private Transform player;
    private SpriteRenderer SlimeBlue_Idle1;

    public int hitFrame = 4; // The frame number at which the slime should hit the player.

    public bool hasHitPlayer = false;

    public int hittime = 1;
    public int health = 100;
    private float attackRange = 1.0f;



    [Header("Move Info")]
    [SerializeField] private float movespeedenemy;
    float stopDistance = 2.0f;



    private int enemyattackpower = 2;
    private int currenthealth;

    //private float damagetakenTime = 0.7f;
    //private float damagetakenTime2 = 0f;
    // Start is called before the first frame update
    public void Start()
    {
        rbs = GetComponent<Rigidbody2D>();
        animem = GetComponent<Animator>();
        SlimeBlue_Idle1 = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has a "Player" tag

        currenthealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate the direction towards the player
            Vector2 moveDirection = (player.position - transform.position).normalized;
            if (moveDirection.x > 0)
            {
                SlimeBlue_Idle1.flipX = true; // Not flipped
            }
            else if (moveDirection.x < 0)
            {
                SlimeBlue_Idle1.flipX = false; // Flipped horizontally
            }

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            // Move the enemy in the direction of the player
            if (distanceToPlayer > stopDistance)
            {
                // Move the enemy in the direction of the player
                rbs.velocity = moveDirection * movespeedenemy;
            }
            else
            {
                // Stop the enemy when it's close to the player
                rbs.velocity = Vector2.zero;
                
                Attackenemy();
            }
        }
    }

    void Attackenemy()
    {
        
        animem.Play("enemyattack");


        
        
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange);

            // Handle the attack logic (e.g., damaging enemies)
            foreach (Collider2D playerhit in hitPlayer)
            {
                Player player = playerhit.GetComponent<Player>();
            if (player != null)
            {


               
                 player.TakeHit(enemyattackpower); 
                


            }
            }
        

    }
    
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        Debug.Log(currenthealth);
        if (currenthealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Implement death behavior (e.g., play death animation, remove enemy from the scene, award points, etc.)
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            animem.SetBool("isAttacking", false);
        }
    }

}
