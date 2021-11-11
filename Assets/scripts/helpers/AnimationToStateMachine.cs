using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Todo might want to let Dispatcher handle allowing animators to call methods on our states
public class AnimationToStateMachine : MonoBehaviour {

  public AttackState attackState;

  private void TriggerAttack() {
    attackState.TriggerAttack();
  }

  private void FinishAttack() {
    attackState.FinishAttack();
  }

}
