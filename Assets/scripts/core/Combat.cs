using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable {

  [SerializeField] private float maxHealth = 10f;
  private float currentHealth;
  private bool isKnockbackActive;
  private float knockbackStartTime;

  protected override void Awake() {
    base.Awake();
    this.currentHealth = this.maxHealth;
  }

  public void LogicUpdate() {
    CheckKnockback();
  }

  public void Damage(float amount) {
    Debug.Log(core.transform.parent.name + " Damaged!");
    Debug.Log("  amount: " + amount);
    Debug.Log("  maxHealth: " + this.maxHealth);
    Debug.Log("  currentHealth: " + this.currentHealth);
    this.currentHealth -= amount;
    Debug.Log("  currentHealthUpdate: " + this.currentHealth);
  }

  public void Knockback(Vector2 angle, float strength, int direction) {
    core.Movement.SetVelocity(strength, angle, direction);
    core.Movement.canSetVelocity = false;
    isKnockbackActive = true;
    knockbackStartTime = Time.time;
  }

  private void CheckKnockback() {
    if (isKnockbackActive && core.Movement.currentVelocity.y <= 0.01f && core.CollisionSense.Ground) {
      isKnockbackActive = false;
      core.Movement.canSetVelocity = true;
    }
  }
}
