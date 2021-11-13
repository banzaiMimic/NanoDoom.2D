using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockbackable : IHasCore {
  
  void Knockback(Vector2 angle, float strenght, int direction);

}
