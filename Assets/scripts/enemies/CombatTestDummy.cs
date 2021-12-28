using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable {

  [SerializeField] private GameObject drop;

  public void Damage(float amount, float knockbackStrength) {
    Instantiate(drop, transform.position, Quaternion.identity);
    //Destroy(gameObject);
  }

  public bool HasCore() {
    return false;
  }
}
