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
    /// ��������
    /// </summary>
    public void EnableGameplayInput()
    {
        inputActions.Gameplay.Enable();

        //ʹ��겻�ɼ�
        Cursor.visible = false;
        //�������
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// ��������
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
