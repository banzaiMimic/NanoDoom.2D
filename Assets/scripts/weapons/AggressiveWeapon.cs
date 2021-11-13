using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Debug.Log("CheckMeleeAttack >> ------");
    WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];
    foreach (IDamageable item in detectedDamageable) {
      Debug.Log("checking melee attack found idamageable ------");
      item.Damage(details.damageAmount);
    }
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
