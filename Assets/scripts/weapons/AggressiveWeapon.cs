using System.Runtime.CompilerServices;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon {

  protected SO_AggressiveWeaponData aggressiveWeaponData;
  private List<Entity> entityHitList = new List<Entity>();
  private float hitRange = 4.86f;
  private float hitDistance = 3f;
  [SerializeField] private float knockbackStrength = 12f;
  [SerializeField] private float hitDamage = 5f;
  [SerializeField] private Transform firePoint;
  [SerializeField] private Player player;

  private void initListeners() {
    Dispatcher.Instance.OnPrimaryAttackAction += this.handleMeleeAttack;
  }

  protected override void Awake() {
    base.Awake();
    this.initListeners();
    if (weaponData.GetType() == typeof(SO_AggressiveWeaponData)) {
      aggressiveWeaponData = (SO_AggressiveWeaponData) weaponData;
    } else {
      Debug.LogError("[AggressiveWeapon] wrong data for weaponData");
    }
  }

  private void handleMeleeAttack() {
    Vector3 originV3 = this.firePoint.transform.position;
    Vector2 origin = new Vector2( originV3.x, originV3.y);

    if (this.comboChains > 1) {

      if (this.lastEnemyHit != null) {
        // - move player near by that position so follow up hit will land
        Vector3 enemyPos = this.lastEnemyHit.transform.position;
        float teleportOffsetX = this.core.Movement.facingDirection * 1.2f;
        Vector3 teleportToV3 = new Vector3(enemyPos.x + -teleportOffsetX, enemyPos.y, enemyPos.z);

        this.core.Movement.rBody.transform.position = teleportToV3;
      }
    }
    
    Debug.DrawRay( new Vector3(origin.x, origin.y), Combos.Instance.hitDirection * this.hitDistance, Color.yellow, 1f);
    RaycastHit2D[] hits = Physics2D.RaycastAll( origin, Combos.Instance.hitDirection, this.hitDistance);

    for (int i = 0; i < hits.Length; i++) {
      RaycastHit2D hit = hits[i];
      
      if (hit.transform.name != "Player") {
        Combat enemyCombat = hit.transform.GetComponentInChildren<Combat>();
        if (enemyCombat != null) {
          int myFacingDirection = this.core.Movement.facingDirection;
          enemyCombat.Damage(this.hitDamage * this.comboChains, this.knockbackStrength, myFacingDirection);
          //@Todo store reference to last enemy hit
          this.lastEnemyHit = enemyCombat.GetComponentInParent<Core>().Movement;
          if (this.comboChains > 1) {
            // @Todo hit-stop 
          }
        }
      }
    }
  }

}
