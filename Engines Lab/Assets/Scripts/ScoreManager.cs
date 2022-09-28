using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void ChangeScore()
    {
        score += 1;
        Debug.Log(score);
    }
}
