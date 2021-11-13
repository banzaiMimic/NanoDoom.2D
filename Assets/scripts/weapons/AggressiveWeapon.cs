using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// using .Linq for -> .ToList it will loop a copy instead of actual (so when destroyed there is no error thrown)

public class AggressiveWeapon : Weapon {

  protected SO_AggressiveWeaponData aggressiveWeaponData;
  private List<IDamageable> detectedDamageable = new List<IDamageable>();

  protected override void Awake() {
    base.Awake();
    if (weaponData.GetType() == typeof(SO_AggressiveWeaponData)) {
      aggressiveWeaponData = (SO_AggressiveWeaponData) weaponData;
    } else {
      Debug.LogError("[AggressiveWeapon] wrong data for weaponData");
    }
  }

  public override void AnimationActionTrigger() {
    base.AnimationActionTrigger();
    CheckMeleeAttack();
  }

  private void CheckMeleeAttack() {
    WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];
    foreach (IDamageable item in detectedDamageable.ToList()) {
      item.Damage(details.damageAmount);
    }
  }

  public void AddToDetected(Collider2D collision) {
    IDamageable damageable = collision.GetComponent<IDamageable>();

    if (damageable != null) {
      detectedDamageable.Add(damageable);
    }
  }

  public void RemoveFromDetected(Collider2D collision) {
    IDamageable damageable = collision.GetComponent<IDamageable>();

    if (damageable != null) {
      detectedDamageable.Remove(damageable);
    }
  }
}
