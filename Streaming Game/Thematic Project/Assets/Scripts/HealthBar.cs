using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] Sprite[] sprites;

    public void SetHealth(int health) //method to set the current health of the slider
    {
        hp = health;
        GetComponent<SpriteRenderer>().sprite = sprites[(int)(hp/ 2)];
    }
}
