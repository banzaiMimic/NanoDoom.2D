using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

  public PlayerStateMachine stateMachine { get; private set; }
  public PlayerIdleState idleState { get; private set; }
  public PlayerMoveState moveState { get; private set; }
  public PlayerJumpState jumpState { get; private set; }
  public PlayerInAirState inAirState { get; private set; }
  public PlayerLandState landState { get; private set; }
  public PlayerAttackState primaryAttackState { get; private set; }
  public PlayerDashState dashState { get; private set; }
  public List<PlayerAbilityState> unlockedAbilities { get; private set; }
  public PlayerAbilityState activeAbility { get; private set; }

  public Core core { get; private set; }
  public Animator animator { get; private set; }
  public PlayerInputHandler inputHandler { get; private set; }
  public Rigidbody2D rBody { get; private set; }
  public PlayerInventory inventory { get; private set; }

  [SerializeField]
  private SO_PlayerData playerData;
  [SerializeField]
  private GameObject abilityDashUi;

  [HideInInspector]
  public MovementSM movementSm;

  private BoxCollider2D bCollider;
  private Vector2 velocityWorkspace;

  private void Awake() {
    this.abilityDashUi.SetActive(false);
    this.unlockedAbilities = new List<PlayerAbilityState>();
    core = GetComponentInChildren<Core>();
    this.initializeStates();
    Dispatcher.Instance.OnUpdatePlayerHealth(core.Combat.currentHealth, core.Combat.maxHealth);
    Dispatcher.Instance.OnUpdatePlayerAbilityCharges( playerData.startingAbilityCharges, playerData.maxAbilityCharges);
  }

  private void initializeStates() {
    this.stateMachine = new PlayerStateMachine();
    this.idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
    this.moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
    this.jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
    this.inAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
    this.landState = new PlayerLandState(this, stateMachine, playerData, "land");
    this.primaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
    this.dashState = new PlayerDashState(this, stateMachine, playerData, "dash");
  }

  public PlayerAbilityState GetActiveAbility() {
    return this.activeAbility;
  }

  public void SetActiveAbility(PlayerAbilityState abilityState) {
    this.activeAbility = abilityState;
  }

  public void UnlockAbility(PlayerAbilityState abilityState) {
    if (!this.unlockedAbilities.Contains(abilityState)) {
      this.unlockedAbilities.Add(abilityState);
      this.SetActiveAbility(abilityState);
    }
  }

  private void OnEnable() {
    Dispatcher.Instance.OnTriggerPlayerHitAction += this.TriggerPlayerHit;
    Dispatcher.Instance.OnPickupAction += this.HandlePickup;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnTriggerPlayerHitAction -= this.TriggerPlayerHit;
    Dispatcher.Instance.OnPickupAction -= this.HandlePickup;
  }

  private void Start() {
    this.animator = GetComponent<Animator>();
    this.inputHandler = GetComponent<PlayerInputHandler>();
    this.rBody = GetComponent<Rigidbody2D>();
    this.bCollider = GetComponent<BoxCollider2D>();
    this.stateMachine.Initialize(idleState);
    this.inventory = GetComponent<PlayerInventory>();
    this.primaryAttackState.SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
  }

  private void Update() {
    core.LogicUpdate();
    stateMachine.currentState.LogicUpdate();
    PreventInfiniteFall();
  }

  private void PreventInfiniteFall() {
    if (gameObject.transform.position.y <= -100f) {
      SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
  }

  private void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }

  private void AnimationStarted() => stateMachine.currentState.AnimationStarted();
  private void AnimationFinished() => stateMachine.currentState.AnimationFinished();

  private void TriggerPlayerHit(float damage, int direction) {
    core.Combat.Damage(damage);
    float knockBackStrength = 12f;
    core.Combat.Knockback(new Vector2(direction, 2), knockBackStrength, direction);
  }

  private void HandlePickup(Collectible collectible) {
    switch (collectible.type) {
      case CollectibleType.ABILITY_CHARGE:
        int chargeUpdate = this.dashState.AddCharge();
        Dispatcher.Instance.OnUpdatePlayerAbilityCharges(chargeUpdate, this.dashState.GetMaxCharges());
      break;
      case CollectibleType.HEALTH:

      break;
      case CollectibleType.ABILITY:
        switch (collectible.abilityType) {
          case AbilityType.DASH:
            this.abilityDashUi.SetActive(true);
            UnlockAbility(this.dashState);
          break;
          default:
          break;
        }
      break;
    }
    
  }

}
