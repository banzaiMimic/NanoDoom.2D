using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
  makeshift singleton
*/
public sealed class Globals {

  private static readonly Globals instance = new Globals();
  public float SpawnFlyingEnemyTime = 4f;

  // Explicit static constructor to tell C# compiler
  // not to mark type as beforefieldinit
  static Globals() { }
  private Globals() { }

  public static void Log(string msg) {
    Debug.Log($"<color=#00FF00>" + msg + "</color>");
  }

  public static void LogP(string msg) {
    Debug.Log($"<color=#FFA500>" + msg + "</color>");
  }

  public static Globals Instance {
    get {
      return instance;
    }
  }
}