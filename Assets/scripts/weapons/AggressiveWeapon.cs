using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// using .Linq for -> .ToList it will loop a copy instead of actual (so when destroyed there is no error thrown)

public class AggressiveWeapon : Weapon {

  protected SO_AggressiveWeaponData aggressiveWeaponData;
  private List<IDamageable> detectedDamageables = new List<IDamageable>();
  private List<IKnockbackable> detectedKnockbackables = new List<IKnockbackable>();

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
    foreach (IDamageable item in detectedDamageables.ToList()) {
      if (item.HasCore()) {
        item.Damage(details.damageAmount);
      } else {
        RemoveDamageableFromList(item);
      }
      
    }
    foreach (IKnockbackable item in detectedKnockbackables.ToList()) {
      if (item.HasCore()) {
        item.Knockback(details.knockbackAngle, details.knockbackStrength, core.Movement.facingDirection);
      } else {
        RemoveKnockbackableFromList(item);
      }
    }
  }

  public void AddToDetected(Collider2D collision) {
    IDamageable damageable = collision.GetComponent<IDamageable>();

    if (damageable != null) {
      detectedDamageables.Add(damageable);
    }

    IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
    if (knockbackable != null) {
      detectedKnockbackables.Add(knockbackable);
    }
  }

  private void RemoveDamageableFromList(IDamageable item) {
    detectedDamageables.Remove(item);
  }

  private void RemoveKnockbackableFromList(IKnockbackable item) {
    detectedKnockbackables.Remove(item);
  }

  public void RemoveFromDetected(Collider2D collision) {
    IDamageable damageable = collision.GetComponent<IDamageable>();

    if (damageable != null) {
      detectedDamageables.Remove(damageable);
    }

    IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
    if (knockbackable != null) {
      detectedKnockbackables.Remove(knockbackable);
    }
  }
}
