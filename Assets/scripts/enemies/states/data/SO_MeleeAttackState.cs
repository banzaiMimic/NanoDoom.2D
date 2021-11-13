using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/StateData/MeleeAttackState")]
public class SO_MeleeAttackState : ScriptableObject {
  
  public float attackRadius = 0.5f;
  public float attackDamage = 10f;
  public float knockbackStrength = 10f;
  public Vector2 knockbackAngle = Vector2.one;
  public LayerMask whatIsPlayer;

}
