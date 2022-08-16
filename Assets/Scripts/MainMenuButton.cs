using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{

    public GameObject howPlayWindow; // 조작법 윈도우
    public GameObject howPlayGrayWindow;
    public void SelectGameStartButton()
    {
        // 메인 메뉴에서 게임 시작 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene("BattleRound1");
    }

    public void SelectHowPlayButton()
    {
        // 메인 메뉴에서 조작 방법 버튼 눌렀을 때 실행
        // 조작 방법 창에서 닫기 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        howPlayGrayWindow.SetActive(!howPlayGrayWindow.activeSelf);
        howPlayWindow.SetActive(!howPlayWindow.activeSelf);
    }

    public void SelectOptionButtonOpen()
    {
        // 메인 메뉴에서 옵션 버튼 눌렀을 때 실행
        GameObject.Find("OptionCanvas").GetComponent<OptionManager>().SelectOptionWindowOpen();
    }

    public void SelectGameExitButton()
    {
        // 메인 메뉴에서 게임 종료 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        Application.Quit();
    }

    /// <summary>
    /// 작성자: 김희재
    /// 작성일자: 20220815 21:20
    /// 여기 밑으로는 제가 임의로 작성했습니다.
    /// 지우셔도 되고 고치셔도 됩니다^^
    /// </summary>

    [SerializeField]
    GameObject titleImage;

    void Awake()
    {
        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        while (true)
        {
            for (int i = 0; i <= 40; i++)
            {
                titleImage.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, i/4f);
                yield return null;
            }
            for (int i = 40; i >= -40; i--)
            {
                titleImage.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, i/4f);
                yield return null;
            }
            for (int i = -40; i <= 0; i++)
            {
                titleImage.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, i/4f);
                yield return null;
            }
        }
    }
}