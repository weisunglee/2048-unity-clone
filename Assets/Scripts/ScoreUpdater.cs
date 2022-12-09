using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreUpdater : MonoBehaviour
{
    private int totalScore = 0;    

    public void UpdateScore(int newScore)
    {
        totalScore += newScore;
        gameObject.GetComponent<Text>().text = totalScore.ToString();        
    }
}
