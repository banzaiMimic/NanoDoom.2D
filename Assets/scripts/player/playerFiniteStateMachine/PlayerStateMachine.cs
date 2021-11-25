using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine {

  public PlayerState currentState { get; private set; }
  public PlayerState previousState { get; private set; }

  public void Initialize(PlayerState playerState) {
    currentState = playerState;
    currentState.Enter();
  }

  public void ChangeState(PlayerState playerState) {
    previousState = currentState;
    currentState.Exit();
    currentState = playerState;
    currentState.Enter();
  }

}
