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
  [SerializeField] private float knockbackStrength = 2f;
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

  // to help chain combos, will teleport player to enemy with a slight offset on x so that following hit will land
  private void tryTeleportPlayerToChainCombo() {
    if (this.comboChains > 1) {
      if (this.lastEnemyHit != null) {
        // - move player near by that position so follow up hit will land
        Vector3 enemyPos = this.lastEnemyHit.transform.position;
        Vector2 enemyAngle = new Vector2(enemyPos.x, enemyPos.y) - new Vector2( this.core.Movement.rBody.transform.position.x, this.core.Movement.rBody.transform.position.y );
        float teleportOffsetX = this.core.Movement.facingDirection * 1.2f;
        Vector3 teleportToV3 = new Vector3(enemyPos.x + -teleportOffsetX, enemyPos.y, enemyPos.z);
        //this.core.Movement.QuickBurst(20f, enemyAngle);
        this.core.Movement.rBody.transform.position = teleportToV3;
        // @Todo try adding a smoke cloud where player was...
        
      }
    }
  }

  private void handleMeleeAttack() {

    tryTeleportPlayerToChainCombo();

    int myFacingDirection = this.core.Movement.facingDirection;
    Vector3 originV3 = this.firePoint.transform.position;
    Vector2 origin = new Vector2( originV3.x, originV3.y);
    
    Debug.DrawRay( new Vector3(origin.x, origin.y), Combos.Instance.hitDirection * this.hitDistance, Color.yellow, 1f);
    RaycastHit2D[] hits = Physics2D.RaycastAll( origin, Combos.Instance.hitDirection, this.hitDistance);

    bool hitMade = false;
    // check hits
    for (int i = 0; i < hits.Length; i++) {
      RaycastHit2D hit = hits[i];
      
      if (hit.transform.name != "Player") {
        Combat enemyCombat = hit.transform.GetComponentInChildren<Combat>();
        if (enemyCombat != null) {
          hitMade = true;
          this.lastEnemyHit = enemyCombat.GetComponentInParent<Core>().Movement;
          if (this.comboChains == 1) {
            //Dispatcher.Instance.HitStop(.1f);
            enemyCombat.Damage(this.hitDamage, this.knockbackStrength, myFacingDirection);
          } else if (this.comboChains == 2) {
            //Dispatcher.Instance.HitStop(.2f);
            enemyCombat.Damage(this.hitDamage, this.knockbackStrength, myFacingDirection);
          } else if (this.comboChains == 3) {
            enemyCombat.SuperDamage(this.hitDamage, myFacingDirection);
          }
        } 
      }
    }
    if (hitMade) {
      Dispatcher.Instance.OnPlayerMeleeHit();
    }
  }
}
