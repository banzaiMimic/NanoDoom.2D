using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  public PlayerStateMachine stateMachine { get; private set; }
  public PlayerIdleState idleState { get; private set; }
  public PlayerMoveState moveState { get; private set; }
  public Animator animator { get; private set; }

  [SerializeField]
  private SO_PlayerData playerData;

  [SerializeField] 
  private LayerMask jumpableGround;

  [HideInInspector]
  public MovementSM movementSm;

  private Rigidbody2D rBody;
  private BoxCollider2D bCollider;
  //private ItemCollector itemCollector;

  private void Awake() {
    this.initializeStates();
    this.rBody = GetComponent<Rigidbody2D>();
    this.bCollider = GetComponent<BoxCollider2D>();
  }

  private void Start() {
    animator = GetComponent<Animator>();
    stateMachine.Initialize(idleState);
  }

  private void initializeStates() {
    stateMachine = new PlayerStateMachine();
    idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
    moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
  }

  private void Update() {
    stateMachine.currentState.LogicUpdate();
  }

  private void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }
}
