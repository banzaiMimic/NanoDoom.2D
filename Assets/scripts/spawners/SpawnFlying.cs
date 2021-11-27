using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlying : MonoBehaviour {
  
  [SerializeField] private GameObject flyingEnemy;
  [SerializeField] private Player player;

  public float maxTime = 6;
  public float minTime = 3;
  private float time;
  private float spawnTime;

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
    float spawnY = player.core.Movement.rBody.transform.position.y + Random.Range(0f, 10f);
    float spawnX = player.core.Movement.rBody.transform.position.x + 20;
    Vector3 spawnPoint = new Vector3(spawnX, spawnY, player.core.Movement.rBody.transform.position.z);
    Instantiate (flyingEnemy, spawnPoint , flyingEnemy.transform.rotation);
  }

  void SetRandomTime(){
    spawnTime = Random.Range(minTime, maxTime);
  }

}
