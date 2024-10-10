using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private Player player;
    private Enemy enemy;
    public Transform spawnposplayer;
    public Transform spawnposenemy;

    public GameObject Enemy; // Reference to the enemy prefab.
    public float spawnInterval = 5.0f; // Time between enemy spawns.
    private float timeSinceLastSpawn = 0.0f;
    
    
    
  
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        enemy = GameObject.FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentscene = SceneManager.GetActiveScene();
        if (currentscene.name == "Game")
        {
            startspawning();
        }

        if (player.healthplayer <= 0)
        {
            Time.timeScale = 0;
            
            Die();
        }
    }
    public void startspawning()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            // Instantiate a new enemy with health.
            GameObject newEnemy = Instantiate(Enemy, spawnposenemy.transform.position, Quaternion.identity);

            // Attach the enemy health script to the clone.
            Enemy enemyHealth = newEnemy.GetComponent<Enemy>();

            if (enemyHealth != null)
            {
                enemyHealth.Start(); // Initialize the health component.
            }

            // Reset the timer.
            timeSinceLastSpawn = 0.0f;
        }
    }
        void Die()
    {
        
        
        RestartGame();
       // enemy.animationStop();
    }
    void RestartGame()
    {

        StartCoroutine(RestartGameAfterDelay(1f));
        player.healthplayer = 100;
        player.transform.position = spawnposplayer.position;
        enemy.transform.position = spawnposenemy.position;

        Time.timeScale = 1;
        
    }

    IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reload the current scene to restart the game.
        SceneManager.LoadScene("StartGame");
    }
}
