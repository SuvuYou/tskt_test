using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "ScriptableObjects/InputReaderSO")]
public class InputReaderSO : ScriptableObject, PlayerInputActions.IMainActions
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnAttackEvent;

    public PlayerInputActions InputActions;
    public Vector2 Direction => InputActions.Main.Move.ReadValue<Vector2>();

    public void EnableActions()
    {
        if (InputActions == null)
        {
            InputActions = new PlayerInputActions();
            InputActions.Main.SetCallbacks(this);  
        }

        InputActions.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context) 
    {
        if (context.performed) OnAttackEvent?.Invoke();
    }
}