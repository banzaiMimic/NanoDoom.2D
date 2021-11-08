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
    isDetectingWall = entity.CheckWall();
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
  }

  public override void Enter() {
    base.Enter();
    entity.SetVelocity(stateData.movementSpeed);
    Debug.Log("[MoveState] entity velocity to " + stateData.movementSpeed);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    entity.SetVelocity(stateData.movementSpeed);
    Debug.Log("entity velocity x: " + entity.rb.velocity.x );
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

}
