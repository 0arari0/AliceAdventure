using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBossTime : MonoBehaviour
{
    public Slider timeValueSlider;
    [SerializeField]
    [Range(20f, 120f)]
    float timeValue;

    public void TimeValueSliderOn() // 1라 보스 남은 시간 바 활성화
    {
        if (!timeValueSlider.gameObject.activeSelf)
        {
            timeValueSlider.gameObject.SetActive(true);
            timeValueSlider.maxValue = timeValue;
            timeValueSlider.value = timeValueSlider.maxValue;
        }
    }
    public void TimeValueSliderOff() // 1라 보스 남은 시간 바 비활성화
    {
        if (timeValueSlider.gameObject.activeSelf)
            timeValueSlider.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        TimeValueSliderOff();
    }
    void Update()
    {
        if (GameObject.Find("SpadeMaster(Clone)"))
        {
            timeValueSlider.value = timeValueSlider.value - Time.deltaTime;
        }
        if (timeValueSlider.value <= 0)
        {
            Player.instance.PlayerGameOver();
        }
    }
}