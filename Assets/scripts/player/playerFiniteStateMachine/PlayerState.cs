using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {

  protected Core core;
  protected Player player;
  protected PlayerStateMachine stateMachine;
  protected SO_PlayerData playerData;
  protected float startTime;
  protected bool isAnimationFinished;

  private string animBoolName;

  public PlayerState(
    Player player, 
    PlayerStateMachine stateMachine, 
    SO_PlayerData playerData,
    string animBoolName
  ) {
    this.player = player;
    this.stateMachine = stateMachine;
    this.playerData = playerData;
    this.animBoolName = animBoolName;
    this.core = player.core;
  }

  public virtual void Enter() {
    DoChecks();
    player.animator.SetBool(animBoolName, true);
    startTime = Time.time;
    isAnimationFinished = false;
  }

  public virtual void Exit() {
    player.animator.SetBool(animBoolName, false);
  }

  public virtual void LogicUpdate() {
  }

  public virtual void PhysicsUpdate() {
    DoChecks();
  }

  public virtual void DoChecks() {}

  public virtual void AnimationStarted() {}

  public virtual void AnimationFinished() => isAnimationFinished = true;

}
