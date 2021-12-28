using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent {

  [SerializeField] public float maxHealth = 10f;
  [SerializeField] public GameObject[] bloodSplatters;
  [SerializeField] private Transform cameraTransform;
  [SerializeField] private GameObject drop;
  public Weapon weapon;
  public GameObject deathEffect;
  public float currentHealth;
  private bool isKnockbackActive;
  private float knockbackStartTime;
  private float knockbackMinDuration = 1.3f;

  protected override void Awake() {
    base.Awake();
    if (!this.cameraTransform) {
      this.cameraTransform = GameObject.Find("CAM").transform;
    }
    this.currentHealth = this.maxHealth;
  }

  public void LogicUpdate() {
    CheckKnockback();
  }

  public bool isBeingKnockbacked() {
    return this.isKnockbackActive;
  }

  public void Damage(float amount, float knockbackStrength, int direction) {
    if (core != null) {
      this.currentHealth -= amount;
      float offsetX = direction * 3f;
      Vector2 endLocationV2 = new Vector2( this.core.Movement.transform.position.x + (2f * direction), this.core.Movement.transform.position.y + 3f);
      Vector2 myLocationV2 = new Vector2( this.core.Movement.transform.position.x, this.core.Movement.transform.position.y );
      Vector2 angle = endLocationV2 - myLocationV2;
      
      this.Knockback(angle, knockbackStrength, direction);

      if (core.transform.parent.name == "Player") {
        Dispatcher.Instance.OnUpdatePlayerHealth(currentHealth, maxHealth);
      } else {
        
      }
      if (this.currentHealth <= 0) {
        handleDeath(core.transform.parent.name);
      }
    }
  }
  public void DamageWithoutKnockback(float amount, float knockbackStrength, int direction) {
    if (core != null) {
      this.currentHealth -= amount;
      float offsetX = direction * 3f;
      Vector2 endLocationV2 = new Vector2( this.core.Movement.transform.position.x + (2f * direction), this.core.Movement.transform.position.y + 3f);
      Vector2 myLocationV2 = new Vector2( this.core.Movement.transform.position.x, this.core.Movement.transform.position.y );
      Vector2 angle = endLocationV2 - myLocationV2;

      if (core.transform.parent.name == "Player") {
        Dispatcher.Instance.OnUpdatePlayerHealth(currentHealth, maxHealth);
      } else {
        
      }
      if (this.currentHealth <= 0) {
        handleDeath(core.transform.parent.name);
      }
    }
  }
  public void SuperDamage(float amount, int direction) {
    if (core != null) {
      this.currentHealth -= amount;
      float offsetX = direction * 3f;
      float randStrength = Random.Range(50f, 100f);
      
      Vector2 endLocationV2 = new Vector2( this.core.Movement.transform.position.x + (2f * direction), this.core.Movement.transform.position.y + 3f);
      Vector2 myLocationV2 = new Vector2( this.core.Movement.transform.position.x, this.core.Movement.transform.position.y );
      Vector2 angle = endLocationV2 - myLocationV2;
      
      this.Knockback(Combos.Instance.hitDirection, randStrength, direction);

      if (this.currentHealth <= 0) {
        Globals.Log("handle delayed death?");
        // @Todo handle delayed death?
        //handleDeath(core.transform.parent.name);
      }
    }
  }
  public void Knockback(Vector2 angle, float strength, int direction) {
    knockbackStartTime = Time.time;
    isKnockbackActive = true;
    core.Movement.SetVelocity(strength, angle, direction);
    core.Movement.canSetVelocity = false;
  }
  public void SuperKnockback() {
    float randDamage = Random.Range(20f, 40f);
    float randX = Random.Range(20f, 500f);
    float randY = Random.Range(20f, 500f);
    float randStrength = Random.Range(50f, 100f);
    int randDirection = randDamage >= 30f ? 1 : -1;
    
    Globals.Log("SuperKnockback --: " + randDamage + " randX: " + randX + " randY: " + randY);
    DamageWithoutKnockback(randDamage, randStrength, randDirection);
    Knockback(new Vector2(randX, randY), randStrength, 1);
  }

  private void handleDeath(string name) {
    Instantiate(deathEffect, core.transform.position, Quaternion.identity);
    if (bloodSplatters.Length > 0) {
      splatterBlood();
    }
    Destroy(core.transform.parent.gameObject);
    if (name == "Player") {
      Dispatcher.Instance.OnPlayerDeath();
    } else {
      Dispatcher.Instance.OnScoreUpdate(.86f);
    }
    if (drop != null) {
      ThrowDrop();
    }
  }

  private void ThrowDrop() {
    Instantiate(drop, core.transform.position, Quaternion.identity);
  }

  private void splatterBlood() {
    int randNum = UnityEngine.Random.Range(0, bloodSplatters.Length);
    GameObject blood = Instantiate(bloodSplatters[randNum], core.transform.position, Quaternion.identity);
    blood.transform.eulerAngles = new Vector3( blood.transform.rotation.x, blood.transform.rotation.y, UnityEngine.Random.Range(0, 360));
    blood.AddComponent<BloodSplatterMovement>().cameraTransform = this.cameraTransform;
  }

  private void CheckKnockback() {
    if (isKnockbackActive && core.Movement.currentVelocity.y <= 0.01f && core.CollisionSense.Ground) {
      core.Movement.canSetVelocity = true;
      if (knockbackMinDuration <= (Time.time - knockbackStartTime)) {
        isKnockbackActive = false;
      }
    }
  }

  public bool HasCore() => core != null;

}
