using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon {

  protected SO_AggressiveWeaponData aggressiveWeaponData;
  private List<Entity> entityHitList = new List<Entity>();
  private float hitRange = 4.86f;
  private float hitDistance = 3f;
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
    Globals.Log("[AggressiveWeapon.handleMeleeAttack] currentState: " + this.player.stateMachine.currentState);
    Vector3 originV3 = this.firePoint.transform.position;
    Vector2 origin = new Vector2( originV3.x, originV3.y);
    
    Debug.DrawRay( new Vector3(origin.x, origin.y), Combos.Instance.hitDirection * this.hitDistance, Color.yellow, 1f);
    RaycastHit2D[] hits = Physics2D.RaycastAll( origin, Combos.Instance.hitDirection, this.hitDistance);

    Globals.Log("[hit -->] " + hits.Length + " count");
    for (int i = 0; i < hits.Length; i++) {
      RaycastHit2D hit = hits[i];
      Debug.Log("  [hit!] -> " + hit.transform.name + " comboChains: " + this.comboChains);
    }
  }

}
