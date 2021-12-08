using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// using .Linq for -> .ToList it will loop a copy instead of actual (so when destroyed there is no error thrown)

public class AggressiveWeapon : Weapon {

  protected SO_AggressiveWeaponData aggressiveWeaponData;
  private List<Entity> entityHitList = new List<Entity>();
  //private List<IDamageable> detectedDamageables = new List<IDamageable>();
  //private List<IKnockbackable> detectedKnockbackables = new List<IKnockbackable>();

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

    foreach (Entity entity in entityHitList.ToList()) {
      Debug.Log("damaging [TryHit] on entity:" + entity.name);
      //@Recall
      // @Todo if entity is in blockState -> switch them to blockStunState
      // State entityState = entity.stateMachine.currentState;
      // Debug.Log("entity in " + entityState + " state.");
      //Debug.Log("entity in " + entityState.ToString() + " state.");

      entity.stateMachine.ChangeState(entity.getState(typeof(HitStunState)));
      // default -> switch them to hitStunState
    }

    // foreach (IDamageable item in detectedDamageables.ToList()) {
    //   if (item.HasCore()) {
    //     item.Damage(details.damageAmount);
    //   } else {
    //     RemoveDamageableFromList(item);
    //   }
      
    // }
    // foreach (IKnockbackable item in detectedKnockbackables.ToList()) {
    //   if (item.HasCore()) {
    //     item.Knockback(details.knockbackAngle, details.knockbackStrength, core.Movement.facingDirection);
    //   } else {
    //     RemoveKnockbackableFromList(item);
    //   }
    // }
  }

  public void AddEntityToHitList(Entity entity) {
    entityHitList.Add(entity);
    // IDamageable damageable = collision.GetComponent<IDamageable>();

    // if (damageable != null) {
    //   //detectedDamageables.Add(damageable);
    // }

    // IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
    // if (knockbackable != null) {
    //   detectedKnockbackables.Add(knockbackable);
    // }
  }

  public void RemoveEntityFromHitList(Entity entity) {
    entityHitList.Remove(entity);
  }

  // private void RemoveKnockbackableFromList(IKnockbackable item) {
  //   detectedKnockbackables.Remove(item);
  // }

  // public void RemoveFromDetected(Entity entity) {
  //   detectedEntities.Remove(entity);
  //   //IDamageable damageable = collision.GetComponent<IDamageable>();

  //   // if (damageable != null) {
  //   //   detectedDamageables.Remove(damageable);
  //   // }

  //   // IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
  //   // if (knockbackable != null) {
  //   //   detectedKnockbackables.Remove(knockbackable);
  //   // }
  // }
}
