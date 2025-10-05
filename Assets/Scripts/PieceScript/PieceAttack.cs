using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PieceAttack : MonoBehaviour
{
    Controls controls;
    Vector2 direction;

    private void Awake()
    {
        controls = new();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();       
    }

    void Update()
    {
        if (controls.Player.AttackDirection.IsPressed())
        {
            direction = controls.Player.AttackDirection.ReadValue<Vector2>();
            Debug.Log(direction);
        }
        else
        {
            
        }
    }
}
