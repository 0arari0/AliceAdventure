using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBossTime : MonoBehaviour
{
    public Slider timeValueSlider;
    public float timeValue;
    void OnEnable()
    {
        timeValueSlider.maxValue = timeValue;
        timeValueSlider.value = timeValueSlider.maxValue;
    }
    void Update()
    {
        if (GameObject.Find("SpadeMaster(Clone)"))
        {
            timeValueSlider.value = timeValueSlider.value - Time.deltaTime;
        }
        if (timeValueSlider.value <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PlayerGameOver();
        }
    }
}