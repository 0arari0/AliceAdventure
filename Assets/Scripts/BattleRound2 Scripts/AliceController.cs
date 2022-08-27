using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceController : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 3f)]
    float fadeInSpeed; // 투명도가 1f가 되는데 까지 소요되는 시간
    float deltaFadeInSpeed;

    public BubbleController bubbleController;
    public TransformMove transformMove;

    public bool isFadingIn { get; private set; } = false;

    SpriteRenderer spriteRenderer;

    Coroutine _fadeIn = null;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        deltaFadeInSpeed = 1f / (fadeInSpeed * 50f);
    }

    /// <summary>
    /// 단순 호출로 실행
    /// </summary>
    public void FadeInStart()
    {
        if (_fadeIn == null && !isFadingIn)
            _fadeIn = StartCoroutine(CFadeIn());
    }
    public void FadeInStop()
    {
        if (_fadeIn != null)
        {
            StopCoroutine(_fadeIn);
            _fadeIn = null;
            isFadingIn = false;
        }
    }

    /// <summary>
    /// IEnumerator로 실행
    /// </summary>
    /// <returns></returns>
    public IEnumerator CFadeIn()
    {
        isFadingIn = true;
        for (float i = 0f; i <= 1f; i += deltaFadeInSpeed)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.02f);
        }
        isFadingIn = false;
    }
}
