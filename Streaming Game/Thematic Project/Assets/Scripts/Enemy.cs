using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] static int maxHealth;
    private int currentHealth;
    [SerializeField] static int enemyDamage;
    [SerializeField] PlayerManager player;

    [Header("Score")]
    [SerializeField] static int enemyScore;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip enemyHit;
    [SerializeField] AudioClip enemyDeath;

    [Header("Animation")]
    [SerializeField] Animator anim;

    [Header("Movement")]
    [SerializeField] float speed;

    [Header("Spinning")]
    [SerializeField] float maxSpinSpeed;
    private Rigidbody2D rb2d;
    private float spinSpeed;

    public static int enemyDeaths;

    void Start()
    {
        currentHealth = maxHealth;

        player = GameObject.Find("Player").GetComponent<PlayerManager>();

        rb2d = GetComponent<Rigidbody2D>();
        spinSpeed = Random.Range(-maxSpinSpeed, maxSpinSpeed);
    }

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);

        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

        // Check if the asteroid is outside of the screen bounds
        if (screenPos.y < 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) //called when one objects collider makes contact with another
    {
        if (collision.gameObject.tag == "Bullet") //if the collision is with the player
        {
            //player.TakeEnemyDamage(enemyDamage);
            Die();
        }
    }

    public void TakeDamage(int damage) //public method so that it can be used in the other script
    {
        anim.SetTrigger("IsHit");
        currentHealth -= damage; //damage is taken from health0

        //play hit audio
        audioSource.clip = enemyHit;
        audioSource.Play();

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
