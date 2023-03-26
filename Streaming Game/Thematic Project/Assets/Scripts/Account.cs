using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account : MonoBehaviour
{
    public string AccountName;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
