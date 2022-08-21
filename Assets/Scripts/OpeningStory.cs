using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningStory : MonoBehaviour
{
    public float nextTime;
    public GameObject skipButton;
    public Image[] storyImages;
    public Image[] storyArrows;
    int storyPicNum = 0;
    int storyArrowNum = 0;

    void OnEnable()
    {
        storyPicNum = 0;
        storyArrowNum = 0;
        SoundManager.instance.PlayBgm(SoundManager.BGM_Name_.OpeningStory);
        skipButton.GetComponent<Button>().enabled = true;
    }

    public void GoToRound1()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        skipButton.GetComponent<Button>().enabled = false;
        Time.timeScale = 1f;
        SoundManager.instance.bgmPlayer.Stop();
        GameManager.instance.StartCoroutine(GameManager.instance.CorLoadNextScene());
    }

    void FixedUpdate()
    {
        if (storyPicNum >= storyImages.Length) return;
        if (storyArrowNum >= storyArrows.Length) return;
        if (storyArrowNum == 1) skipButton.SetActive(true);

        if (storyImages[storyPicNum].color.a < 1)
        {
            storyImages[storyPicNum].color = new Color(1, 1, 1, storyImages[storyPicNum].color.a + Time.deltaTime * (1f / nextTime));
        }
        if (storyArrows[storyArrowNum].color.a < 1)
        {
            storyArrows[storyArrowNum].color = new Color(1, 1, 1, storyArrows[storyArrowNum].color.a + Time.deltaTime * (1f / nextTime));
        }
        if (storyImages[storyPicNum].color.a >= 1)
        {
            storyPicNum++;
        }
        if (storyArrows[storyArrowNum].color.a >= 1)
        {
            storyArrowNum++;
        }
    }
}