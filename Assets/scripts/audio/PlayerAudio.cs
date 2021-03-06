using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip bgAmbient;
  [SerializeField] private AudioClip bgBoss;
  [SerializeField] private AudioClip[] footsteps;
  [SerializeField] private AudioClip jump;
  [SerializeField] private AudioClip land;
  [SerializeField] private AudioClip meleeSwing;
  [SerializeField] private AudioClip meleeHit;
  [SerializeField] private bool muteBgAudio;
  public float timeBetweenSteps = 0.39f;

  private float timer;
  private bool isMoving = false;

  private void Start() {
    audioSource.clip = bgBoss;
    audioSource.loop = true;
    audioSource.volume = .6f;
    if (!muteBgAudio) {
      audioSource.Play();
    }
  }
  
  private void OnEnable() {
    Dispatcher.Instance.OnPlayerMoveStateEnterAction += this.enterMove;
    Dispatcher.Instance.OnPlayerMoveStateExitAction += this.exitMove;
    Dispatcher.Instance.OnPlayerJumpAction += this.onPlayerJump;
    Dispatcher.Instance.OnPlayerLandAction += this.onPlayerLand;
    Dispatcher.Instance.OnPlayerMeleeSwingAction += this.onPlayerMeleeSwingSound;
    Dispatcher.Instance.OnPlayerMeleeHitAction += this.onPlayerMeleeHit;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnPlayerMoveStateEnterAction -= this.enterMove;
    Dispatcher.Instance.OnPlayerMoveStateExitAction -= this.exitMove;
    Dispatcher.Instance.OnPlayerJumpAction -= this.onPlayerJump;
    Dispatcher.Instance.OnPlayerLandAction -= this.onPlayerLand;
    Dispatcher.Instance.OnPlayerMeleeSwingAction -= this.onPlayerMeleeSwingSound;
    Dispatcher.Instance.OnPlayerMeleeHitAction -= this.onPlayerMeleeHit;
  }

  public void onPlayerJump() {
    audioSource.PlayOneShot(jump, .3f);
  }
  public void onPlayerLand() {
    audioSource.PlayOneShot(land, .7f);
  }
  public void onPlayerMeleeSwingSound() {
    audioSource.PlayOneShot(meleeSwing, 1f);
  }
  public void onPlayerMeleeHit() {
    audioSource.PlayOneShot(meleeHit, .8f);
  }

  private void playRunSfx() {
    timer += Time.deltaTime;
    if (timer > timeBetweenSteps) {
      audioSource.PlayOneShot(RandomClip(), 1f);
      timer = 0;
    }
  }

  private void enterMove() {
    isMoving = true;
    timer = .38f;
  }

  private void exitMove() {
    isMoving = false;
    timer = 0;
  }

  private void Update() {
    if (isMoving) {
      playRunSfx();
    }
  }

  private AudioClip RandomClip() {
    int randNum = UnityEngine.Random.Range(0, footsteps.Length);
    return footsteps[randNum];
  }

}
