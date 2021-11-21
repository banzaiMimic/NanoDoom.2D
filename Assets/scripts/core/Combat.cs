using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable {

  public GameObject deathEffect;
  [SerializeField] public float maxHealth = 10f;
  public float currentHealth;
  private bool isKnockbackActive;
  private float knockbackStartTime;

  private IEnumerator coroutine;

  private IEnumerator TestParticles(float waitTime) {
    while (true) {
      yield return new WaitForSeconds(waitTime);
      Instantiate(deathEffect, core.transform.position, Quaternion.identity);
    }
  }

  protected override void Awake() {
    base.Awake();
    this.currentHealth = this.maxHealth;
    coroutine = TestParticles(2.0f);
    StartCoroutine(coroutine);
  }

  public void LogicUpdate() {
    CheckKnockback();
  }

  public void Damage(float amount) {
    if (core != null) {
      Debug.Log(core.transform.parent.name + " Damaged!");
      Debug.Log("  amount: " + amount);
      Debug.Log("  maxHealth: " + this.maxHealth);
      Debug.Log("  currentHealth: " + this.currentHealth);
      this.currentHealth -= amount;
      if (core.transform.parent.name == "Player") {
        Dispatcher.Instance.OnUpdatePlayerHealth(currentHealth, maxHealth);
      } else {
        // leaving here for now but should have better way to handle for each entity
        Dispatcher.Instance.OnPlayerMeleeHit();
      }
      if (this.currentHealth <= 0) {
        HandleDeath();
      }
      Debug.Log("  currentHealthUpdate: " + this.currentHealth);
    }
  }

  private void HandleDeath() {
    Instantiate(deathEffect, core.transform.position, Quaternion.identity);
    Destroy(core.transform.parent.gameObject);
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

  public bool HasCore() => core != null;

}
