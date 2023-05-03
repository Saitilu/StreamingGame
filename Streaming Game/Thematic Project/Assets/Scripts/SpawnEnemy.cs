using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    [Header("X Axis Spawn Value")]
    [SerializeField] int xMin;
    [SerializeField] int xMax;

    [Header("Speed")]
    [SerializeField] float speedMin;
    [SerializeField] float speedMax;

    [Header("Direction")]
    [SerializeField] int dirMin;
    [SerializeField] int dirMax;

    [Header("Spawn Times")]
    private float nextSpawnTime;
    [SerializeField] float spawnInterval;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "Game")
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }

    public void Spawn(string viewerName)
    {
        Vector2 pos = new Vector2(Random.Range(xMin, xMax), 6);
        GameObject enemyObj = Instantiate(enemy, pos, Quaternion.identity);
        Enemy enemyScript = enemyObj.GetComponent<Enemy>();
        enemyScript.SetName(viewerName);

        // Set random speed and direction
        float speed = Random.Range(speedMin, speedMax);
        float direction = Random.Range(dirMin, dirMax);
        Vector2 velocity = Quaternion.Euler(0f, 0f, direction) * Vector2.down * speed;

        // Set velocity of balloon
        Rigidbody2D rb2d = enemyObj.GetComponent<Rigidbody2D>();
        rb2d.velocity = velocity;
    }
}
