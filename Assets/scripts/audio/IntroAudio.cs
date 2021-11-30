using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudio : MonoBehaviour {
  
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip bgAmbient;

  private void Start() {
    audioSource.clip = bgAmbient;
    audioSource.loop = true;
    audioSource.volume = .6f;
    audioSource.Play();
  }

}
