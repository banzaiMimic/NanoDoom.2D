using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying_MoveState : MoveState {

  private float speedY = 3f;
  private float minSpeedY = 1f;
  private float maxSpeedY = 6f;
  private float minSpeedX = 1f;
  private float maxSpeedX = 6f;
  private float startY = 0f;
  private float startX = 0f;
  private float maxTravelDistanceY = 3f;
  private float maxTravelDistanceX = 30f;
  
  public EnemyFlying_MoveState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName, 
    SO_MoveState stateData
  ) : base(entity, stateMachine, animBoolName, stateData) {
    RandomizeParams();
  }

  private void RandomizeParams() {
    this.speedY = Random.Range(minSpeedY, maxSpeedY);
    this.stateData.movementSpeed = -Random.Range(minSpeedX, maxSpeedX);
  }

  public override void Enter() {
    base.Enter();
    startY = Mathf.Abs(entity.transform.position.y);
    startX = Mathf.Abs(entity.transform.position.x);
    entity.core.Movement.SetVelocityY(-speedY);
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    float travelDistanceY = startY - Mathf.Abs(entity.core.Movement.rBody.transform.position.y);
    float travelDistanceX = startX - Mathf.Abs(entity.core.Movement.rBody.transform.position.x);
    if (Mathf.Abs(travelDistanceY) >= maxTravelDistanceY) {
      startY = Mathf.Abs(entity.transform.position.y);
      entity.core.Movement.SetVelocityY(-entity.core.Movement.currentVelocity.y);
    }

    if (Mathf.Abs(travelDistanceX) >= maxTravelDistanceX) {
      this.entity.DestroyEntity();
    }
  }

  public override void Exit() {
    base.Exit();
  }

}
