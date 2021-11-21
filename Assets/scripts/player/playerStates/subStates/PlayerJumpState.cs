using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState {

  private int amountOfJumpsLeft;

  public PlayerJumpState(
    Player player, 
    PlayerStateMachine stateMachine, 
    SO_PlayerData playerData, 
    string animBoolName
  ) : base(player, stateMachine, playerData, animBoolName) {
    this.amountOfJumpsLeft = playerData.amountOfJumps;
  }

  public override void Enter() {
    base.Enter();
    Dispatcher.Instance.OnPlayerJump();
    core.Movement.SetVelocityY(playerData.jumpVelocity);
    isAbilityDone = true;
    DecreaseAmountOfJumpsLeft();
    player.inAirState.SetIsJumping();
  }

  public bool CanJump() {
    //@Todo fix below if we want double jumps but leaving out for now
    if (amountOfJumpsLeft > 0 && core.Movement.currentVelocity.y >= -0.2f) {
      return true;
    } else {
      return false;
    }
  }

  public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;
  public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;

}
