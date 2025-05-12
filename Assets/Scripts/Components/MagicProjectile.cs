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

    // �߻� ���� ���� + ��������Ʈ ����
    public void SetDirection(float dir)
    {
        direction = Mathf.Sign(dir); // -1 �Ǵ� +1

        // ��������Ʈ �¿� ����
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    // ���� �浹 ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ ���̾ �ƴϸ�
        if (((1 << collision.gameObject.layer) & hitLayers) == 0) return;

        // ������ �� �� �ִ� ����̸� ó�� (dmg -> target���� ������ ����)
        if (collision.TryGetComponent<IDamageable>(out var target))
        {                                               
            target.TakeDamage(1);
        }

        // ����ü �ı�
        Destroy(gameObject);
    }

    
}
