using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
  
  public Movement Movement { 
    get => GenericNotImplementedError<Movement>.TryGet(movement, transform.parent.name); 
    private set => movement = value;
  }
  public CollisionSense CollisionSense { 
    get => GenericNotImplementedError<CollisionSense>.TryGet(collisionSense, transform.parent.name); 
    private set => collisionSense = value;
  }
  public Combat Combat {
    get => GenericNotImplementedError<Combat>.TryGet(combat, transform.parent.name); 
    private set => combat = value;
  }
  
  public Movement movement;
  public CollisionSense collisionSense;
  public Combat combat;

  private void Awake() {
    Movement = GetComponentInChildren<Movement>();
    CollisionSense = GetComponentInChildren<CollisionSense>();
    Combat = GetComponentInChildren<Combat>();
  }

  public void LogicUpdate() {
    Movement.LogicUpdate();
    Combat.LogicUpdate();
  }
}
