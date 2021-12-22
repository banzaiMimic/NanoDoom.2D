using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState {

  private Weapon weapon;
  private int xInput;
  private float velocityToSet;
  private float? lastHitTime;
  private float resetTimeInState = .2f;
  private float timeInState = .2f;
  private bool setVelocity;
  private bool shouldCheckFlip;
  private int comboChains = 1;
  private float attackForce = 3f;

  public PlayerAttackState(
    Player player, 
    PlayerStateMachine stateMachine, 
    SO_PlayerData playerData, 
    string animBoolName
  ) : base(player, stateMachine, playerData, animBoolName) {
    
  }

  private bool hitWithinChainTime() {
    bool result = (Time.time - this.lastHitTime) <= Combos.Instance.ChainIfWithinTime;
    //Debug.Log("comboTime: " + result + (Time.time - this.lastHitTime));
    return result;
  }

  public override void Enter() {
    base.Enter();
    this.SetPlayerVelocity(this.attackForce);
    this.timeInState = this.resetTimeInState;
    
    this.core.Movement.EnableMovement();
    Dispatcher.Instance.onPlayerMeleeSwingSound();
    setVelocity = false;
    weapon.EnterWeapon();

    if (this.hitWithinChainTime()) {
      //Debug.Log("[PlayerAttackState - hitWithinChainTime] lastHitX: " + Combos.Instance.lastNormalizedInputX + " lastY: " + Combos.Instance.lastNormalizedInputY);
      this.comboChains++;
      // first combo chain means 2 consecutive swings
      if (this.comboChains == 2) {

      }
    } else {
      this.comboChains = 1;
      this.weapon.clearLastEnemyHit();
    }

    this.lastHitTime = Time.time;
    this.weapon.UpdateComboChains(this.comboChains);
    Dispatcher.Instance.OnPrimaryAttack();
  }

  public override void Exit() {
    base.Exit();
    weapon.ExitWeapon();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    if (this.isAnimationFinished) {
      this.stateMachine.ChangeState(this.player.idleState);
      return;
    }
    if (this.timeInState > 0) {
      this.timeInState -= Time.deltaTime;
    } else {
      this.timeInState = 0;
      Debug.Log("calling change state back to idle...");
      this.AnimationFinished();
      
      //this.stateMachine.ChangeState(this.player.idleState);
    }
    xInput = player.inputHandler.normalizedInputX;

    if (player.inputHandler.attackInputs[(int)CombatInputs.secondary]) {
      
      PlayerAbilityState abilityState = player.GetActiveAbility();
      if (abilityState != null && !AbilityCooldown.isOnCoolDown) {
        stateMachine.ChangeState(abilityState);
      }

    }
    
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
