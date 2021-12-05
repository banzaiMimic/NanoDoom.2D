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
    Dispatcher.Instance.OnPlayerRespawnAction += this.PlayerRespawn;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnUpdatePlayerHealthAction -= this.UpdateHealth;
    Dispatcher.Instance.OnUpdatePlayerAbilityChargesAction -= this.UpdateAbilityCharges;
    Dispatcher.Instance.OnPlayerRespawnAction -= this.PlayerRespawn;
  }

  private void PlayerRespawn(Player player) {
    this.UpdateHealth(100f, 100f);
  }

  public void UpdateHealth(float currentHealth, float maxHealth) {
    healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
  }

  public void UpdateAbilityCharges(int charges, int max) {
    abilityText.text = charges.ToString() + " / " + max.ToString();
  }

}
