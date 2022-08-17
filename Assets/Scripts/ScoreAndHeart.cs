using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndHeart : MonoBehaviour
{
    public Text scoreValueText;
    public Image[] heartsImage;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    void Update()
    {
        scoreValueText.text = Player.instance.stageScore.ToString();
        RefreshHeartNum();
    }

    public void InitScore()
    {
        Player.instance.stageScore = 0;
    }

    public void AddScore(int _score)
    {
        Player.instance.stageScore += _score;
    }
    void RefreshHeartNum()
    {
        for (int i = 0; i < 3; i++)
            heartsImage[i].sprite = emptyHeart;
        for (int i = 0; i < Player.instance.playerHp; i++)
            heartsImage[i].sprite = fullHeart;
    }
}