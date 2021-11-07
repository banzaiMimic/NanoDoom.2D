using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class Collectible : MonoBehaviour {

  private void OnEnable() {
    Dispatcher.Instance.OnPickupAction += this.handlePickup;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnPickupAction -= this.handlePickup;
  }

  void OnTriggerEnter2D(Collider2D other) {
    var player = other.GetComponent<Player>();
    if (player) {
      Debug.Log("sending dispatcher onPickup from Collectible ->");
      Dispatcher.Instance.OnPickup(this);
    }
  }

  private void handlePickup(Collectible collectible) {
    Destroy(collectible.gameObject);
  }

}
