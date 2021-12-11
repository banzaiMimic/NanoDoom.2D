using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState {

  private Weapon weapon;
  private int xInput;
  private float velocityToSet;
  private bool setVelocity;
  private bool shouldCheckFlip;
  private float? lastHitTime;
  private int comboChains = 1;

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
    // enable player movement
    
    //LogHighlight("entering attack state-- " + this.comboChains + " comboChains.");
    this.core.Movement.EnableMovement();
    Dispatcher.Instance.OnPlayerMeleeSwing();
    setVelocity = false;
    weapon.EnterWeapon();

    if (this.hitWithinChainTime()) {
      //Debug.Log("[PlayerAttackState - hitWithinChainTime] lastHitX: " + Combos.Instance.lastNormalizedInputX + " lastY: " + Combos.Instance.lastNormalizedInputY);
      //Debug.Log("combo++ " + this.comboChains);
      this.comboChains++;
      // first combo chain means 2 consecutive swings
      if (this.comboChains == 2) {
        // @Recall 
        // if user is moving on d-pad, combos dont seem to be incrementing / player movement is not disabled.
        this.core.Movement.DisableMovement();
      }
    } else {
      this.comboChains = 1;
    }

    this.lastHitTime = Time.time;
  }

  public override void Exit() {
    base.Exit();
    weapon.ExitWeapon();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
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
