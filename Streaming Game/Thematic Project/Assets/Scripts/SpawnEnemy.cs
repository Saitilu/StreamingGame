using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;

    [Header("X Axis Spawn Value")]
    public int xMin;
    public int xMax;

    [Header("Speed")]
    public int speedMin;
    public int speedMax;

    [Header("Direction")]
    public int dirMin;
    public int dirMax;

    [Header("Spawn Times")]
    private float nextSpawnTime;
    public float spawnInterval;
    
    void Update(){
        if (Time.time > nextSpawnTime)
        {
            Spawn();
            nextSpawnTime = Time.time + spawnInterval;
        }      
    }

    public void Spawn(){
        Vector2 pos = new Vector2 (Random.Range (xMin, xMax), 6);
        GameObject enemyObj = Instantiate(enemy, pos, Quaternion.identity);

        // Set random speed and direction
        float speed = Random.Range(speedMin, speedMax);
        float direction = Random.Range(dirMin, dirMax);
        Vector2 velocity = Quaternion.Euler(0f, 0f, direction) * Vector2.down * speed;

        // Set velocity of asteroid
        Rigidbody2D rb2d = enemyObj.GetComponent<Rigidbody2D>();
        rb2d.velocity = velocity;
    }
}
