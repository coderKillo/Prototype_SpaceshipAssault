using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int m_score = 0;

    static private Scoreboard _instance;
    static public Scoreboard instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {
        SetScore();
    }

    void Update()
    {

    }

    public void IncreaseScore(int points)
    {
        m_score += points;
        SetScore();
    }

    private void SetScore()
    {
        scoreText.SetText(m_score.ToString());
    }

}
