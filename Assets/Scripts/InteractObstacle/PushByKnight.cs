using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushByKnight : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private KnightController knight;
    private bool isBeingPushed = false;

    [SerializeField] private float pushSmoothness = 5f;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    private void FixedUpdate()
    {
        if (isBeingPushed && knight != null)
        {
            float moveDir = knight.GetMoveDirection();
            float moveSpeed = knight.GetCurrentSpeed();

            if (moveSpeed > 0.3f)
            {
                float pushSpeed = moveSpeed * 7.0f;
                Vector3 targetPos = transform.position + new Vector3(moveDir * pushSpeed * Time.fixedDeltaTime, 0f, 0f);
           

                transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * pushSmoothness);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<KnightController>(out var k))
        {
            knight = k;
            isBeingPushed = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<KnightController>(out var k))
        {
            knight = k;
            isBeingPushed = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<KnightController>(out _))
        {

            isBeingPushed= false;
            knight = null;
        }
    }

}
