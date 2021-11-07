using UnityEngine;

public class StateMachine : MonoBehaviour {

  BaseState currentState;

  void Start() {
    currentState = GetInitialState();
    if (currentState != null) {
      currentState.Enter();
    }
  }

  void Update() {
    if (currentState != null) {
      currentState.Update();
    }
  }

  void LateUpdate() {
    if (currentState != null) {
      currentState.LateUpdate();
    }
  }

  public void ChangeState(BaseState newState) {
    if (currentState.name != newState.name) {
      currentState.Exit();
      currentState = newState;
      currentState.Enter();
    }
  }

  protected virtual BaseState GetInitialState() {
    return null;
  }

  private void OnGUI() {
    string content = currentState != null ? currentState.name : "(no current state)";
    GUILayout.Label($"<color='black'><size=60>{content}</size></color>");
  }
}
