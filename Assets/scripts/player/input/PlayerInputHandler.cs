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
  private Vector2 hitLineEnd;

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
    this.initHitLine();
  }

  private void initHitLine() {
    Vector3 playerPos = this.player.core.transform.position;
    Vector2 hitLineStart = new Vector2(playerPos.x, playerPos.y);
    this.hitLineEnd = new Vector2(this.player.core.transform.position.x + this.player.core.Movement.facingDirection, this.player.core.transform.position.y);
    Combos.Instance.updateHitLine(hitLineEnd - hitLineStart);
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

  private Vector2 defaultPlayerHitline(Vector3 playerPos, int playerFacing) => new Vector2( playerPos.x + playerFacing, playerPos.y );
  
  public void OnMoveInput(InputAction.CallbackContext context) {
    
    Vector3 playerPos = this.player.core.transform.position;
    Vector2 hitLineStart = new Vector2(playerPos.x, playerPos.y);
    int playerFacing = this.player.core.Movement.facingDirection;
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

          hitLineEnd = defaultPlayerHitline(playerPos, playerFacing);
        }

      }

      if (gamepad != null) {
        
        if (gamepad.wasUpdatedThisFrame) {
          rawMovementInput = context.ReadValue<Vector2>();
          normalizedInputX = Mathf.RoundToInt(rawMovementInput.x);
          normalizedInputY = Mathf.RoundToInt(rawMovementInput.y);
          
          hitLineEnd = new Vector2( playerPos.x + rawMovementInput.x, playerPos.y + rawMovementInput.y);
          bool gamepadMovementStopped = normalizedInputX == 0 && normalizedInputY == 0;
          if (gamepadMovementStopped) {
            hitLineEnd = defaultPlayerHitline(playerPos, playerFacing);
          }

        }
      }
    }

    Combos.Instance.updateHitLine(hitLineEnd - hitLineStart);

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