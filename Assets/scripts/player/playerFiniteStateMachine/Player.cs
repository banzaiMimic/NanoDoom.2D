using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  public PlayerStateMachine stateMachine { get; private set; }
  public PlayerIdleState idleState { get; private set; }
  public PlayerMoveState moveState { get; private set; }
  public PlayerJumpState jumpState { get; private set; }
  public PlayerInAirState inAirState { get; private set; }
  public PlayerLandState landState { get; private set; }
  public PlayerAttackState primaryAttackState { get; private set; }
  public PlayerAttackState secondaryAttackState { get; private set; }

  public Core core { get; private set; }
  public Animator animator { get; private set; }
  public PlayerInputHandler inputHandler { get; private set; }
  public Rigidbody2D rBody { get; private set; }
  public PlayerInventory inventory { get; private set; }

  [SerializeField]
  private SO_PlayerData playerData;

  [HideInInspector]
  public MovementSM movementSm;

  private BoxCollider2D bCollider;
  private Vector2 velocityWorkspace;

  private void Awake() {
    core = GetComponentInChildren<Core>();
    Dispatcher.Instance.OnUpdatePlayerHealth(core.Combat.currentHealth, core.Combat.maxHealth);
    this.initializeStates();
  }

  private void Start() {
    this.animator = GetComponent<Animator>();
    this.inputHandler = GetComponent<PlayerInputHandler>();
    this.rBody = GetComponent<Rigidbody2D>();
    this.bCollider = GetComponent<BoxCollider2D>();
    this.stateMachine.Initialize(idleState);
    this.inventory = GetComponent<PlayerInventory>();
    this.primaryAttackState.SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
    //this.secondaryAttackState.SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
  }

  private void initializeStates() {
    this.stateMachine = new PlayerStateMachine();
    this.idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
    this.moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
    this.jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
    this.inAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
    this.landState = new PlayerLandState(this, stateMachine, playerData, "land");
    this.primaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
    this.secondaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
  }

  private void Update() {
    core.LogicUpdate();
    stateMachine.currentState.LogicUpdate();
  }

  private void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }

  private void AnimationStarted() => stateMachine.currentState.AnimationStarted();
  private void AnimationFinished() => stateMachine.currentState.AnimationFinished();

}
