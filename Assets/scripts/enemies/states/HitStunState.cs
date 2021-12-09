using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Todo BlockStunState
public class HitStunState : State {

  private float stunTime = 10f;
  private float stunTimeLeft;
  private bool isStunned = false;
  private float? lastHitTime;
  private int comboChains = 0;

  public HitStunState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName
  ) : base(entity, stateMachine, animBoolName) {
    
  }

  private bool hitWithinChainTime() => (Time.time - this.lastHitTime) <= Combos.Instance.ChainIfWithinTime;

  public override void Enter() {
    base.Enter();
    Debug.Log("lastHitTime was : " + this.lastHitTime);
    
    if (this.hitWithinChainTime()) {
      Debug.Log("[hitWithinChainTime] lastHitX: " + Combos.Instance.lastNormalizedInputX + " lastY: " + Combos.Instance.lastNormalizedInputY);
      this.comboChains++;
      if (this.comboChains == 3) {
        // @Recall 
        // - set direction for HitFlyState to fly in...
        // - changeState to HitFlyState
      }
    }

    this.stunTimeLeft = this.stunTime;
    this.isStunned = true;
    this.lastHitTime = Time.time;
    //@Todo apply damage to self
    Debug.Log("hit stun state entered.. lastHitTime set to: " + this.lastHitTime);
  }

  public override void Exit() {
    base.Exit();
    Debug.Log("hit stun state exited..");
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    if (this.isStunned) {
      if (this.stunTimeLeft > 0 ) {
        this.stunTimeLeft -= Time.deltaTime;
      } else {
        this.stunTimeLeft = 0;
        this.isStunned = false;
        this.stateMachine.ChangeState(entity.getState(typeof(IdleState)));
      }
    }
  }
  

}
