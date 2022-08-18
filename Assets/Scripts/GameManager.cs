using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    string[] sceneNames; // Scene 순서대로 삽입
    Dictionary<string, int> _numberOfScene; // Scene 넘버 저장
    int _curSceneIdx;

    public int stageScore { get; private set; }

    public bool isClear { get; set; } // 해당 Scene을 클리어했는가?

    StripeFade stripeFade;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        _numberOfScene = new Dictionary<string, int>();
        for (int i = 0; i < sceneNames.Length; i++)
            _numberOfScene.Add(sceneNames[i], i);
        _curSceneIdx = 0;

        isClear = false;
        InitializeScore();

        stripeFade = StripeFade.instance;
    }

    public void InitializeScore()
    {
        stageScore = 0;
    }
    public void AddScore(int score)
    {
        if (score < 0) return;
        stageScore += score;
    }

    public void LoadScene(string name) // 함수로 실행시키는 경우 동시 실행 가능
    {
        StartCoroutine(CorLoadScene(name));
    }
    public void LoadNextScene() // 함수로 실행시키는 경우 동시 실행 가능
    {
        StartCoroutine(CorLoadNextScene());
    }
    public IEnumerator CorLoadScene(string name) // 직접 코루틴을 실행시키는 경우 해당 시간을 기다려야 다음 코드 실행
    {
        yield return stripeFade.StartCoroutine(stripeFade.FadeOut());
        
        SceneManager.LoadScene(name);
        _curSceneIdx = _numberOfScene[name];
        isClear = false;

        yield return stripeFade.StartCoroutine(stripeFade.FadeIn());
    }
    public IEnumerator CorLoadNextScene() // 직접 코루틴을 실행시키는 경우 해당 시간을 기다려야 다음 코드 실행
    {
        yield return stripeFade.StartCoroutine(stripeFade.FadeOut());

        _curSceneIdx++;
        if (_curSceneIdx >= sceneNames.Length)
        {
            _curSceneIdx--;
            yield break;
        }
        SceneManager.LoadScene(sceneNames[_curSceneIdx]);
        isClear = false;

        yield return stripeFade.StartCoroutine(stripeFade.FadeIn());
    }
}
