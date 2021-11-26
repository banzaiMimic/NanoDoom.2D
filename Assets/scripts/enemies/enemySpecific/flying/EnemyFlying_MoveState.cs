using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying_MoveState : MoveState {

  private float speedY = 3f;
  private float startY = 0f;
  private float maxTravelDistanceY = 3f;
  
  public EnemyFlying_MoveState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName, 
    SO_MoveState stateData
  ) : base(entity, stateMachine, animBoolName, stateData) {
  }

  public override void Enter() {
    base.Enter();
    Debug.Log("Flying move state entered");
    startY = Mathf.Abs(entity.transform.position.y);
    entity.core.Movement.SetVelocityY(-speedY);
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    // @TODO move vel y up and down
    float travelDistanceY = startY - Mathf.Abs(entity.transform.position.y);
    Debug.Log("Mathf.Abs(travelDistanceY): " + Mathf.Abs(travelDistanceY));
    if (Mathf.Abs(travelDistanceY) >= maxTravelDistanceY) {
      startY = Mathf.Abs(entity.transform.position.y);
      entity.core.Movement.SetVelocityY(-entity.core.Movement.currentVelocity.y);
    }
    //core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.facingDirection);
    // @TODO we need to be able to detect a trigger ? 
    // or attach a trigger to a collider...
  }

  public override void Exit() {
    base.Exit();
    Debug.Log("Flying move state exit");
  }

}
