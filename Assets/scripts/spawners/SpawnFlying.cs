using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlying : MonoBehaviour {
  
  [SerializeField] private GameObject flyingEnemy;

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
    if(time >= spawnTime){
      SpawnObject();
      SetRandomTime();
    }

  }


  //Spawns the object and resets the time
  void SpawnObject(){
    Debug.Log("[SpawnFlying] -- spawning enemy flyer");
    time = minTime;
    Instantiate (flyingEnemy, transform.position, flyingEnemy.transform.rotation);
  }

  //Sets the random time between minTime and maxTime
  void SetRandomTime(){
    spawnTime = Random.Range(minTime, maxTime);
  }

}
