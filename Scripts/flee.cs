using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flee
{
    public class flee : MonoBehaviour
    {
        public float moveSpeed = 4f;  
        public float maxAcc = 2f;
        public Rigidbody rb;
        GameObject target;
        Kinematic Kin;
        Kinematic beginningPos;
        steeringOutput steering;
        // GameObject room1;
        // GameObject room2;
        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindWithTag("Player");
            Kin = new Kinematic();
            beginningPos = new Kinematic();
            steering = new steeringOutput();
            rb = GetComponent<Rigidbody>();
            Kin.position = transform.position; //setting kin to intial position of enemy
            beginningPos.position = transform.position;
            // room1 = GameObject.FindWithTag("room1");
            // room2 = GameObject.FindWithTag("room2");
        }

        // Update is called once per frame
        public void RunAway()
        {
            steering = getSteering(steering);             
            Kin.velocity = steering.linear;       
            Kin.position = Kin.position + (Kin.velocity * Time.deltaTime);
        
            rb.velocity = new Vector3(Kin.velocity.x, 0f, Kin.velocity.z) * moveSpeed;
            gameObject.transform.LookAt(target.transform.position * -1);

            if(Kin.velocity.magnitude > maxAcc){
                Kin.velocity.Normalize();
                Kin.velocity = Kin.velocity * maxAcc;
            }
        }

        steeringOutput getSteering(steeringOutput steer){ 
            steer.linear = gameObject.transform.position - target.transform.position;
            steer.linear.Normalize();
            steer.linear = steer.linear * 2.0f;

            return steer;
        }
    }
}
