using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMelee : MonoBehaviour {

  [SerializeField] private GameObject meleeEnemy;
  [SerializeField] private Player player;

  public float maxTime = 6;
  public float minTime = 3;
  private float time;
  private float spawnTime;

  private float minSpeedY = 1f;
  private float maxSpeedY = 6f;
  private float minSpeedX = 3f;
  private float maxSpeedX = 6f;

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
    GameObject go1 = Instantiate (meleeEnemy, spawnPoint , meleeEnemy.transform.rotation);

    float spawnYy = player.core.Movement.rBody.transform.position.y + Random.Range(0f, 8f);
    float spawnXx = player.core.Movement.rBody.transform.position.x - 20;
    Vector3 spawnPoint2 = new Vector3(spawnXx, spawnYy, player.core.Movement.rBody.transform.position.z);
    GameObject go2 = Instantiate (meleeEnemy, spawnPoint2 , meleeEnemy.transform.rotation);
    Enemy1 ee = go2.GetComponent<Enemy1>();
    Movement mv2 = ee.core.Movement;
    mv2.Flip();

  }

  void SetRandomTime(){
    spawnTime = Random.Range(minTime, maxTime);
  }
}
