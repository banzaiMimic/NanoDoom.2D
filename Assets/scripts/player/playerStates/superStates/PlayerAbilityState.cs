using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState {

  protected bool isAbilityDone;
  protected int chargesAvailable;
  protected int chargesTotal;

  //@Todo might want to keep track of cd + timer from actual ability states...
  //public bool offCoolDown = true;

  private string animBoolName;
  private bool isGrounded;

  public PlayerAbilityState(
    Player player, 
    PlayerStateMachine stateMachine, 
    SO_PlayerData playerData, 
    string animBoolName
  ) : base(player, stateMachine, playerData, animBoolName) {
    this.animBoolName = animBoolName;
    this.chargesAvailable = playerData.startingAbilityCharges;
    this.chargesTotal = playerData.maxAbilityCharges;
  }

  public bool HasCharges() {
    return this.chargesAvailable > 0;
  }

  public int AddCharge() {
    if (this.chargesAvailable < this.chargesTotal) {
      this.chargesAvailable++;
    }
    return this.chargesAvailable;
  }

  public int GetMaxCharges() {
    return this.chargesTotal;
  }


  public override void DoChecks() {
    base.DoChecks();
    isGrounded = core.CollisionSense.Ground;
  }

  public override void Enter() {
    base.Enter();
    isAbilityDone = false;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    if (isAbilityDone) {
      if (isGrounded && core.Movement.currentVelocity.y < 0.01f) {
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
