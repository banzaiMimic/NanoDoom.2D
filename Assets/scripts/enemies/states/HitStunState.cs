using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Todo BlockStunState
public class HitStunState : State {

  

  public HitStunState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName
  ) : base(entity, stateMachine, animBoolName) {
    
  }

  public override void Enter() {
    base.Enter();
    Debug.Log("hit stun state entered..");
  }

  public override void Exit() {
    base.Exit();
    Debug.Log("hit stun state exited..");
  }
  

}
