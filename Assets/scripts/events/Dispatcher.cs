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
  public event Action<float, int> OnTriggerPlayerHitAction;
  public event Action<int, int> OnUpdatePlayerAbilityChargesAction;
  public event Action<AbilityType, float> OnPlayerAbilityAction;
  public event Action OnPlayerDeathAction;
  public event Action<Player> OnPlayerRespawnAction;
  public event Action<float> OnScoreUpdateAction;
  public event Action OnPrimaryAttackStateChangeRequestAction;
  public event Action OnPrimaryAttackAction;
  public event Action<float> OnHitStopAction;

  static Dispatcher() { }
  private Dispatcher() { }

  public static Dispatcher Instance {
    get { return instance; }
  }

  public void HitStop(float duration) {
    OnHitStopAction?.Invoke(duration);
  }

  public void OnPrimaryAttack() {
    OnPrimaryAttackAction?.Invoke();
  }

  public void OnPrimaryAttackStateChangeRequest() {
    OnPrimaryAttackStateChangeRequestAction?.Invoke();
  }

  public void OnScoreUpdate(float score) {
    OnScoreUpdateAction?.Invoke(score);
  }

  public void OnPlayerRespawn(Player player) {
    OnPlayerRespawnAction?.Invoke(player);
  }

  public void OnPlayerDeath() {
    OnPlayerDeathAction?.Invoke();
  }

  public void OnUpdatePlayerAbilityCharges(int charges, int max) {
    OnUpdatePlayerAbilityChargesAction?.Invoke(charges, max);
  }

  public void OnPlayerAbility(AbilityType abilityType, float cdTime) {
    OnPlayerAbilityAction?.Invoke(abilityType, cdTime);
  }

  public void OnPlayerJump() {
    OnPlayerJumpAction?.Invoke();
  }
  public void OnPlayerLand() {
    OnPlayerLandAction?.Invoke();
  }
  public void onPlayerMeleeSwingSound() {
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
    OnPickupAction?.Invoke(collectible);
  }

  public void OnUpdatePlayerHealth(float currentHealth, float maxHealth) {
    OnUpdatePlayerHealthAction?.Invoke(currentHealth, maxHealth);
  }

  public void OnTriggerPlayerHit(float damage, int direction) {
    OnTriggerPlayerHitAction?.Invoke(damage, direction);
  }
}