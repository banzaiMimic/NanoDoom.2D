using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
  
  public Movement Movement { get => movement; private set => movement = value;}
  public CollisionSense CollisionSense { get => collisionSense; private set => collisionSense = value;}
  public Movement movement;
  public CollisionSense collisionSense;

  private void Awake() {
    movement = GetComponentInChildren<Movement>();
    collisionSense = GetComponentInChildren<CollisionSense>();
  }

  public void LogicUpdate() {
    movement.LogicUpdate();
  }
}
