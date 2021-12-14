using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Combos {

  private static readonly Combos instance = new Combos();
  public float ChainIfWithinTime = 1f;
  public Vector2 hitDirection;

  // Explicit static constructor to tell C# compiler
  // not to mark type as beforefieldinit
  static Combos() { }
  private Combos() { }

  public void updateHitLine(Vector2 direction) {
    this.hitDirection = direction;
  }

  public static Combos Instance {
    get {
      return instance;
    }
  }
}