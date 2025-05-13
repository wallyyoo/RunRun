using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위해 필요

public class CustomClock : MonoBehaviour
{
    public TMP_Text timeText; // TimeClock 오브젝트에 붙은 TextMeshPro 컴포넌트를 연결

    private float elapsedSeconds = 0f;     // 경과 시간 (초 단위)
    private int displayMinutes = 0;        // 00~59까지 증가하는 '분'
    private int displaySeconds = 0;        // 00~59까지 증가하는 '초'

    void Update()
    {
        elapsedSeconds += Time.deltaTime;

        // 1초가 지났을 때마다 숫자를 갱신
        if (elapsedSeconds >= 1f)
        {
            elapsedSeconds -= 1f; // 1초 차감
            displaySeconds++;     // 초 증가

            if (displaySeconds > 60)
            {
                displaySeconds = 0;
                displayMinutes++;
            }

            if (displayMinutes > 60)
            {
                displayMinutes = 59;
                displaySeconds = 59;
                enabled = false; // 99:59 도달 시 스크립트 중단
            }

            // 시계 텍스트 업데이트
            timeText.text = string.Format("{0:00}:{1:00}", displayMinutes, displaySeconds);
        }
    }
}


