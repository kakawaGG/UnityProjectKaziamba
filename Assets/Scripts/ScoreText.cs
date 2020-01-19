using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text scoreText;

    void Update()
    {
        scoreText.text = "Score: " + Main.Score.ToString();
    }
}
