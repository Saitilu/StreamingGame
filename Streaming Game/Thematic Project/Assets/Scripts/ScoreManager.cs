using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public TextMeshProUGUI UIScore;
    [SerializeField] GameObject canvas;

    private void Start()
    {
        DontDestroyOnLoad(canvas);
    }
    // Update is called once per frame
    void Update()
    {
        UIScore.text = "SCORE: " + score.ToString(); //display it in the UI text
        if (SceneManager.GetActiveScene().name == "InputAccount")
            SceneManager.MoveGameObjectToScene(canvas, SceneManager.GetActiveScene());
    }
}
