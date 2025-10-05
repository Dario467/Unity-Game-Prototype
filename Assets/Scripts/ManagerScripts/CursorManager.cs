using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    private Controls controls;
    private Vector2 mousePos;
    public static event Action MissClick;

    void Awake()
    {
        controls = new Controls();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Click.performed += Click;
    }

    void OnDisable()
    {
        controls.Player.Click.performed -= Click;
        controls.Disable();
    }

    public void Click(InputAction.CallbackContext context)
    {
        Debug.Log("Hay click");
        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        mouseScreen.z = -Camera.main.transform.position.z; // proyecta al plano de juego 2D
        mousePos = Camera.main.ScreenToWorldPoint(mouseScreen);
        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector3.zero);
        if (ray.collider != null)
        {
            IControllable controllable = ray.collider.GetComponent<IControllable>();
            if (controllable != null)
            {
                Debug.Log("Se clickeo un controllable");
                controllable.Clicked();
            }
        }
        else
        {
            MissClick?.Invoke();
        }
    }
}
