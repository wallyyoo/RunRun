using System.Collections;
using UnityEngine;
using TMPro;

public class EndingSceneController : MonoBehaviour
{
    [Header("Credits 설정")]
    public TextMeshProUGUI[] creditNames; // 크레딧 이름들 (최대 5명)
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    [Header("배경 스프라이트")]
    public SpriteRenderer backgroundSpriteRenderer; // SpriteRenderer에 적용
    public Sprite shakingCastle;
    public Sprite peacefulEnding;

    [Header("Thank you 메시지")]
    public TextMeshProUGUI thanksText;

    [Header("카메라 흔들림")]
    public Transform cameraTransform;
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 2f;

    private void Start()
    {
        // 초기 상태 설정
        thanksText.gameObject.SetActive(false);

        foreach (var t in creditNames)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, 0); // 알파값 0으로
        }

        // 시작 배경 설정
        backgroundSpriteRenderer.sprite = shakingCastle;

        StartCoroutine(PlayCredits());
    }

    IEnumerator PlayCredits()
    {
        // 1. 흔들기 시작 (병렬 실행)
        Coroutine shakeRoutine = StartCoroutine(ShakeCameraLoop());

        // 2. 크레딧 하나씩 보여주기
        foreach (var name in creditNames)
        {
            yield return StartCoroutine(FadeText(name, 0f, 1f));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeText(name, 1f, 0f));
        }

        // 3. 흔들기 멈추기
        StopCoroutine(shakeRoutine);

        // 4. 배경 변경
        backgroundSpriteRenderer.sprite = peacefulEnding;
        yield return new WaitForSeconds(1f);

        // 5. Thank you 메시지
        thanksText.gameObject.SetActive(true);
        yield return StartCoroutine(FadeText(thanksText, 0f, 1f));
    }

    IEnumerator FadeText(TextMeshProUGUI text, float from, float to)
    {
        float time = 0f;
        Color c = text.color;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            c.a = Mathf.Lerp(from, to, t);
            text.color = c;
            time += Time.deltaTime;
            yield return null;
        }

        c.a = to;
        text.color = c;
    }

    IEnumerator ShakeCamera(float duration)
    {
        Vector3 originalPos = cameraTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
            float offsetY = Random.Range(-1f, 1f) * shakeIntensity;
            cameraTransform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPos;
    }
    IEnumerator ShakeCameraLoop()
    {
        Vector3 originalPos = cameraTransform.localPosition;

        while (true) // 무한 흔들기
        {
            float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
            float offsetY = Random.Range(-1f, 1f) * shakeIntensity;
            cameraTransform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);
            yield return null;
        }
    }
}
