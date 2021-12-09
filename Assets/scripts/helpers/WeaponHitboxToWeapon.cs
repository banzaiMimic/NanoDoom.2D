using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour {
  
  private AggressiveWeapon weapon;

  private void Awake() {
    weapon = GetComponentInParent<AggressiveWeapon>();
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    Entity entity = collision.GetComponentInParent<Entity>();
    Debug.Log("[WeaponHitboxToWeapon] collision enter");
    if (entity != null) {
      weapon.AddEntityToHitList(entity);
    }
    
  }

  private void OnTriggerExit2D(Collider2D collision) {
    Entity entity = collision.GetComponentInParent<Entity>();
    if (entity != null) {
      weapon.RemoveEntityFromHitList(entity);
    }
    //weapon.RemoveFromDetected(collision);
  }

}
