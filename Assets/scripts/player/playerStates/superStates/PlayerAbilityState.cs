using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState {

  protected bool isAbilityDone;

  private string animBoolName;
  private bool isGrounded;

  public PlayerAbilityState(
    Player player, 
    PlayerStateMachine stateMachine, 
    SO_PlayerData playerData, 
    string animBoolName
  ) : base(player, stateMachine, playerData, animBoolName) {
    this.animBoolName = animBoolName;
  }

  public override void DoChecks() {
    base.DoChecks();
    isGrounded = core.collisionSense.CheckIfGrounded();
  }

  public override void Enter() {
    base.Enter();
    isAbilityDone = false;
    Debug.Log("[playerAbility] " + animBoolName);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    if (isAbilityDone) {
      if (isGrounded && core.movement.currentVelocity.y < 0.01f) {
        stateMachine.ChangeState(player.idleState);
      } else {
        stateMachine.ChangeState(player.inAirState);
      }
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

}
