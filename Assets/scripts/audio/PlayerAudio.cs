using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {
  
  public AudioSource audioSource;
  public AudioClip[] footsteps;
  public float timeBetweenSteps = 0.39f;

  private float timer;
  private bool isMoving = false;
  
  private void OnEnable() {
    Dispatcher.Instance.OnPlayerMoveStateEnterAction += this.enterMove;
    Dispatcher.Instance.OnPlayerMoveStateExitAction += this.exitMove;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnPlayerMoveStateEnterAction -= this.enterMove;
    Dispatcher.Instance.OnPlayerMoveStateExitAction -= this.exitMove;
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

  private void playRunSfx() {
    timer += Time.deltaTime;
    if (timer > timeBetweenSteps) {
      audioSource.PlayOneShot(RandomClip());
      timer = 0;
    }
  }

  private AudioClip RandomClip() {
    int randNum = UnityEngine.Random.Range(0, footsteps.Length);
    Debug.Log("randNum: " + randNum);
    return footsteps[randNum];
  }

}
