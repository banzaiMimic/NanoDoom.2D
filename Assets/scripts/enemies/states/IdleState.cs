using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {

  protected SO_IdleState stateData;
  protected bool flipAfterIdle;
  protected bool isIdleTimeOver;
  protected bool isPlayerInMinAggroRange;
  protected float idleTime;

  public IdleState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName, 
    SO_IdleState stateData
  ) : base(entity, stateMachine, animBoolName) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();
    core.Movement.SetVelocityX(0f);
    isIdleTimeOver = false;
    SetRandomIdleTime();
  }

  public override void Exit() {
    base.Exit();

    if (flipAfterIdle) {
      core.Movement.Flip();
    }
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(0f);

    if (Time.time >= startTime + idleTime) {
      isIdleTimeOver = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
  }

  public void SetFlipAfterIdle(bool flip) {
    flipAfterIdle = flip;
  }

  private void SetRandomIdleTime() {
    idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
  }

}
