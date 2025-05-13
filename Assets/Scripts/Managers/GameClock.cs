using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro�� ����ϱ� ���� �ʿ�

public class CustomClock : MonoBehaviour
{
    public TMP_Text timeText; // TimeClock ������Ʈ�� ���� TextMeshPro ������Ʈ�� ����

    private float elapsedSeconds = 0f;     // ��� �ð� (�� ����)
    private int displayMinutes = 0;        // 00~59���� �����ϴ� '��'
    private int displaySeconds = 0;        // 00~59���� �����ϴ� '��'

    void Update()
    {
        elapsedSeconds += Time.deltaTime;

        // 1�ʰ� ������ ������ ���ڸ� ����
        if (elapsedSeconds >= 1f)
        {
            elapsedSeconds -= 1f; // 1�� ����
            displaySeconds++;     // �� ����

            if (displaySeconds > 60)
            {
                displaySeconds = 0;
                displayMinutes++;
            }

            if (displayMinutes > 60)
            {
                displayMinutes = 59;
                displaySeconds = 59;
                enabled = false; // 99:59 ���� �� ��ũ��Ʈ �ߴ�
            }

            // �ð� �ؽ�Ʈ ������Ʈ
            timeText.text = string.Format("{0:00}:{1:00}", displayMinutes, displaySeconds);
        }
    }
}


