using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {

  public Vector2 rawMovementInput { get; private set; }
  public int normalizedInputX { get; private set; }
  public int normalizedInputY { get; private set; }
  
  public void OnMoveInput(InputAction.CallbackContext context) {
    rawMovementInput = context.ReadValue<Vector2>();
    normalizedInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
    normalizedInputY = (int)(rawMovementInput * Vector2.up).normalized.y;
  }
  
  public void OnJumpInput(InputAction.CallbackContext context) {
    
  }

}
