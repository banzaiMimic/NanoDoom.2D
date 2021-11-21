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
    Dispatcher.Instance.OnPlayerMeleeSwing();
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
      core.Movement.CheckIfShouldFlip(xInput);
    }

    if (setVelocity) {
      core.Movement.SetVelocityX(velocityToSet * core.Movement.facingDirection);
    }
  }

  public void SetWeapon(Weapon weapon) {
    this.weapon = weapon;
    weapon.InitializeWeapon(this, core);
  }

  public void SetPlayerVelocity(float velocity) {
    core.Movement.SetVelocityX(velocity * core.Movement.facingDirection);
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
