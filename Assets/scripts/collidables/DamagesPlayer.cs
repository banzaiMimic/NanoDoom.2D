using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagesPlayer : MonoBehaviour {

  private Collider2D col;

  private void Awake() {
    col = GetComponent<Collider2D>();
  }

  void OnTriggerEnter2D(Collider2D collision) {
    var player = collision.GetComponent<Player>();
    if (player && player.stateMachine.currentState != player.dashState) {
      if (collision.transform.position.x < col.transform.position.x) {
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
