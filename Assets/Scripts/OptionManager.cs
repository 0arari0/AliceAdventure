using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    /* 20220811 작성자 : 김두현
     * 옵션창에 관련된 스크립트
     */
    public static OptionManager instance;
    public GameObject optionWindow;
    public GameObject optionGrayWindow;
    public Slider bgmSlider;
    public Text bgmValueText;
    public Slider sfxSlider;
    public Text sfxValueText;
    bool isBattleScene = false;

    void Awake()
    {
        // 여러 씬에서 옵션창을 사용할 수 있어야하므로 파괴되지 않도록 설정하였다.
        // AddListener 를 사용하여 bgmSlider, sfxSlider 의 value 가 변하면 BGM, SFX 볼륨조절 함수를 실행하도록 설정하였다.
        ApplySingletonPattern();
        bgmSlider.onValueChanged.AddListener(delegate { SetBgmVolumeValue(); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSfxVolumeValue(); });
    }

    void SetBgmVolumeValue()
    {
        SoundManager.instance.bgmPlayer.volume = bgmSlider.value;
        bgmValueText.text = ((int)(bgmSlider.value * 100)).ToString() + "%";
    }

    void SetSfxVolumeValue()
    {
        SoundManager.instance.sfxPlayer.volume = sfxSlider.value;
        sfxValueText.text = ((int)(sfxSlider.value * 100)).ToString() + "%";
    }

    public void SelectOptionWindowClose()
    {
        // 옵션 윈도우 닫을때 사용
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        optionGrayWindow.SetActive(false);
        optionWindow.SetActive(false);
    }

    public void SelectOptionWindowOpen()
    {
        // 옵션 윈도우 열때 사용
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        optionGrayWindow.SetActive(true);
        optionWindow.SetActive(true);
    }

    void ApplySingletonPattern()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}