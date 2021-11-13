using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable {


  public void Damage(float amount) {
    Debug.Log(amount + " damage taken");
    Destroy(gameObject);
    // instantiate particles
    // Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
    //anim.SetTrigger("damage");  
  }

}
