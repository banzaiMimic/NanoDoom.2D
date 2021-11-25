using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {

  protected int xInput;
  protected int yInput;

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
    isGrounded = core.CollisionSense.Ground;
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
    yInput = player.inputHandler.normalizedInputY;
    jumpInput = player.inputHandler.jumpInput;

    //@Todo is touching ceiling check for if ...primary and if ... secondary
    if (player.inputHandler.attackInputs[(int)CombatInputs.primary]) {
      stateMachine.ChangeState(player.primaryAttackState);
    } else if (player.inputHandler.attackInputs[(int)CombatInputs.secondary]) {
      stateMachine.ChangeState(player.dashState);
    } else if (jumpInput && player.jumpState.CanJump()) {
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
