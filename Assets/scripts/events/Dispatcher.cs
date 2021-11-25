using System;
using UnityEngine;

/**
  makeshift event dispatcher for small games
*/
public sealed class Dispatcher {

  private static readonly Dispatcher instance = new Dispatcher();
  public event Action<Collectible> OnPickupAction;
  public event Action<float, float> OnUpdatePlayerHealthAction;
  public event Action OnPlayerMoveStateEnterAction;
  public event Action OnPlayerMoveStateExitAction;
  public event Action OnPlayerJumpAction;
  public event Action OnPlayerLandAction;
  public event Action OnPlayerMeleeSwingAction;
  public event Action OnPlayerMeleeHitAction;

  static Dispatcher() { }
  private Dispatcher() { }

  public static Dispatcher Instance {
    get { return instance; }
  }

  public void OnPlayerJump() {
    OnPlayerJumpAction?.Invoke();
  }
  public void OnPlayerLand() {
    OnPlayerLandAction?.Invoke();
  }
  public void OnPlayerMeleeSwing() {
    OnPlayerMeleeSwingAction?.Invoke();
  }
  public void OnPlayerMeleeHit() {
    OnPlayerMeleeHitAction?.Invoke();
  }

  public void OnPlayerMoveStateEnter() {
    OnPlayerMoveStateEnterAction?.Invoke();
  }

  public void OnPlayerMoveStateExit() {
    OnPlayerMoveStateExitAction?.Invoke();
  }

  public void OnPickup(Collectible collectible) {
    Debug.Log("[Dispatcher] OnPickup received <-");
    OnPickupAction?.Invoke(collectible);
  }

  public void OnUpdatePlayerHealth(float currentHealth, float maxHealth) {
    OnUpdatePlayerHealthAction?.Invoke(currentHealth, maxHealth);
  }
}