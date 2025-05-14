using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public LayerMask hitLayers;

    private float maxDuration = 1.0f;
    private float _timer = 0f;
    private float direction = 1f;

    private void Awake()
    {
        
    }

    private void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        _timer += Time.deltaTime;
        if(_timer > maxDuration)
        {
            Destroy(gameObject);
        }
    }

    // 발사 방향 설정 + 스프라이트 반전
    public void SetDirection(float dir)
    {
        direction = Mathf.Sign(dir); // -1 또는 +1

        // 스프라이트 좌우 반전
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    // 물리 충돌 감지
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌감지" + collision.name);
        // 무시할 레이어가 아니면
        if (((1 << collision.gameObject.layer) & hitLayers) == 0){
        Debug.Log("무시된 레이어: " + LayerMask.LayerToName(collision.gameObject.layer));
            return;
        }


        if (collision.TryGetComponent<IDamageable>(out var target))
        {
            Debug.Log("파괴");                                         
            target.TakeDamage(1);
        }

        else
        {
            Debug.LogWarning("Idamageble 문제");
        }
            // 투사체 파괴
            Destroy(gameObject);
    }

    
}
