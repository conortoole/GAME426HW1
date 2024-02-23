using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pursuit;
using Flee;

public class EnemyTransition : MonoBehaviour
{
    GameObject room1;
    GameObject room2;
    GameObject player;
    GameObject[] turtles;
    GameObject[] slimes;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        room1 = GameObject.FindWithTag("room1");
        room2 = GameObject.FindWithTag("room2");
        player = GameObject.FindWithTag("Player");
        turtles = GameObject.FindGameObjectsWithTag("turtle");
        slimes = GameObject.FindGameObjectsWithTag("slime");
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if (IsInRoom(room1) || IsInRoom(room2)){
            //pursuit code
            foreach (GameObject turtle in turtles){
                if (gameObject == turtle){
                    gameObject.GetComponent<pursuit>().Pursue();
                }
            }
            foreach (GameObject slime in slimes){
                if (gameObject == slime){
                    gameObject.GetComponent<flee>().RunAway();
                }
            }
            
        }
        else{
            //leave this
            gameObject.transform.position = startPos;
            
            //wander code goes here//////////////////////
            
        }
    }

    bool IsInRoom(GameObject room){
        Collider roomCollider = room.GetComponent<Collider>();
        if (roomCollider.bounds.Contains(player.transform.position) && roomCollider.bounds.Contains(gameObject.transform.position)){
            return true;
        }
        return false;
    }
}
