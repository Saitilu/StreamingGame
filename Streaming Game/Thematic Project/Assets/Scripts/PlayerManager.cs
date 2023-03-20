using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int maxHealth = 100;
    public static int currentHealth;
    public Animator anim; 
    public HealthBar healthBar; //reference to the health bar script
    public AudioSource audioSource;
    public AudioSource dialogueSource;
    public AudioClip hit;
    public AudioClip death;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; //set the current health the max health
        healthBar.SetMaxHealth(maxHealth); //set the health bars max health through the SetMaxHealth method
    }

    public void TakeEnemyDamage(int damage)
    {
        //play hit audio
        audioSource.clip = hit;
        audioSource.Play();

        //play hit animation
        anim.SetTrigger("IsHit");
        currentHealth -= damage; //damage is taken from health

        healthBar.SetHealth(currentHealth); //set the healthbar health to the current health

        if (currentHealth <= 0) //if health becomes 0
        {
            Die(); //call the die function
        }
    }

    void Die()
    {
        //play death audio
        audioSource.clip = death;
        audioSource.Play();

        //load game over scene
        SceneManager.LoadScene("GAMEOVER");
    }
}
