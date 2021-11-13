using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {
  
  protected FiniteStateMachine stateMachine;
  protected Entity entity;
  protected float startTime;
  protected string animBoolName;
  protected Core core;

  public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName) {
    this.entity = entity;
    this.stateMachine = stateMachine;
    this.animBoolName = animBoolName;
    this.core = entity.core;
  }

  public virtual void Enter() {
    startTime = Time.time;
    entity.animator.SetBool(animBoolName, true);
    DoChecks();
  }

  public virtual void Exit() {
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
