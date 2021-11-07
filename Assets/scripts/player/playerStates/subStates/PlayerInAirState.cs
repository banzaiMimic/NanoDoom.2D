using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState {

  private int xInput;
  private bool isGrounded;

  public PlayerInAirState(
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
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.inputHandler.normalizedInputX;

    if (isGrounded && player.currentVelocity.y < 0.01f) {
      stateMachine.ChangeState(player.landState);
    } else {
      // might not want to move in the air 
      player.CheckIfShouldFlip(xInput);
      player.SetVelocityX(playerData.moveVelocity * xInput);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

}
