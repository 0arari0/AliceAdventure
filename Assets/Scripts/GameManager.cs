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

    public bool isClear { get; set; } // 해당 Scene을 클리어했는가?

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
    }

    public void LoadScene(string name)
    {
        try
        {
            SceneManager.LoadScene(name);
            _curSceneIdx = _numberOfScene[name];

            isClear = false;
        }
        catch
        {
            Debug.Log("Wrong Scene Name!");
        }
        finally
        {

        }
    }
    public void LoadNextScene()
    {
        try
        {
            _curSceneIdx++;
            if (_curSceneIdx >= sceneNames.Length)
            {
                _curSceneIdx--;
                return;
            }
            SceneManager.LoadScene(sceneNames[_curSceneIdx]);

            isClear = false;
        }
        catch
        {
            Debug.Log("Wrong Scene Index!");
        }
        finally
        {

        }
    }
}
