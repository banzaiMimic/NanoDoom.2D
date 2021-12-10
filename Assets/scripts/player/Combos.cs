using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Combos {

  private static readonly Combos instance = new Combos();
  public float ChainIfWithinTime = 1f;
  public Vector2 lastRawInput = new Vector2();
  public int lastNormalizedInputX = 0;
  public int lastNormalizedInputY = 0;

  // Explicit static constructor to tell C# compiler
  // not to mark type as beforefieldinit
  static Combos() { }
  private Combos() { }

  public void updateLastMovement(Vector2 rawInput, int normalizedX, int normalizedY) {
    this.lastRawInput = rawInput;
    this.lastNormalizedInputX = normalizedX;
    this.lastNormalizedInputY = normalizedY;
  }

  public static Combos Instance {
    get {
      return instance;
    }
  }
}