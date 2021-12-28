using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlying : MonoBehaviour {
  
  [SerializeField] private GameObject flyingEnemy;
  [SerializeField] private Player player;
  private List<GameObject> spawned = new List<GameObject>();

  public float maxTime = 6;
  public float minTime = 3;
  private float time;
  private float spawnTime;

  private float minSpeedY = 1f;
  private float minSpeedX = 3f;
  private float maxSpeedX = 6f;

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
    GameObject go1 = Instantiate (flyingEnemy, spawnPoint , flyingEnemy.transform.rotation);
    spawned.Add(go1);
    EnemyFlying eFlying1 = go1.GetComponent<EnemyFlying>();
    Movement mv1 = eFlying1.core.Movement;

    mv1.baseSpeedMultiplier = Random.Range(minSpeedX, maxSpeedX) * .1f;
    //mv1.SetVelocityY(Random.Range(minSpeedY, minSpeedY));
    //mv1.SetVelocityX(Random.Range(minSpeedX, maxSpeedX));


    float spawnYy = player.core.Movement.rBody.transform.position.y + Random.Range(0f, 8f);
    float spawnXx = player.core.Movement.rBody.transform.position.x - 20;
    Vector3 spawnPoint2 = new Vector3(spawnXx, spawnYy, player.core.Movement.rBody.transform.position.z);
    GameObject go2 = Instantiate (flyingEnemy, spawnPoint2 , flyingEnemy.transform.rotation);
    spawned.Add(go2);
    EnemyFlying eFlying2 = go2.GetComponent<EnemyFlying>();
    Movement mv2 = eFlying2.core.Movement;

    mv2.Flip();

    mv2.SetVelocityY(Random.Range(minSpeedY, minSpeedY));
    mv2.SetVelocityX(Random.Range(minSpeedX, maxSpeedX));
  }

  void SetRandomTime(){
    spawnTime = Random.Range(minTime, maxTime);
  }

}
