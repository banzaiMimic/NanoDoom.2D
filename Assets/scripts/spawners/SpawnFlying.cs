using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlying : MonoBehaviour {
  
  [SerializeField] private GameObject flyingEnemy;
  [SerializeField] private Player player;

  public float maxTime = 5;
  public float minTime = 2;
  //current time
  private float time;

  //The time to spawn the object
  private float spawnTime;

  void Start(){
    SetRandomTime();
    time = minTime;
  }

  void FixedUpdate(){

    //Counts up
    time += Time.deltaTime;

    //Check if its the right time to spawn the object
    if(time >= spawnTime && player){
      SpawnObject();
      SetRandomTime();
    }

  }

  //Spawns the object and resets the time
  void SpawnObject(){
    Debug.Log("[SpawnFlying] -- spawning enemy flyer");
    time = minTime;
    float spawnY = player.core.Movement.rBody.transform.position.y + Random.Range(0f, 10f);
    float spawnX = player.core.Movement.rBody.transform.position.x + 20;
    Vector3 spawnPoint = new Vector3(spawnX, spawnY, player.core.Movement.rBody.transform.position.z);
    Instantiate (flyingEnemy, spawnPoint , flyingEnemy.transform.rotation);
  }

  //Sets the random time between minTime and maxTime
  void SetRandomTime(){
    spawnTime = Random.Range(minTime, maxTime);
  }

}
