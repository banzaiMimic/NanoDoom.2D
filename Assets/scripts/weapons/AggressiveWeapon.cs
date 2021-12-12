using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon {

  protected SO_AggressiveWeaponData aggressiveWeaponData;
  private List<Entity> entityHitList = new List<Entity>();
  private float hitRange = 4.86f;
  private float hitDistance = 10f;
  [SerializeField] private Transform firePoint;
  [SerializeField] private Player player;

  private int delLayer = 0;

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
    Vector2 lastRawInput = Combos.Instance.lastRawInput;
    //float hitExtensionX = lastRawInput.x * hitRange;
    float hitExtensionX = this.core.Movement.facingDirection * hitRange;
    // if (hitExtensionX == 0) {
    //   hitExtensionX = hitRange;
    //   hitExtensionX = this.core.Movement.facingDirection == -1 ? -hitExtensionX : hitExtensionX;
    // }
    //@Recall i think the hackFix is messing up things on the right direction...
    // if you're super close you can hit
    Debug.Log("[tt1:] " + hitExtensionX + " facing : " + this.core.Movement.facingDirection + " lastRawY: " + lastRawInput.y);
    Vector2 endV2 = new Vector2(origin.x + hitExtensionX, origin.y + (lastRawInput.y * this.hitRange));
    //Vector2 endV2 = new Vector2(origin.x + hitExtensionX, origin.y + lastRawInput.y);
    
    //Vector2 endV2 = new Vector2(origin.x + hitExtensionX, origin.y);
    Vector2 direction = endV2 - origin;
    //Debug.DrawLine( new Vector3(origin.x, origin.y), new Vector3(endV2.x, endV2.y), Color.yellow, 1f);
    Debug.DrawRay( new Vector3(origin.x, origin.y), direction, Color.yellow, 1f);
    RaycastHit2D[] hits = Physics2D.RaycastAll( origin, direction, this.hitDistance);

    Globals.Log("[hit -->] " + hits.Length + " count");
    for (int i = 0; i < hits.Length; i++) {
      RaycastHit2D hit = hits[i];
      Debug.Log("  [hit!] -> " + hit.transform.name);
    }
  }

}
