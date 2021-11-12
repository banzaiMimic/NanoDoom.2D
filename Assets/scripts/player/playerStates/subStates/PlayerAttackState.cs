using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState {

  private Weapon weapon;
  private float velocityToSet;
  private bool setVelocity;

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
    if (setVelocity) {
      player.SetVelocityX(velocityToSet * player.facingDirection);
    }

  }

  public void SetWeapon(Weapon weapon) {
    this.weapon = weapon;
    weapon.InitializeWeapon(this);
  }

  public void SetPlayerVelocity(float velocity) {
    player.SetVelocityX(velocity * player.facingDirection);
    velocityToSet = velocity;
    setVelocity = true;
  }

  public override void AnimationFinished() {
    base.AnimationFinished();
    isAbilityDone = true;
  }
}
