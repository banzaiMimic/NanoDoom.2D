using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

  protected Animator baseAnimator;
  protected Animator weaponAnimator;
  protected PlayerAttackState attackState;

  protected virtual void Start() {
    this.baseAnimator = transform.Find("Base").GetComponent<Animator>();
    this.weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
    gameObject.SetActive(false);
  }

  public virtual void EnterWeapon() {
    gameObject.SetActive(true);
    baseAnimator.SetBool("attack", true);
    weaponAnimator.SetBool("attack", true);
  }

  public virtual void ExitWeapon() {
    gameObject.SetActive(false);
    baseAnimator.SetBool("attack", false);
    weaponAnimator.SetBool("attack", false);
  }

  public virtual void AnimationFinishTrigger() {
    attackState.AnimationFinished();
  }

  public void InitializeWeapon(PlayerAttackState state) {
    this.attackState = state;
  }

}
