using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/StateData/IdleState")]
public class SO_IdleState : ScriptableObject {
  
  public float minIdleTime = 1f;
  public float maxIdleTime = 2f;

}
