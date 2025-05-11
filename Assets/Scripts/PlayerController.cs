using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : BaseController
{
    private Camera _camera;

    protected override void Start()
    {
        base.Start();
        _camera = Camera.main;

    }

    protected override void HandleActiom()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertial = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertial).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = _camera.ScreenToWorldPoint(mousePosition);
        movementDirection = new Vector2(horizontal, 0f).normalized;
        IookDirection = (worldPos - (Vector2)transform.position);

        if (IookDirection.magnitude < .9f)
        {
            IookDirection = Vector2.zero;
        }
        else
        {
            IookDirection = IookDirection.normalized;
        }
    }
}

