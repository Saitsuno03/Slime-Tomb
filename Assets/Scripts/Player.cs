using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    private SpriteRenderer Warrior_Idle_1;
    
    [Header("Health Info")]
    public int healthplayer = 100;
    public TextMeshProUGUI TextHealth;

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;

    bool isMoving;
    bool isDead;
    //bool isAttacking;
    [Header("Teleport Info")]
    public float teleportRange = 2.0f;
    public float minXLimit = -13.0f; 
    public float maxXLimit = 12.0f;
    private int maxteleport = 5;
    

    [Header("Attack Info")]
    [SerializeField] private int attackpower = 20;
    private float attackRange = 1.0f;

    

    private float damagetakenTime = 2f;
    private float damagetakenTime2 = 0f;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Warrior_Idle_1 = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T) && maxteleport > 0) // Change the key or trigger condition as needed
        {
            maxteleport--;
            TeleportPlayer();
        }
        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("Playerattack");
            if (Time.deltaTime == 2)
            {
                anim.Play("Playeridle");
            }
                Attackplayer();
        }
        if (maxteleport < 2)
        {
            maxteleport += 1;
        }
        
    }
void Attackplayer()
    {
        
        anim.Play("Playerattack");
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);

        // Handle the attack logic (e.g., damaging enemies)
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemy2S = enemy.GetComponent<Enemy>();
            if (enemy2S != null)
            {
               
                enemy2S.TakeDamage(attackpower);
            }
        }

    }

    public void TakeHit(int damage)
    {
        if (damagetakenTime2 >= damagetakenTime)
        {
            healthplayer -= damage;
            if (healthplayer > 0)
            { anim.Play("PlayerHurt"); }
            Debug.Log(healthplayer);
            damagetakenTime2 = 0f;
            TextHealth.text = healthplayer.ToString();
        }
        else
        {
            damagetakenTime2 += Time.deltaTime;
        }
    }

    /*public void Die()
    {
        // Implement death behavior (e.g., play death animation, remove enemy from the scene, award points, etc.)
        anim.Play("PlayerDeath");
        
    }*/

    void TeleportPlayer()
    {
        // Calculate a random position within the specified range
        float randomX = transform.position.x + Random.Range(-teleportRange, teleportRange);
        randomX = Mathf.Clamp(randomX, minXLimit, maxXLimit);
        // Set the player's position to the random position
        transform.position = new Vector3(randomX, transform.position.y, transform.position.z);

    }

    void FixedUpdate()
    {
        float horizontalinput = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
        Vector2 movement = new Vector2(horizontalinput, 0);
        
        if (movement.x > 0)
        {
            Warrior_Idle_1.flipX = false; // Not flipped
        }
        else if (movement.x < 0)
        {
            Warrior_Idle_1.flipX = true; // Flipped horizontally
        }
        
        
        if (movement != Vector2.zero)
        {
            rb.velocity = movement * moveSpeed;
            anim.SetBool("isMoving", true);
        }
        else 
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }
}
