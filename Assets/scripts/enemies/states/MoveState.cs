using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State {

  protected SO_MoveState stateData;
  protected bool isDetectingWall;
  protected bool isDetectingLedge;
  protected bool isPlayerInMinAggroRange;

  public MoveState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName,
    SO_MoveState stateData
    ) : base(entity, stateMachine, animBoolName) {
      this.stateData = stateData;
  }

  public override void DoChecks() {
    base.DoChecks();
    isDetectingLedge = entity.CheckLedge();
    isDetectingWall = core.CollisionSense.WallFront;
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
  }

  public override void Enter() {
    base.Enter();
    core.Movement.SetVelocityX((stateData.movementSpeed * core.Movement.baseSpeedMultiplier) * core.Movement.facingDirection);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    core.Movement.SetVelocityX((stateData.movementSpeed * core.Movement.baseSpeedMultiplier) * core.Movement.facingDirection);
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

}
