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
  

}
