using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState {

  

  public PlayerIdleState(
    Player player, 
    PlayerStateMachine stateMachine, 
    SO_PlayerData playerData, 
    string animBoolName
  ) : base(player, stateMachine, playerData, animBoolName) {

  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void Enter() {
    base.Enter();
    Debug.Log("entered Idle state ----");
    core.Movement.SetVelocityX(0f);
  }

  public override void Exit() {
    base.Exit();
    Debug.Log("exited Idle state ----");
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    if (xInput != 0) {
      stateMachine.ChangeState(player.moveState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

}
