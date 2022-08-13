using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    /* 20220811 작성자 : 김두현
     * 배틀씬에서 esc를 누르면 옵션창을 띄우기위한 스크립트
     */

    public GameObject optionWindow;
    public GameObject optionGrayWindow;
    public Slider bgmSlider;
    public Text bgmValueText;
    public Slider sfxSlider;
    public Text sfxValueText;
    bool isBattleScene = false;
    void Start()
    {
        // 여러 씬에서 옵션창을 사용할 수 있어야하므로 파괴되지 않도록 설정하였다.
        // AddListener 를 사용하여 bgmSlider, sfxSlider 의 value 가 변하면 자동으로 특정 함수를 실행하도록 설정하였다.
        DontDestroyOnLoad(this.gameObject);
        bgmSlider.onValueChanged.AddListener(delegate { SetBgmVolumeValue(); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSfxVolumeValue(); });
    }
    void Update()
    {
        isBattleScene = (SceneManager.GetActiveScene().name == "BattleRound1" || SceneManager.GetActiveScene().name == "BattleRound2") ? true : false;
        if (isBattleScene && Input.GetKeyDown(KeyCode.Escape))
        {
            optionGrayWindow.SetActive(true);
            optionWindow.SetActive(true);
        }
    }

    void SetBgmVolumeValue()
    {
        SoundManager.instance.bgmPlayer.volume = bgmSlider.value;
    }

    void SetSfxVolumeValue()
    {
        SoundManager.instance.sfxPlayer.volume = sfxSlider.value;
    }

    public void SelectOptionWindowClose()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        optionGrayWindow.SetActive(false);
        optionWindow.SetActive(false);
    }

    public void SelectOptionWIndowOpen()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        optionGrayWindow.SetActive(true);
        optionWindow.SetActive(true);
    }
}