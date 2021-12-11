using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent {

  [SerializeField]
  public Transform tForm;

  public Rigidbody2D rBody { get; private set; }
  public Vector2 currentVelocity { get; private set; }
  public int facingDirection { get; private set; }
  public bool canSetVelocity { get; set; }
  public bool canFlip { get; set; }
  public bool canMove { get; private set; }
  public float baseSpeedMultiplier = 1;

  private Vector2 velocityWorkspace;

  protected override void Awake() {
    base.Awake();
    this.rBody = GetComponentInParent<Rigidbody2D>();
    this.facingDirection = 1;
    this.canMove = true;
    this.canSetVelocity = true;
    this.canFlip = true;
  }

  public void DisableMovement() {
    //Debug.Log("Movement disabled.");
    this.SetVelocityZero();
    this.canMove = false;
    this.canSetVelocity = false;
    this.canFlip = false;
  }

  public void EnableMovement() {
    //Debug.Log("Movement enabled.");
    this.canSetVelocity = true;
    this.canFlip = true;
    this.canMove = true;
  }

  public void LogicUpdate() {
    currentVelocity = rBody.velocity;
  }

  public void SetVelocityX(float velocity) {
    velocityWorkspace.Set(velocity, currentVelocity.y);
    SetFinalVelocity();
  }

  public void SetVelocityY(float velocity) {
    velocityWorkspace.Set(currentVelocity.x, velocity);
    SetFinalVelocity();
  }

  public void SetVelocityZero() {
    velocityWorkspace = Vector2.zero;
    SetFinalVelocity();
  }

  private void SetFinalVelocity() {
    if (canSetVelocity) {
      rBody.velocity = velocityWorkspace;
      currentVelocity = velocityWorkspace;
    }
  }

  public void SetVelocity(float velocity, Vector2 angle, int direction) {
    angle.Normalize();
    velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
    rBody.velocity = velocityWorkspace;
    currentVelocity = velocityWorkspace;
  }
  
  public void SetVelocity(float velocity, Vector2 direction) {
    velocityWorkspace = direction * velocity;
    rBody.velocity = velocityWorkspace;
    currentVelocity = velocityWorkspace;
  }

  public void SetVelocity(float velX, float velY) {
    velocityWorkspace = new Vector2(velX, velY);
    rBody.velocity = velocityWorkspace;
    currentVelocity = velocityWorkspace;
  }

  public void CheckIfShouldFlip(int xInput) {
    if (xInput != 0 && xInput != facingDirection) {
      Flip();
    }
  }

  public void Flip() {
    if (canFlip) {
      facingDirection *= -1;
      tForm.Rotate(0.0f, 180.0f, 0.0f);
    }
  }
}
