using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Entity {

  public EnemyFlying_MoveState moveState { get; private set; }

  [SerializeField] public SO_MoveState moveStateData;

  public override void Awake() {
    base.Awake();
    moveState = new EnemyFlying_MoveState(this, stateMachine, "move", moveStateData);
  }

  public override void Start() {
    stateMachine.Initialize(moveState);
  }
}
