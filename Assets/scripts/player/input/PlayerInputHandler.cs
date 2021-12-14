using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {

  public Vector2 rawMovementInput { get; private set; }
  public int normalizedInputX { get; private set; }
  public int normalizedInputY { get; private set; }
  public bool jumpInput { get; private set; }
  public bool jumpInputStop { get; private set; }
  public bool[] attackInputs { get; private set; }
  public PlayerInput playerInput { get; private set; }
  private Player player;
  private Vector2 hitLineEnd = new Vector2();

  [SerializeField]
  private float inputHoldTime = 0.2f;

  private float jumpInputStartTime;

  private void Awake() {
    this.player = GetComponent<Player>();
  }

  private void Start() {
    this.playerInput = GetComponent<PlayerInput>();
    int count = Enum.GetValues(typeof(CombatInputs)).Length;
    attackInputs = new bool[count];
  }

  private void Update() {
    CheckJumpInputHoldTime();
  }

  public void OnPrimaryAttackInput(InputAction.CallbackContext context) {
    if (context.started) {
      Dispatcher.Instance.OnPrimaryAttackStateChangeRequest();
    }
  }

  public void OnSecondaryAttackInput(InputAction.CallbackContext context) {
    if (context.started) {
      attackInputs[(int)CombatInputs.secondary] = true;
    }

    if (context.canceled) {
      attackInputs[(int)CombatInputs.secondary] = false;
    }
  }

  private void MovePlayerViaKeyboard(bool pressingUp, bool pressingRight, bool pressingDown, bool pressingLeft) {
    
  }
  
  public void OnMoveInput(InputAction.CallbackContext context) {
    
    var gamepad = Gamepad.current;
    var keyboard = Keyboard.current;

    if (this.player.core.Movement.canMove) {
      if (keyboard != null) {

        bool pressingRight = keyboard.dKey.IsPressed();
        bool pressingLeft = keyboard.aKey.IsPressed();
        bool pressingUp = keyboard.wKey.IsPressed();
        bool pressingDown = keyboard.sKey.IsPressed();
        int iX = 0;
        int iY = 0;
        Vector3 playerPos = this.player.core.transform.position;

        if (keyboard.anyKey.IsPressed()) {

          if (pressingRight) { iX = 1; } 
          else if (pressingLeft) { iX = -1; }
          if (pressingUp) { iY = 1;
          } else if (pressingDown) { iY = -1; }

          if (this.player.core.Movement.canMove) {
            this.normalizedInputX = iX;
            this.normalizedInputY = iY;
          }

          hitLineEnd = new Vector2( playerPos.x + iX, playerPos.y + iY );

        } else {
          normalizedInputX = 0;
          normalizedInputY = 0;

          hitLineEnd = new Vector2( playerPos.x + this.player.core.Movement.facingDirection, playerPos.y );
        }

        Combos.Instance.updateHitLine(hitLineEnd - new Vector2(playerPos.x, playerPos.y));

      }

      if (gamepad != null) {
        if (gamepad.wasUpdatedThisFrame) {
          rawMovementInput = context.ReadValue<Vector2>();
          Globals.Log("rawY: " + rawMovementInput.y);
          normalizedInputX = Mathf.RoundToInt(rawMovementInput.x);
          normalizedInputY = Mathf.RoundToInt(rawMovementInput.y);
          
          Vector2 rawMovementHackFix = rawMovementInput;

          // keyboard (d key) movement was showing values at 0.707107, not sure where offset is coming from (gamepad + d-pad are correct)
          // if (rawMovementHackFix.y == -0.707107f) {
          //   rawMovementHackFix = new Vector2(rawMovementHackFix.x, rawMovementHackFix.y + 0.707107f);
          // }

          //Debug.Log("moving... x: " + rawMovementHackFix.x);
          //Debug.Log("y: " + rawMovementHackFix.y);
          //Combos.Instance.updateLastMovement(rawMovementHackFix, normalizedInputX, normalizedInputY);
        }
      }
    }

  }
  
  public void OnJumpInput(InputAction.CallbackContext context) {
    if (context.started) {
      jumpInput = true;
      jumpInputStop = false;
      jumpInputStartTime = Time.time;
    }

    if (context.canceled) {
      jumpInputStop = true;
    }
  }

  public void UseJumpInput() => jumpInput = false;

  private void CheckJumpInputHoldTime() {
    if (Time.time >= jumpInputStartTime + inputHoldTime) {
      jumpInput = false;
    }
  }

}

public enum CombatInputs {
  primary,
  secondary
}