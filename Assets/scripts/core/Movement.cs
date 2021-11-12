using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent {

  [SerializeField]
  public Transform tForm;

  public Rigidbody2D rBody { get; private set; }
  public Vector2 currentVelocity { get; private set; }
  public int facingDirection { get; private set; }

  private Vector2 velocityWorkspace;

  protected override void Awake() {
    base.Awake();
    rBody = GetComponentInParent<Rigidbody2D>();
    this.facingDirection = 1;
  }

  public void LogicUpdate() {
    currentVelocity = rBody.velocity;
  }

  public void SetVelocityX(float velocity) {
    velocityWorkspace.Set(velocity, currentVelocity.y);
    rBody.velocity = velocityWorkspace;
    currentVelocity = velocityWorkspace;
  }

  public void SetVelocityY(float velocity) {
    velocityWorkspace.Set(currentVelocity.x, velocity);
    rBody.velocity = velocityWorkspace;
    currentVelocity = velocityWorkspace;
  }

  public void SetVelocityZero() {
    rBody.velocity = Vector2.zero;
    currentVelocity = Vector2.zero;
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

  public void CheckIfShouldFlip(int xInput) {
    if (xInput != 0 && xInput != facingDirection) {
      Flip();
    }
  }

  public void Flip() {
    facingDirection *= -1;
    tForm.Rotate(0.0f, 180.0f, 0.0f);
  }
}
