using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

  [SerializeField] protected SO_WeaponData weaponData;

  protected Animator baseAnimator;
  protected Animator weaponAnimator;
  protected PlayerAttackState attackState;
  protected int attackCounter;

  protected virtual void Awake() {
    this.baseAnimator = transform.Find("Base").GetComponent<Animator>();
    this.weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
    gameObject.SetActive(false);
  }

  public virtual void EnterWeapon() {
    gameObject.SetActive(true);
    if (attackCounter >= weaponData.amountOfAttacks) {
      attackCounter = 0;
    }
    baseAnimator.SetBool("attack", true);
    weaponAnimator.SetBool("attack", true);
    baseAnimator.SetInteger("attackCounter", attackCounter);
    weaponAnimator.SetInteger("attackCounter", attackCounter);
  }

  public virtual void ExitWeapon() {
    baseAnimator.SetBool("attack", false);
    weaponAnimator.SetBool("attack", false);
    attackCounter++;
    gameObject.SetActive(false);
  }

  public virtual void AnimationFinishTrigger() {
    attackState.AnimationFinished();
  }

  public virtual void AnimationStartMovementTrigger() {
    attackState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
  }

  public virtual void AnimationStopMovementTrigger() {
    attackState.SetPlayerVelocity(0f);
  }

  public virtual void AnimationTurnOffFlipTrigger() {
    attackState.SetFlipCheck(false);
  }

  public virtual void AnimationTurnOnFlipTrigger() {
    attackState.SetFlipCheck(true);
  }

  public virtual void AnimationActionTrigger() {}

  public void InitializeWeapon(PlayerAttackState state) {
    this.attackState = state;
  }

}
