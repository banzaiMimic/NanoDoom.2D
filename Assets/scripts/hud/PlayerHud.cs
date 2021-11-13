using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHud : MonoBehaviour {
  
  [SerializeField] private TextMeshProUGUI healthText;

  private void OnEnable() {
    Dispatcher.Instance.OnUpdatePlayerHealthAction += this.UpdateHealth;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnUpdatePlayerHealthAction -= this.UpdateHealth;
  }

  public void UpdateHealth(float currentHealth, float maxHealth) {
    healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
  }

}
