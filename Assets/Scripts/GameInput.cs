using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    private InputSystem newInput;

    public event EventHandler OnInteractEventHandler;
    public event EventHandler OnInteractAlternat;
    private void Awake(){
        newInput = new InputSystem();
        newInput.PlayerInputKeyboard.Enable();
        newInput.PlayerInputKeyboard.InteractButton.performed += Interact_performed;
        newInput.PlayerInputKeyboard.InteractButtonAlternate.performed += InteractButtonAlternateOnperformed;
    }

    private void InteractButtonAlternateOnperformed(InputAction.CallbackContext obj){
        OnInteractAlternat?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext objInteractPerformed){
        OnInteractEventHandler?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 Vector2InputNormalazed(){
        Vector2 inputVector = newInput.PlayerInputKeyboard.Keyboard.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}