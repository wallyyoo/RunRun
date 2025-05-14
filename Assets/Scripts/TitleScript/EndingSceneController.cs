using System.Collections;
using UnityEngine;
using TMPro;

public class EndingSceneController : MonoBehaviour
{
    [Header("Credits ����")]
    public TextMeshProUGUI[] creditNames; // ũ���� �̸��� (�ִ� 5��)
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    [Header("��� ��������Ʈ")]
    public SpriteRenderer backgroundSpriteRenderer; // SpriteRenderer�� ����
    public Sprite shakingCastle;
    public Sprite peacefulEnding;

    [Header("Thank you �޽���")]
    public TextMeshProUGUI thanksText;

    [Header("ī�޶� ��鸲")]
    public Transform cameraTransform;
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 2f;

    private void Start()
    {
        // �ʱ� ���� ����
        thanksText.gameObject.SetActive(false);

        foreach (var t in creditNames)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, 0); // ���İ� 0����
        }

        // ���� ��� ����
        backgroundSpriteRenderer.sprite = shakingCastle;

        StartCoroutine(PlayCredits());
    }

    IEnumerator PlayCredits()
    {
        // 1. ���� ���� (���� ����)
        Coroutine shakeRoutine = StartCoroutine(ShakeCameraLoop());

        // 2. ũ���� �ϳ��� �����ֱ�
        foreach (var name in creditNames)
        {
            yield return StartCoroutine(FadeText(name, 0f, 1f));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeText(name, 1f, 0f));
        }

        // 3. ���� ���߱�
        StopCoroutine(shakeRoutine);

        // 4. ��� ����
        backgroundSpriteRenderer.sprite = peacefulEnding;
        yield return new WaitForSeconds(1f);

        // 5. Thank you �޽���
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

        while (true) // ���� ����
        {
            float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
            float offsetY = Random.Range(-1f, 1f) * shakeIntensity;
            cameraTransform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);
            yield return null;
        }
    }
}
