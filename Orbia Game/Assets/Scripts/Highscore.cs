using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    
    public Text highScore;


    private void Start()
    {
        highScore.text = "Highscore : "+PlayerPrefs.GetInt("highScore");
       
    }
}
