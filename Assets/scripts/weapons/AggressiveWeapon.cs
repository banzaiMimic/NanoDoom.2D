using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon {
  private List<IDamageable> detectedDamageable = new List<IDamageable>();

  public override void AnimationActionTrigger() {
    base.AnimationActionTrigger();
  }

  public void AddToDetected(Collider2D collision) {
    Debug.Log("[aggressiveWeapon - addToDetected]");
    IDamageable damageable = collision.GetComponent<IDamageable>();

    if (damageable != null) {
      Debug.Log("[aggressiveWeapon - added!]");
      detectedDamageable.Add(damageable);
    }
  }

  public void RemoveFromDetected(Collider2D collision) {
    Debug.Log("[aggressiveWeapon - removeFromDetected]");
    IDamageable damageable = collision.GetComponent<IDamageable>();

    if (damageable != null) {
      Debug.Log("[aggressiveWeapon - removed!]");
      detectedDamageable.Remove(damageable);
    }
  }
}
