using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State {

  protected SO_LookForPlayerState stateData;
  protected bool flipImmediately;
  protected bool isPlayerInMinAggroRange;
  protected bool isAllTurnsDone;
  protected bool isAllTurnsTimeDone;
  protected float lastTurnTime;
  protected int amountOfTurnsDone;

  public LookForPlayerState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName,
    SO_LookForPlayerState stateData
  ) : base(entity, stateMachine, animBoolName) {
    this.stateData = stateData;
  }

  public override void DoChecks() {
    base.DoChecks();
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
  }

  public override void Enter() {
    base.Enter();
    isAllTurnsDone = false;
    isAllTurnsTimeDone = false;
    lastTurnTime = startTime;
    amountOfTurnsDone = 0;
    entity.SetVelocity(0f);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (flipImmediately) {
      entity.Flip();
      lastTurnTime = Time.time;
      amountOfTurnsDone++;
      flipImmediately = false;
    } else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone) {
      entity.Flip();
      lastTurnTime = Time.time;
      amountOfTurnsDone++;
    }

    if (amountOfTurnsDone >= stateData.amountOfTurns) {
      isAllTurnsDone = true;
    }

    if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone) {
      isAllTurnsTimeDone = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public void SetFlipImmediately(bool flip) {
    flipImmediately = flip;
  }

}
