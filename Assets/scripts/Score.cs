using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

  [SerializeField] private TextMeshProUGUI score;
  private float currentScore;

  private void Awake() {
    this.currentScore = 0;
  }
  
  private void OnEnable() {
    Dispatcher.Instance.OnScoreUpdateAction += this.AddToScore;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnScoreUpdateAction -= this.AddToScore;
  }

  private void AddToScore(float score) {
    this.currentScore += score;
    this.score.text = "Score: " + this.currentScore;
  }

}
