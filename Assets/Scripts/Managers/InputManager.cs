using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // 마법사 움직임 로직
    public Vector2 GetWizardMovement()
    {
        float pos = 0;
        if (Input.GetKey(KeyCode.A)) pos = -1f;
        if (Input.GetKey(KeyCode.D)) pos = 1f;

        return new Vector2(pos, 0f).normalized;
    }

    public bool GetWizardJump()
    {
        return Input.GetKey(KeyCode.W);
    }

    public bool GetWizardAttackDown()
    {
        return Input.GetKey(KeyCode.F);
    }


    // 기사 움직임 로직
    public Vector2 GetKnightMovement()
    {
        float pos = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) pos = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) pos = 1f;

        return new Vector2(pos, 0f).normalized;
    }

    public bool GetKnightJump()
    {
        return Input.GetKey(KeyCode.UpArrow);
    }

    public bool GetKnightAttack()
    {
        return Input.GetKey(KeyCode.P);
    }
}
