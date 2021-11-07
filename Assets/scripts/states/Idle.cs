using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Grounded {

  public Idle(MovementSM stateMachine) : base("Idle", stateMachine) { }

  public override void Enter() {
    base.Enter();
  }

  public override void Update() {
    base.Update();
  }

  public override void LateUpdate() {
    base.LateUpdate();
    Vector2 vel = _movementSm.rBody.velocity;
    vel.x = 0;
    _movementSm.rBody.velocity = vel;
  }

}
