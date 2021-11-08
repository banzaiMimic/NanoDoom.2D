using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName ="Data/PlayerData/BaseData")]
public class SO_PlayerData : ScriptableObject {
  [Header("moveState")]
  public float moveVelocity = 10f;
  [Header("jumpState")]
  public float jumpVelocity = 15f;
  public int amountOfJumps = 1;
  [Header("checkVars")]
  public float groundCheckRadius = 0.3f;
  public LayerMask whatIsGround;
  [Header("inAirState")]
  public float variableJumpHeightMultiplier = 0.5f;
}
