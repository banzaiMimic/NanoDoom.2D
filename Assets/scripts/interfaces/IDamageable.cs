using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable : IHasCore {
  
  void Damage(float amount);

}
