using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying_MoveState : MoveState {


  
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
    //core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.facingDirection);
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    // @TODO move vel y up and down
    //core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.facingDirection);
    // @TODO we need to be able to detect a trigger ? 
    // or attach a trigger to a collider...
  }

  public override void Exit() {
    base.Exit();
    Debug.Log("Flying move state exit");
  }

}
