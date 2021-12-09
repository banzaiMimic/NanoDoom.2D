using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Combos {

  private static readonly Combos instance = new Combos();
  public float ChainIfWithinTime = 1f;
  public int lastNormalizedInputX = 0;
  public int lastNormalizedInputY = 0;

  // Explicit static constructor to tell C# compiler
  // not to mark type as beforefieldinit
  static Combos() { }
  private Combos() { }

  public void updateLastMovement(int x, int y) {
    this.lastNormalizedInputX = x;
    this.lastNormalizedInputY = y;
  }

  public static Combos Instance {
    get {
      return instance;
    }
  }
}