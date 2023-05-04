using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnPlay()
    {
        //play hit audio
        audioSource.clip = sound;
        audioSource.Play();
        Invoke("Die", audioSource.clip.length);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
