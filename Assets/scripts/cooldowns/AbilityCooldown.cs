using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// this only holds cooldown for dash, if we add abilities later we need 
// to update this to handle multiple abilities (i.e. player.unlockedAbilities)
public class AbilityCooldown : MonoBehaviour {
  
  [SerializeField] private Image imageCooldown;
  [SerializeField] private TMP_Text textCooldown;

  private bool isCooldown = false;
  private float cooldownTime = 10.0f;
  private float cooldownTimer = 0.0f;

  private void OnEnable() {
    Dispatcher.Instance.OnPlayerAbilityAction += this.handlePlayerAbility;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnPlayerAbilityAction -= this.handlePlayerAbility;
  }

  private void handlePlayerAbility(AbilityType abilityType, float cdTime) {
    cooldownTime = cdTime;
    this.UseAbility();
  } 

  void Start() {
    textCooldown.gameObject.SetActive(false);
    imageCooldown.fillAmount = 0.0f;
  }

  void Update() {
    if (isCooldown) {
      ApplyCooldown();
    }
  }

  void ApplyCooldown() {
    cooldownTimer -= Time.deltaTime;

    if (cooldownTimer < 0.0f) {
      isCooldown = false;
      textCooldown.gameObject.SetActive(false);
      imageCooldown.fillAmount = 0.0f;
    } else {
      textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
      imageCooldown.fillAmount = cooldownTimer / cooldownTime;
    }
  }

  public void UseAbility() {
    if (isCooldown) {

    } else {
      isCooldown = true;
      textCooldown.gameObject.SetActive(true);
      cooldownTimer = cooldownTime;
    }
  }

}
