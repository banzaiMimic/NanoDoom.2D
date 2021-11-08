using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {
  
  protected FiniteStateMachine stateMachine;
  protected Entity entity;
  protected float startTime;
  protected string animBoolName;

  public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName) {
    this.entity = entity;
    this.stateMachine = stateMachine;
    this.animBoolName = animBoolName;
  }

  public virtual void Enter() {
    startTime = Time.time;
    Debug.Log("[State] animator ENTER --" + animBoolName + "--");
    entity.animator.SetBool(animBoolName, true);
    DoChecks();
  }

  public virtual void Exit() {
    Debug.Log("[State] animator EXIT --" + animBoolName + "--");
    entity.animator.SetBool(animBoolName, false);
  }

  public virtual void LogicUpdate() {
    
  }

  public virtual void PhysicsUpdate() {
    DoChecks();
  }

  public virtual void DoChecks() {

  }

}
