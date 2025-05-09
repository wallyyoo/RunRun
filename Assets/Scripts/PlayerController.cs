using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        if (Input.anyKeyDown)
            Debug.Log("아무 키나 눌림");

        Debug.Log("현재 속도: " + _rigidBody2D.velocity);
        Vector2 newVelocity = _rigidBody2D.velocity;


        if (Input.GetKey(KeyCode.A)) newVelocity.x = -moveSpeed;
        else if (Input.GetKey(KeyCode.D)) newVelocity.x = moveSpeed;
        else newVelocity.x = 0f;

        Debug.Log("입력됨 / velocity: " + newVelocity + moveSpeed);


        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            newVelocity.y = jumpForce;
            isGrounded = false;
        }


        _rigidBody2D.velocity = newVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
