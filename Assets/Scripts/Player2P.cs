using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2P : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D _rigidBody2D;
    private bool isGrounded = false;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Vector2 newvelocity = _rigidBody2D.velocity;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newvelocity.x = -moveSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            newvelocity.x = moveSpeed;
        }
        else
        {
            newvelocity.x = 0f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            newvelocity.y = jumpForce;
            isGrounded = false;
        }
        _rigidBody2D.velocity = newvelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
