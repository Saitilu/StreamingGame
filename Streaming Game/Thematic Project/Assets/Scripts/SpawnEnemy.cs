using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public int xMin;
    public int xMax;

    public void Spawn(){
        Vector2 pos = new Vector2 (Random.Range (xMin, xMax), 385);
        Instantiate(enemy, pos, Quaternion.identity);

    }
}
