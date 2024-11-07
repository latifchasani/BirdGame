using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scr;
    /*public Controller controller;
    int score = 0;*//*
    public Text scr;

    int score = 0;

    void Start()
    {
        Debug.Log("start asd");
        scr.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
        score += 1;
        scr.text = score.ToString();
        *//*controller.incrementPoint(score);*//*
        Debug.Log("update asd " +score);
    }*/

    public void updateScore(int point) 
    {
        scr.text = point.ToString();
        
    }
}
