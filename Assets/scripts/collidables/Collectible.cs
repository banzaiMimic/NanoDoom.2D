using System.Runtime.InteropServices;
using System;
using UnityEngine;

public enum CollectibleType {
  ABILITY,
  ABILITY_CHARGE,
  HEALTH
}

public enum AbilityType {
  NONE,
  DASH
}

public class Collectible : MonoBehaviour {

  public CollectibleType type;
  public AbilityType abilityType = AbilityType.NONE;

  private void OnEnable() {
    Dispatcher.Instance.OnPickupAction += this.handlePickup;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnPickupAction -= this.handlePickup;
  }

  void OnTriggerEnter2D(Collider2D other) {
    var player = other.GetComponent<Player>();
    if (player) {
      Dispatcher.Instance.OnPickup(this);
    }
  }

  private void handlePickup(Collectible collectible) {
    Destroy(collectible.gameObject);
  }

}
