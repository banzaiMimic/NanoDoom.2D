using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {

  protected int xInput;

  private bool jumpInput;
  private bool isGrounded;

  public PlayerGroundedState(
    Player player, 
    PlayerStateMachine stateMachine, 
    SO_PlayerData playerData, 
    string animBoolName
  ) : base(player, stateMachine, playerData, animBoolName) {

  }

  public override void DoChecks() {
    base.DoChecks();
    isGrounded = player.CheckIfGrounded();
  }

  public override void Enter() {
    base.Enter();
    player.jumpState.ResetAmountOfJumpsLeft();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    xInput = player.inputHandler.normalizedInputX;
    jumpInput = player.inputHandler.jumpInput;

    if (jumpInput && player.jumpState.CanJump()) {
      player.inputHandler.UseJumpInput(); // not sure on this, should be a better way to reset our jump bool
      stateMachine.ChangeState(player.jumpState);
    } else if (!isGrounded) {
      player.jumpState.DecreaseAmountOfJumpsLeft();
      stateMachine.ChangeState(player.inAirState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

}
