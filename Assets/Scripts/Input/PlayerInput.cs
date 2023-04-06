using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, PlayerInputActions.IGameplayActions
{
    PlayerInputActions inputActions;
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };

    void OnEnable()
    {
        inputActions = new PlayerInputActions();
        inputActions.Gameplay.SetCallbacks(this);

    }

    private void OnDisable()
    {
        DisableAllInputs();
    }

    /// <summary>
    /// 启动输入
    /// </summary>
    public void EnableGameplayInput()
    {
        inputActions.Gameplay.Enable();

        //使光标不可见
        Cursor.visible = false;
        //锁定光标
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// 禁用输入
    /// </summary>
    public void DisableAllInputs()
    {
        inputActions.Gameplay.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            onMove.Invoke(context.ReadValue<Vector2>());
        }

        if(context.phase == InputActionPhase.Canceled)
        {
            onStopMove.Invoke();
        }
    }


}
