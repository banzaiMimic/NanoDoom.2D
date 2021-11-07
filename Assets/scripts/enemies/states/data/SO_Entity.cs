using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/EntityData/BaseData")]
public class SO_Entity : ScriptableObject {
  
  public float wallCheckDistance = 0.2f;
  public float ledgeCheckDistance = 0.4f;
  public float minAggroDistance = 3f;
  public float maxAggroDistance = 4f;
  public float closeRangeActionDistance = 1f;
  public LayerMask whatIsGround;
  public LayerMask whatIsPlayer;

}
