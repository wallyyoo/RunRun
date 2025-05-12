using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaderByElement : MonoBehaviour

// ���� Ÿ��Ʋ�� ��ư�� �ð��ʸ� �ΰ� ������ �����ϵ���
// ȿ���� ������ ��ũ��Ʈ �Դϴ�.
{
    [Header("Ÿ��Ʋ ����")]
    public CanvasGroup titleGroup;
    public float titleFadeDelay = 0.5f;
    public float titleFadeDuration = 1.5f;

    [Header("��ư ����")]
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

