﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject canvas;
    [SerializeField] Transform viewerName;

    [Header("Health Bar")]
    [SerializeField] static int maxHealth;
    private int currentHealth;
    [SerializeField] static int enemyDamage;
    [SerializeField] PlayerManager playerScript;

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

    float timeTillNextShot;
    Transform playerTransform;

    [SerializeField] Vector3 shotDirection;

    public void SetName(string newName)
    {
        TextMeshPro text = viewerName.GetComponent<TextMeshPro>();
        text.text = newName;
    }
    void Start()
    {
        currentHealth = maxHealth;

        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerManager>();

        rb2d = GetComponent<Rigidbody2D>();
        spinSpeed = Random.Range(-maxSpinSpeed, maxSpinSpeed);

        //viewerName.transform.SetParent(transform.Find("Enemy Canvas"));
        //Destroy(canvas);

        timeTillNextShot = Random.RandomRange(1.5f, 3.5f);
    }

    void Update()
    {
        ScoreManager.score = enemyDeaths * 100;

        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);

        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

        // Check if the asteroid is outside of the screen bounds
        if (screenPos.y < 0f)
        {
            Destroy(gameObject);
        }

        //move name
        viewerName.position = transform.position + new Vector3(0, .75f, 0);
        viewerName.rotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        timeTillNextShot -= Time.fixedDeltaTime;
        if (timeTillNextShot <= 0)
        {
            timeTillNextShot = Random.RandomRange(1.5f, 3.5f);
            Shoot();
        }
    }

    private void Shoot()
    {
        //shotDirection = playerTransform.position - transform.position ;
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransform.position - transform.position);
        //if (hit.collider.tag == "Enemy")
        //{
        //    Debug.Log(hit.collider.tag);
        //    return;
        //}

        //shot position
        shotDirection = (playerTransform.position - transform.position).normalized * 1.2f;
        Vector3 shooter = transform.position + shotDirection;
        //shot rotation
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, shotDirection);
        //make shot
        GameObject shot = Instantiate(bulletPrefab, shooter, rotation);
        shot.GetComponent<Bullet>().speed = 5;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") //if the collision is with a bullet
        {
            Destroy(collision.gameObject);
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

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        viewerName.gameObject.SetActive(false);
        timeTillNextShot = 10f;

        //play hit audio
        audioSource.clip = enemyDeath;
        audioSource.Play();
        Invoke("Death", audioSource.clip.length);
        

        ScoreManager.score += enemyScore; //add enemy score to score manager
        //Destroy(gameObject);
    }
    void Death()
    {
        Destroy(gameObject);
    }
}
