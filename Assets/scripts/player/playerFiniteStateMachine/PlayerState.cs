using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {

  protected Player player;
  protected PlayerStateMachine stateMachine;
  protected SO_PlayerData playerData;
  protected float startTime;

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
  }

  public virtual void Enter() {
    DoChecks();
    player.animator.SetBool(animBoolName, true);
    startTime = Time.time;
    Debug.Log(animBoolName);
  }

  public virtual void Exit() {
    player.animator.SetBool(animBoolName, false);
  }

  public virtual void LogicUpdate() {

  }

  public virtual void PhysicsUpdate() {
    DoChecks();
  }

  public virtual void DoChecks() {

  }

}
