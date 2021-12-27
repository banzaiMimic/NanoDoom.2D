using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
  
  public FiniteStateMachine stateMachine;
  public SO_Entity entityData;
  public Animator animator { get; private set; }
  public AnimationToStateMachine atsm { get; private set; }
  public Vector2 velocityWorkspace;
  public Core core { get; private set; }
  
  protected Dictionary<Type, State> states = new Dictionary<Type, State>();

  [SerializeField]
  private Transform wallCheck;
  [SerializeField]
  private Transform ledgeCheck;
  [SerializeField]
  private Transform playerCheck;

  public virtual void Awake() {
    core = GetComponentInChildren<Core>();
    animator = GetComponent<Animator>();
    atsm = GetComponent<AnimationToStateMachine>();
    stateMachine = new FiniteStateMachine();
  }

  public State getState(Type type) {
    if (this.states.ContainsKey(type)) {
      return this.states[type];
    } else {
      throw new Exception("State [" + type + "] not found on " + this.ToString());
    }
  }

  public virtual void Start() {

  }

  public virtual void Update() {
    core.LogicUpdate();
    stateMachine.currentState.LogicUpdate();
  }

  public virtual void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }

  public virtual bool CheckWall() {
    return Physics2D.Raycast(wallCheck.position, transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
  }

  public virtual bool CheckLedge() {
    return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
  }

  public virtual bool CheckPlayerInMinAggroRange() {
    return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAggroDistance, entityData.whatIsPlayer);
  }

  public virtual bool CheckPlayerInMaxAggroRange() {
    return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAggroDistance, entityData.whatIsPlayer);
  }

  public virtual bool CheckPlayerInCloseRangeAction() {
    if (this.core.Combat.isBeingKnockbacked()) {
      return false;
    } else {
      return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {

    var player = collision.GetComponent<Player>();
    Player playerCheck = collision.GetComponentInParent<Player>();

    if (playerCheck && playerCheck.stateMachine.currentState != playerCheck.dashState) {
      if (collision.transform.position.x < collision.transform.position.x) {
        // player on left
        Dispatcher.Instance.OnTriggerPlayerHit(10f, -1);
      } else {
        // player on right
        Dispatcher.Instance.OnTriggerPlayerHit(10f, -1);
      }
    } 
    
    if (playerCheck && collision.transform.name == "PlayerCombat" && playerCheck.stateMachine.currentState == playerCheck.dashState) {
      // entity collided while player in dash state
      this.core.Combat.SuperKnockback();
    }

  }

  public void DestroyEntity() {
    Destroy(this.gameObject);
  }

}
