using UnityEngine;
using UnityEngine.InputSystem;

public class Moving : Grounded {

  public Moving(MovementSM stateMachine) : base("Moving", stateMachine) {}

  public override void Enter() {
    base.Enter();
    // we need reference to our player's Animator in order to set a parameter on it.
    _movementSm.animator.SetBool("running", true);
  }

  public override void Exit() {
    base.Exit();
    _movementSm.animator.SetBool("running", false);
  }

  public override void Update() {
    base.Update();
  }

  public override void LateUpdate() {
    base.LateUpdate();
    Vector2 vel = _movementSm.rBody.velocity;
    vel.x = _movementSm.horizontalVelocity * _movementSm.speed;
    _movementSm.rBody.velocity = vel;

    if (!_movementSm.isFacingRight && _movementSm.horizontalVelocity > 0f) {
      Flip();
    } else if (_movementSm.isFacingRight && _movementSm.horizontalVelocity < 0f) {
      Flip();
    }
  }

  private void Flip() {
    _movementSm.isFacingRight = !_movementSm.isFacingRight;
    Vector3 localScale = _movementSm.player.transform.localScale;
    localScale.x *= -1f;
    _movementSm.player.transform.localScale = localScale;
  }

  public void MoveEntity(InputAction.CallbackContext context) {
    Vector2 movementInput = context.ReadValue<Vector2>();
    float moveX = movementInput.x;
    if (moveX > 0) {
      _movementSm.horizontalVelocity = 1;
    } else if (moveX < 0) {
      _movementSm.horizontalVelocity = -1;
    } else {
      _movementSm.horizontalVelocity = 0f;
    }
  }

}
