using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState {

  private bool isDashing = false;
  private float dashTimeLeft;
  private float lastImageXpos;
  private float lastDash = -100f; // last dash time
  public float dashTime = 0.4f;
  public float dashSpeed = 25f;
  public float distanceBetweenImages = 0.1f;
  public float dashCoolDown = 2.5f;

  public PlayerDashState(
    Player player, 
    PlayerStateMachine stateMachine, 
    SO_PlayerData playerData, 
    string animBoolName
  ) : base(player, stateMachine, playerData, animBoolName) {
  }

  public override void AnimationFinished() {
    base.AnimationFinished();
  }

  public override void DoChecks() {
    base.DoChecks();
    checkDash();
  }

  public override void Enter() {
    base.Enter();
    if (this.chargesAvailable > 0) {
      if (dashTimeLeft < 0) {
        dashTimeLeft = 0f;
      }
      
      if (Time.time >= (lastDash + dashCoolDown)) {
        TryDash();
      } else {
        stateMachine.ChangeState(player.stateMachine.previousState);
      }
    } else {
      stateMachine.ChangeState(player.stateMachine.previousState);
    }
  }

  private void TryDash() {
    isDashing = true;
    dashTimeLeft = dashTime;
    lastDash = Time.time;

    PlayerAfterImagePool.Instance.GetFromPool();
    lastImageXpos = player.core.transform.position.x;
    this.chargesAvailable--;
    Dispatcher.Instance.OnUpdatePlayerAbilityCharges(this.chargesAvailable, this.chargesTotal);
    Dispatcher.Instance.OnPlayerAbility(AbilityType.DASH, dashCoolDown);
  }

  // check if should be dashing or stop
  private void checkDash() {
    if (isDashing) {
      if (dashTimeLeft > 0) {
        // go dash
        player.core.movement.canSetVelocity = false;
        player.core.movement.canFlip = false;
        player.core.movement.SetVelocity(dashSpeed * player.core.movement.facingDirection, player.core.movement.rBody.velocity.y);
        dashTimeLeft -= Time.deltaTime;

        if (Mathf.Abs(player.core.transform.position.x - lastImageXpos) > distanceBetweenImages) {
          PlayerAfterImagePool.Instance.GetFromPool();
          lastImageXpos = player.core.transform.position.x;
        }
      } else if (dashTimeLeft <= 0) {
        //give control back to player
        player.core.movement.canSetVelocity = true;
        player.core.movement.canFlip = true;
        this.isAbilityDone = true;
        this.isAnimationFinished = true;
        isDashing = false;
      }
    }
  }

  public override void Exit() {
    base.Exit();
    player.core.movement.canSetVelocity = true;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }
}
