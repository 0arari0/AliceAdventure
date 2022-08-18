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
        scoreValueText.text = GameManager.instance.stageScore.ToString();
        RefreshHeartNum();
    }

    void RefreshHeartNum()
    {
        for (int i = 0; i < 3; i++)
            heartsImage[i].sprite = emptyHeart;
        for (int i = 0; i < Player.instance.playerHp; i++)
            heartsImage[i].sprite = fullHeart;
    }
}