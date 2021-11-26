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
  [Header("inAirState")]
  public float variableJumpHeightMultiplier = 0.5f;
  public int startingAbilityCharges = 3;
  public int maxAbilityCharges = 9;
}
