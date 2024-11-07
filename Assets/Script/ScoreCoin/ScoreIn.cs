using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreIn : MonoBehaviour
{
    public Text scr;

    public void finalUpdateScore(int finalPoint)
    {
        scr.text = finalPoint.ToString();

    }
}
