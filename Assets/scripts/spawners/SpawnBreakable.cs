using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBreakable : MonoBehaviour {
  
  [SerializeField] private GameObject breakable;
  [SerializeField] private Player player;
  private List<GameObject> spawned = new List<GameObject>();

  public float maxTime = 6;
  public float minTime = 3;
  private float time;
  private float spawnTime;

  private void OnEnable() {
    Dispatcher.Instance.OnPlayerDeathAction += this.clearSpawns;
    Dispatcher.Instance.OnPlayerRespawnAction += this.updatePlayer;
  }

  private void OnDisable() {
    Dispatcher.Instance.OnPlayerDeathAction -= this.clearSpawns;
    Dispatcher.Instance.OnPlayerRespawnAction -= this.updatePlayer;
  }

  private void updatePlayer(Player playerUpdate) {
    this.player = playerUpdate;
  }

  private void clearSpawns() {
    this.spawned.ForEach(go => {
      Destroy(go);
    });
  }

  void Start(){
    SetRandomTime();
    time = minTime;
  }

  void FixedUpdate(){
    time += Time.deltaTime;
    //Check if its the right time to spawn the object
    if(time >= spawnTime && player){
      SpawnObject();
      SetRandomTime();
    }
  }

  void SpawnObject(){
    time = minTime;
    float spawnY = player.core.Movement.rBody.transform.position.y + Random.Range(0f, 8f);
    float spawnX = player.core.Movement.rBody.transform.position.x + 20;
    Vector3 spawnPoint = new Vector3(spawnX, spawnY, player.core.Movement.rBody.transform.position.z);
    GameObject go1 = Instantiate (breakable, spawnPoint , breakable.transform.rotation);
    spawned.Add(go1);

    float spawnYy = player.core.Movement.rBody.transform.position.y + Random.Range(0f, 8f);
    float spawnXx = player.core.Movement.rBody.transform.position.x - 20;
    Vector3 spawnPoint2 = new Vector3(spawnXx, spawnYy, player.core.Movement.rBody.transform.position.z);
    GameObject go2 = Instantiate (breakable, spawnPoint2 , breakable.transform.rotation);
    spawned.Add(go2);
  }

  void SetRandomTime(){
    spawnTime = Random.Range(minTime, maxTime);
  }

}
