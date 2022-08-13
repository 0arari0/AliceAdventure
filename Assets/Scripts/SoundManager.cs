using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    /* 20220811 작성자 : 김두현
     * 외부에서 사운드 재생하는법 - AAA 는 enum 으로 선언된 하위 객체
     * SoundManager.instance.PlayBgm(SoundManager.BGM_Name_.AAA); - BGM 재생
     * SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.AAA); - SFX 재생
     */
    
    public static SoundManager instance;
    public AudioSource bgmPlayer;
    public AudioSource sfxPlayer;
    public AudioClip[] bgmArr;
    public AudioClip[] sfxArr;
    public enum BGM_Name_
    {
        MainMenu, Round1, Round2
    }
    public enum SFX_Name_
    {
        ButtonClick
    }
    void Awake()
    {
        // MainMenu 씬에서 생성 이후에 모든 씬에서 사운드를 재생할 수 있어야 하므로 파괴되지 않도록 하였음
        ApplySingletonPattern();
    }
    void Start()
    {
        
    }
    public void PlayBgm(BGM_Name_ _bgmName)
    {
        // bgmPlayer 는 BGM 을 재생하는 오브젝트라서 한번에 하나의 사운드만 출력하기에 Play() 함수 사용
        bgmPlayer.clip = bgmArr[(int)_bgmName];
        bgmPlayer.Play();
    }
    public void PlaySfx(SFX_Name_ _sfxName)
    {
        // sfxPlayer 는 SFX 를 재생하는 오브젝트라서 동시에 여러종류의 사운드를 출력하기에 PlayOneShot() 함수 사용
        sfxPlayer.PlayOneShot(sfxArr[(int)_sfxName]);
    }

    /// <summary>
    /// 20220813 작성자 : 김희재
    /// 싱글톤 패턴으로 구현하였으므로 아래와 같이 작성하면 게임 오브젝트가 중복 생성되는 것을 방지
    /// Awake()에서 구현하는 것이 바람직해 보임(최초 한 번만 실행되면 되기 때문)
    /// </summary>
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