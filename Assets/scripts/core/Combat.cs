using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable {

  [SerializeField] public float maxHealth = 10f;
  [SerializeField] public GameObject[] bloodSplatters;
  [SerializeField] private Transform cameraTransform;
  public GameObject deathEffect;
  public float currentHealth;
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
        handleDeath();
      }
      Debug.Log("  currentHealthUpdate: " + this.currentHealth);
    }
  }

  private void handleDeath() {
    Instantiate(deathEffect, core.transform.position, Quaternion.identity);
    if (bloodSplatters.Length > 0) {
      splatterBlood();
    }
    Destroy(core.transform.parent.gameObject);
  }

  private void splatterBlood() {
    int randNum = UnityEngine.Random.Range(0, bloodSplatters.Length);
    GameObject blood = Instantiate(bloodSplatters[randNum], core.transform.position, Quaternion.identity);
    blood.AddComponent<ParallaxBackground>().cameraTransform = this.cameraTransform;
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
