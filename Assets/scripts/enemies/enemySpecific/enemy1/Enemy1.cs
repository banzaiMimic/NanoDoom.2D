using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity {
  
  public E1_IdleState idleState { get; private set; }
  public E1_MoveState moveState { get; private set; }
  public E1_PlayerDetectedState playerDetectedState { get; private set; }
  public E1_ChargeState chargeState { get; private set; }
  public E1_LookForPlayerState lookForPlayerState { get; private set; }
  public E1_MeleeAttackState meleeAttackState { get; private set; }
  public HitStunState hitStunState { get; private set; }

  [SerializeField]
  private SO_IdleState idleStateData;
  [SerializeField]
  private SO_MoveState moveStateData;
  [SerializeField]
  private SO_PlayerDetectedState playerDetectedStateData;
  [SerializeField]
  private SO_ChargeState chargeStateData;
  [SerializeField]
  private SO_LookForPlayerState lookForPlayerStateData;
  [SerializeField]
  private SO_MeleeAttackState meleeAttackStateData;

  private Enemy1 myself;
  private Vector3 startPos;

  [SerializeField]
  private Transform meleeAttackPosition;

  public override void Awake() {
    base.Awake();
    this.moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
    this.idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
    this.playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    this.chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
    this.lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
    this.meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    this.hitStunState = new HitStunState(this, stateMachine, "hitStun" );
    
    this.states.Add(typeof(MoveState), this.moveState);
    this.states.Add(typeof(IdleState), this.idleState);
    this.states.Add(typeof(PlayerDetectedState), this.playerDetectedState);
    this.states.Add(typeof(ChargeState), this.chargeState);
    this.states.Add(typeof(LookForPlayerState), this.lookForPlayerState);
    this.states.Add(typeof(MeleeAttackState), this.meleeAttackState);
    this.states.Add(typeof(HitStunState), this.hitStunState);
  }

  public void OnEnable() {
    myself = this;
    startPos = this.transform.position;
  }

  public void OnDisable() {
    //CloneMyself();
  }

  private void CloneMyself() {
    Enemy1 newMe = Instantiate (myself, startPos, Quaternion.identity);
    newMe.GetComponent<Animator>().enabled = true;
    newMe.GetComponent<BoxCollider2D>().enabled = true;
    newMe.enabled = true;
  }

  public override void Start() {
    stateMachine.Initialize(moveState);
  }

  public override void OnDrawGizmos() {
    base.OnDrawGizmos();
    DrawMeleeAttackRadius();
  }

  private void DrawMeleeAttackRadius() {
    Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
  }

}
