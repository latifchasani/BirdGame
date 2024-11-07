using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreIn : MonoBehaviour
{
    public Text scr;

    public void bestScore(int bestPoint)
    {
        scr.text = bestPoint.ToString();

    }
}
