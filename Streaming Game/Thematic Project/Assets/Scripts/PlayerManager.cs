using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int maxHealth = 100;
    public static int currentHealth;
    //[SerializeField] Animator anim;
    [SerializeField] HealthBar healthBar; //reference to the health bar script
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource dialogueSource;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip death;

    Rigidbody2D rigidbody;
    float turnSpeed = 150;
    float speed = 75;

    [SerializeField] GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth; //set the current health the max health
        healthBar.SetMaxHealth(maxHealth); //set the health bars max health through the SetMaxHealth method
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Transform shooter = transform.Find("Shooter");
            Instantiate(bulletPrefab, shooter.position, transform.rotation * Quaternion.Euler(0, 0, 180));
        }
    }

    private void FixedUpdate()
    {
        //turn
        float turn = -Input.GetAxis("Horizontal") * Time.fixedDeltaTime * turnSpeed;
        rigidbody.rotation += turn;

        //movement force
        float force = Mathf.Max(0, Input.GetAxis("Vertical")) * Time.fixedDeltaTime * speed;
        rigidbody.AddForce(-transform.up * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") //if the collision is with a bullet
        {
            Destroy(collision.gameObject);
            TakeEnemyDamage(5);
        }
    }

    public void TakeEnemyDamage(int damage)
    {
        //play hit audio
        audioSource.clip = hit;
        audioSource.Play();

        //play hit animation
        //anim.SetTrigger("IsHit");
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
