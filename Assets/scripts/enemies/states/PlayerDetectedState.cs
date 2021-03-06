using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State {

  protected SO_PlayerDetectedState stateData;
  protected bool isPlayerInMinAggroRange;
  protected bool isPlayerInMaxAggroRange;
  protected bool performLongRangeAction;
  protected bool performCloseRangeAction;

  public PlayerDetectedState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName,
    SO_PlayerDetectedState stateData
  ): base(entity, stateMachine, animBoolName) {
    this.stateData = stateData;
  }

  public override void DoChecks() {
    base.DoChecks();
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
  }

  public override void Enter() {
    base.Enter();
    performLongRangeAction = false;
    core.Movement.SetVelocityX(0f);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(0f);

    if (Time.time >= startTime + stateData.longRangeActionTime) {
      performLongRangeAction = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

}
