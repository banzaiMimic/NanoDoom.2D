using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHud : MonoBehaviour {
  
  [SerializeField] private TextMeshProUGUI healthText;
  [SerializeField] private TextMeshProUGUI abilityText;

  private void OnEnable() {
    Dispatcher.Instance.OnUpdatePlayerHealthAction += this.UpdateHealth;
    Dispatcher.Instance.OnUpdatePlayerAbilityChargesAction += this.UpdateAbilityCharges;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnUpdatePlayerHealthAction -= this.UpdateHealth;
    Dispatcher.Instance.OnUpdatePlayerAbilityChargesAction -= this.UpdateAbilityCharges;
  }

  public void UpdateHealth(float currentHealth, float maxHealth) {
    healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
  }

  public void UpdateAbilityCharges(int charges, int max) {
    abilityText.text = charges.ToString() + " / " + max.ToString();
  }

}
