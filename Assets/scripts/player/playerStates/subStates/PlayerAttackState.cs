using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState {

  private Weapon weapon;
  private int xInput;
  private float velocityToSet;
  private bool setVelocity;
  private bool shouldCheckFlip;

  public PlayerAttackState(
    Player player, 
    PlayerStateMachine stateMachine, 
    SO_PlayerData playerData, 
    string animBoolName
  ) : base(player, stateMachine, playerData, animBoolName) {
    
  }

  public override void Enter() {
    base.Enter();
    setVelocity = false;
    weapon.EnterWeapon();
  }

  public override void Exit() {
    base.Exit();
    weapon.ExitWeapon();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    xInput = player.inputHandler.normalizedInputX;
    
    if (shouldCheckFlip) {
      core.movement.CheckIfShouldFlip(xInput);
    }

    if (setVelocity) {
      core.movement.SetVelocityX(velocityToSet * core.movement.facingDirection);
    }
  }

  public void SetWeapon(Weapon weapon) {
    this.weapon = weapon;
    weapon.InitializeWeapon(this);
  }

  public void SetPlayerVelocity(float velocity) {
    core.movement.SetVelocityX(velocity * core.movement.facingDirection);
    velocityToSet = velocity;
    setVelocity = true;
  }

  public void SetFlipCheck(bool check) {
    shouldCheckFlip = check;
  }

  public override void AnimationFinished() {
    base.AnimationFinished();
    isAbilityDone = true;
  }
}
