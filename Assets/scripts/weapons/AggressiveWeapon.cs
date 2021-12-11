using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon {

  protected SO_AggressiveWeaponData aggressiveWeaponData;
  private List<Entity> entityHitList = new List<Entity>();
  private float hitRange = 4.86f;
  [SerializeField] private Transform firePoint;

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
    //Debug.Log("[AggressiveWeapon] -> handleMeleeAttack");
    //Vector3 originV3 = this.core.transform.position;
    Vector3 originV3 = this.firePoint.transform.position;
    Vector2 origin = new Vector2( originV3.x, originV3.y);
    Vector2 lastRawInput = Combos.Instance.lastRawInput;
    float hitExtensionX = lastRawInput.x * hitRange;
    if (hitExtensionX == 0) {
      hitExtensionX = hitRange;
      hitExtensionX = this.core.Movement.facingDirection == -1 ? -hitExtensionX : hitExtensionX;
    }
    
    Debug.Log("[tt1:] " + hitExtensionX + " facing : " + this.core.Movement.facingDirection);
    Vector2 endV2 = new Vector2(origin.x + hitExtensionX, origin.y + (lastRawInput.y * this.hitRange));

    Debug.DrawLine( new Vector3(origin.x, origin.y), new Vector3(endV2.x, endV2.y), Color.yellow, 2f);
    RaycastHit2D[] hits = Physics2D.RaycastAll( origin, endV2, this.hitRange);
    Globals.Log("[hit -->] " + hits.Length + " count");
    for (int i = 0; i < hits.Length; i++)
    {
      RaycastHit2D hit = hits[i];
      Debug.Log("  [hit!] -> " + hit.transform.name);
      Renderer rend = hit.transform.GetComponent<Renderer>();

      if (rend) {
        // Change the material of all hit colliders
        // to use a transparent shader.
        rend.material.shader = Shader.Find("Transparent/Diffuse");
        Color tempColor = rend.material.color;
        tempColor.a = 0.3F;
        rend.material.color = tempColor;
      }
    }
  }

}
