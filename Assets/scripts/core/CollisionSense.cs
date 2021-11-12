using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSense : MonoBehaviour {

  public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
  public float GroundCheckRadius { get => groundCheckRadius; private set => groundCheckRadius = value; }
  public LayerMask WhatIsGround { get => whatIsGround; private set => whatIsGround = value; }

  [SerializeField] private Transform groundCheck;
  [SerializeField] private float groundCheckRadius;
  [SerializeField] private LayerMask whatIsGround;
  
  public bool CheckIfGrounded() {
    return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
  }

}
