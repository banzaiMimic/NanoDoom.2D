using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// using .Linq for -> .ToList it will loop a copy instead of actual (so when destroyed there is no error thrown)

public class AggressiveWeapon : Weapon {

  protected SO_AggressiveWeaponData aggressiveWeaponData;
  private List<Entity> entityHitList = new List<Entity>();
  private float hitDistance = 3f;

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

  private void handleMeleeAttack() {
    Vector3 startV3 = this.core.transform.position;
    Vector2 origin = new Vector2( startV3.x, startV3.y );
    // @Todo end should be direction of d-pad + someDistance from origin 
    Vector2 lastRawInput = Combos.Instance.lastRawInput;
    float endX = (origin.x + lastRawInput.x) + hitDistance * this.core.Movement.facingDirection;
    //float endY = (origin.y + lastRawInput.y) + hitDistance
    //Vector2 end = new Vector2( ,  );
    //RaycastHit2D checkEnemies = Physics2D.Linecast( origin, );
  }

  private void CheckMeleeAttack() {
    WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];



    // foreach (Entity entity in entityHitList.ToList()) {
    //   Debug.Log("damaging [TryHit] on entity:" + entity.name + " Count is [" + entityHitList.Count);
    //   //@Recall
    //   // @Todo if entity is in blockState -> switch them to blockStunState
    //   // State entityState = entity.stateMachine.currentState;
    //   // Debug.Log("entity in " + entityState + " state.");
    //   //Debug.Log("entity in " + entityState.ToString() + " state.");
      
    //   entity.stateMachine.ChangeState(entity.getState(typeof(HitStunState)));
    //   // default -> switch them to hitStunState
    // }

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
    Debug.Log("Adding to hitlist...");
    if (!this.entityHitList.Contains(entity)) {
      this.entityHitList.Add(entity);
    }
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
    Debug.Log("Removing from hitlist...");
    if (this.entityHitList.Contains(entity)) {
      this.entityHitList.Remove(entity);
    }
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
