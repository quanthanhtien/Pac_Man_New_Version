using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Point_Finish : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score");
        scoreText.text = score.ToString().PadLeft(2, '0');
        Debug.Log(score);
        PlayerPrefs.DeleteAll();
    }

}
