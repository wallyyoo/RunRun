using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaderByElement : MonoBehaviour

// 메인 타이틀과 버튼이 시간초를 두고 서서히 등장하도록
// 효과를 설정한 스크립트 입니다.
{
    [Header("타이틀 설정")]
    public CanvasGroup titleGroup;
    public float titleFadeDelay = 0.5f;
    public float titleFadeDuration = 1.5f;

    [Header("버튼 설정")]
    public CanvasGroup buttonGroup;
    public float buttonFadeDelay = 2f;
    public float buttonFadeDuration = 1.5f;

    void Start()
    {
        if (titleGroup != null)
        {
            titleGroup.alpha = 0f;
            titleGroup.gameObject.SetActive(true);
            StartCoroutine(FadeCanvasGroup(titleGroup, titleFadeDelay, titleFadeDuration));
        }

        if (buttonGroup != null)
        {
            buttonGroup.alpha = 0f;
            buttonGroup.gameObject.SetActive(true);
            StartCoroutine(FadeCanvasGroup(buttonGroup, buttonFadeDelay, buttonFadeDuration));
        }
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float delay, float duration)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            yield return null;
        }

        cg.alpha = 1f;
    }
}

