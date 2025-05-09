using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    [SerializeField] private float speed=10f;
    public LayerMask hitLayers;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(((1<<collision.gameObject.layer) & hitLayers)==0) return;

        if (collision.TryGetComponent<IDamageable>(out var dmg))
            dmg.TakeDamage(1);

        Destroy(gameObject);
    }

    
}
