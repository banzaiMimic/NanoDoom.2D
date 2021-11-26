using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagesPlayer : MonoBehaviour {

  private Collider2D collider;

  private void Awake() {
    collider = GetComponent<Collider2D>();
  }

  void OnTriggerEnter2D(Collider2D collision) {
    var player = collision.GetComponent<Player>();
    if (player) {
      if (collision.transform.position.x < collider.transform.position.x) {
        // player on left
        Dispatcher.Instance.OnTriggerPlayerHit(10f, -1);
      } else {
        // player on right
        Dispatcher.Instance.OnTriggerPlayerHit(10f, -1);
      }
    }
  }

  private void handlePickup(Collectible collectible) {
    //Destroy(collectible.gameObject);
  }

}