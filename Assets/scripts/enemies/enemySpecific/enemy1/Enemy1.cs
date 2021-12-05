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
    moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
    idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
    playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
    lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
    meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
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
