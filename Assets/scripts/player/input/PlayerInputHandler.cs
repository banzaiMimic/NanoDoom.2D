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
      Debug.Log("[PlayerInputHandler] dispatching OnPrimaryAttack");
      Dispatcher.Instance.OnPrimaryAttack();
    }
    // if (context.started) {
    //   attackInputs[(int)CombatInputs.primary] = true;
    // }

    // if (context.canceled) {
    //   attackInputs[(int)CombatInputs.primary] = false;
    // }
  }

  public void OnSecondaryAttackInput(InputAction.CallbackContext context) {
    if (context.started) {
      attackInputs[(int)CombatInputs.secondary] = true;
    }

    if (context.canceled) {
      attackInputs[(int)CombatInputs.secondary] = false;
    }
  }
  
  public void OnMoveInput(InputAction.CallbackContext context) {
    if (this.player.core.Movement.canMove) {
      Debug.Log("moving...");
      rawMovementInput = context.ReadValue<Vector2>();
      normalizedInputX = Mathf.RoundToInt(rawMovementInput.x);
      normalizedInputY = Mathf.RoundToInt(rawMovementInput.y);
      Combos.Instance.updateLastMovement(rawMovementInput, normalizedInputX, normalizedInputY);
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