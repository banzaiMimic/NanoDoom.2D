using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerRespawn : MonoBehaviour {
  
  [SerializeField] private GameObject player;
  [SerializeField] private CinemachineVirtualCamera cinemachine;
  private bool isRespawning = false;
  private float respawnStartedAt = 0f;
  private float respawnAfter = 5f;

  private void OnEnable() {
    Dispatcher.Instance.OnPlayerDeathAction += this.StartRespawn;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnPlayerDeathAction -= this.StartRespawn;
  }

  private void StartRespawn() {
    isRespawning = true;
    respawnStartedAt = Time.time;
  }

  private void Update() {
    if (isRespawning) {
      if ((Time.time - respawnStartedAt) >= respawnAfter) {
        respawnStartedAt = Time.time;
        Debug.Log("[SpawnPlayer!]");
        SpawnPlayer();
        isRespawning = false;
      }
    }
  }

  private void SpawnPlayer() {
    Debug.Log("[Spawning player--]");
    GameObject spawn = Instantiate (this.player, this.transform);
    this.cinemachine.Follow = spawn.transform;
    Player playerUpdate = spawn.GetComponent<Player>();
    playerUpdate.name = "Player";
    Dispatcher.Instance.OnPlayerRespawn(playerUpdate);
  }

}
