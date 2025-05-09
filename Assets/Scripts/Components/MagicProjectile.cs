using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    [SerializeField] private float speed=10f;
    public LayerMask hitLayers;

    private Rigidbody2D _rigidbody;
    private float direction = 1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();/// 시작 시 Rigidbody2D 컴포넌트를 가져옴
    }

    // 발사 방향 설정 (외부에서 호출)
    public void SetDirection(float dir)
    {
        direction = dir;
        _rigidbody.velocity = new Vector2(direction * speed, 0f);

        // 좌우 반전이 필요하다면 스프라이트도 반전 가능
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(((1<<collision.gameObject.layer) & hitLayers)==0) return;

        if (collision.TryGetComponent<IDamageable>(out var dmg))
            dmg.TakeDamage(1);

        Destroy(gameObject);
    }

    
}
