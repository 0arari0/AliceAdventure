using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndHeart : MonoBehaviour
{
    public Text scoreValueText;
    public Image[] heartsImage;
    GameObject player;
    int score;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        scoreValueText.text = score.ToString();
        RefreshHeartNum();
    }
    public void AddScore(int _score)
    {
        score += _score;
    }
    void RefreshHeartNum()
    {
        for (int i = 0; i < 3; i++)
            heartsImage[i].sprite = emptyHeart;
        for (int i = 0; i < player.GetComponent<Player>().playerHp; i++)
            heartsImage[i].sprite = fullHeart;
    }
}