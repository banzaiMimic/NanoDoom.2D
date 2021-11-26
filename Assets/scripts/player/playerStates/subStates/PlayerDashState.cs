using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState {

  private bool isDashing = false;
  private float dashTimeLeft;
  private float lastImageXpos;
  private float lastDash = -100f; // last dash time
  public float dashTime = 0.2f;
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
    Debug.Log("entered Dash state ----");
    if (this.chargesAvailable > 0) {
      if (dashTimeLeft < 0) {
        dashTimeLeft = 0f;
      }
      Debug.Log("lastDash: " + lastDash + " isDashing: " + isDashing + " dashTimeLeft: " + dashTimeLeft + " isAbilityDone: " + isAbilityDone + " canSetVelocity: " + player.core.movement.canSetVelocity + " canFlip: " + player.core.movement.canFlip);
      
      if (Time.time >= (lastDash + dashCoolDown)) {
        TryDash();
      } else {
        Debug.Log("Cannot dash... Time.time: " + Time.time + " lastDash + dashCoolDown: " + (lastDash + dashCoolDown));
        stateMachine.ChangeState(player.stateMachine.previousState);
      }
    } else {
      stateMachine.ChangeState(player.stateMachine.previousState);
    }
  }

  private void TryDash() {
    Debug.Log("Trying Dash...");
    isDashing = true;
    dashTimeLeft = dashTime;
    lastDash = Time.time;

    PlayerAfterImagePool.Instance.GetFromPool();
    lastImageXpos = player.core.transform.position.x;
    this.chargesAvailable--;
    Dispatcher.Instance.OnUpdatePlayerAbilityCharges(this.chargesAvailable, this.chargesTotal);
  }

  // check if should be dashing or stop
  private void checkDash() {
    if (isDashing) {
      if (dashTimeLeft > 0) {
        player.core.movement.canSetVelocity = false;
        player.core.movement.canFlip = false;
        player.core.movement.SetVelocity(dashSpeed * player.core.movement.facingDirection, player.core.movement.rBody.velocity.y);
        dashTimeLeft -= Time.deltaTime;

        if (Mathf.Abs(player.core.transform.position.x - lastImageXpos) > distanceBetweenImages) {
          PlayerAfterImagePool.Instance.GetFromPool();
          lastImageXpos = player.core.transform.position.x;
        }
      }
      //@Todo add or is touchingWall
      if (dashTimeLeft <= 0) {
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
    Debug.Log("---- exited Dash state ");
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }
}
