using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState {

  protected SO_MeleeAttackState stateData;

  public MeleeAttackState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName, 
    Transform attackPosition,
    SO_MeleeAttackState stateData
  ) : base(entity, stateMachine, animBoolName, attackPosition) {
    this.stateData = stateData;
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void FinishAttack() {
    base.FinishAttack();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void TriggerAttack() {
    base.TriggerAttack();
    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

    foreach (Collider2D collider in detectedObjects) {
      IDamageable damageable = collider.GetComponent<IDamageable>();
      if (damageable != null) {
        Player player = collider.GetComponentInParent<Player>();
        if (player != null) {
          if (player.stateMachine.currentState != player.dashState) {
            //Debug.Log("melee state::: damage block");
            damageable.Damage(stateData.attackDamage, stateData.knockbackStrength);
          } else {
            // Debug.Log("melee state::: superKnockBack");
            // entity.core.Combat.SuperKnockback();
          }
        }
      }

      IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();
      if (knockbackable != null) {
        knockbackable.Knockback(stateData.knockbackAngle, stateData.knockbackStrength, core.Movement.facingDirection);
      }
    }
  }
}
