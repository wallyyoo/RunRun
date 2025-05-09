using UnityEngine;

public class Trap2D : MonoBehaviour
{
    // 플레이어(테스트용 캐릭터)가 트리거에 들어왔을 때
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tag를 Player로 해뒀다면
        if (other.CompareTag("Player"))
        {
            Debug.Log("죽었습니다");
        }
    }
}
