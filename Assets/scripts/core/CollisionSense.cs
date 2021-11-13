using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSense : CoreComponent {

  public Transform GroundCheck { 
    get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
    private set => groundCheck = value; 
  }
  public Transform WallCheck { 
    get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
    private set => wallCheck = value; 
  }
  public Transform LedgeCheck { 
    get => GenericNotImplementedError<Transform>.TryGet(ledgeCheck, core.transform.parent.name);
    private set => ledgeCheck = value; 
  }
  public float GroundCheckRadius { get => groundCheckRadius; private set => groundCheckRadius = value; }
  public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
  public LayerMask WhatIsGround { get => whatIsGround; private set => whatIsGround = value; }

  [SerializeField] private Transform groundCheck;
  [SerializeField] private Transform wallCheck;
  [SerializeField] private Transform ledgeCheck;
  [SerializeField] private float groundCheckRadius;
  [SerializeField] private float wallCheckDistance;
  [SerializeField] private LayerMask whatIsGround;

  public bool CheckIfGrounded() {
    return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
  }

  public bool Ground {
    get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
  }

  public bool WallFront {
    get => Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.facingDirection, wallCheckDistance, whatIsGround);
  }

}
