using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int maxHealth; 
    public int currentHealth;
    public HealthBar healthBar;
    public static int enemyDamage;
    public static int enemyScore;
    public PlayerManager player;
    public AudioSource audioSource;
    public AudioClip enemyHit;
    public AudioClip enemyDeath;
    public Animator anim;
    public static int enemyDeaths;
         
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    } 

    void OnCollisionEnter2D (Collision2D collision) //called when one objects collider makes contact with another
    {
        if (collision.gameObject.name == "Player") //if the collision is with Miguel
        {          
            player.TakeEnemyDamage(enemyDamage);          
        }           
    }

    public void TakeDamage(int damage) //public method so that it can be used in the other script
    {
        anim.SetTrigger("IsHit");
        currentHealth -= damage; //damage is taken from health0
        healthBar.SetHealth(currentHealth);
        //audioSource.clip = enemyHit;
        //audioSource.Play();      

        if (currentHealth <= 0) //if health becomes 0
        {
            Die(); //call the die function
        }
    }

    void Die()
    {
        enemyDeaths++;

        ScoreManager.score += enemyScore; //add enemy score to score manager
        Destroy(gameObject);
    }
}
