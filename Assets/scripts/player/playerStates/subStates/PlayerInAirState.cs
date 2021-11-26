using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState {

  private int xInput;
  private bool isGrounded;
  private bool isJumping;
  private bool jumpInputStop;
  private bool jumpInput;

  public PlayerInAirState(
    Player player, 
    PlayerStateMachine stateMachine, 
    SO_PlayerData playerData, 
    string animBoolName
  ) : base(player, stateMachine, playerData, animBoolName) {

  }

  public override void DoChecks() {
    base.DoChecks();
    isGrounded = core.CollisionSense.Ground;
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
    jumpInput = player.inputHandler.jumpInput;
    jumpInputStop = player.inputHandler.jumpInputStop;

    CheckJumpMultiplier();

    if (player.inputHandler.attackInputs[(int)CombatInputs.primary]) {

      stateMachine.ChangeState(player.primaryAttackState);

    } else if (player.inputHandler.attackInputs[(int)CombatInputs.secondary]) {

      stateMachine.ChangeState(player.dashState);

    } else if (isGrounded && core.Movement.currentVelocity.y < 0.01f) {
      stateMachine.ChangeState(player.landState);
    } else if (jumpInput && player.jumpState.CanJump()) {
      stateMachine.ChangeState(player.jumpState);
    } else {
      // might not want to move in the air 
      core.Movement.CheckIfShouldFlip(xInput);
      core.Movement.SetVelocityX(playerData.moveVelocity * xInput);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  private void CheckJumpMultiplier() {
    if (isJumping) {
      if (jumpInputStop) {
        core.Movement.SetVelocityY(core.Movement.currentVelocity.y * playerData.variableJumpHeightMultiplier);
        isJumping = false;
      } else if (core.Movement.currentVelocity.y <= 0f) {
        isJumping = false;
      }
    }
  }

  public void SetIsJumping() => isJumping = true;

}
