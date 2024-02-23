using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic{
    public Vector3 position = new Vector3();
    public Vector3 velocity = new Vector3();
    public float orientation = 0f;
    public float rotation = 0f;
}

public class steeringOutput{
    public Vector3 linear = new Vector3();
    public float angular = 0f;

}

namespace Pursuit
{
    public class pursuit : MonoBehaviour
    {
        public float moveSpeed = 4f;  
        public float maxAcc = 2f;
        public Rigidbody rb;
        public GameObject target;
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
        public void Pursue()
        {
            steering = getSteering(steering); 
            Kin.velocity = steering.linear;
            Kin.position = Kin.position + (Kin.velocity * Time.deltaTime);
            
            rb.velocity = new Vector3(Kin.velocity.x, 0f, Kin.velocity.z) * moveSpeed;
            gameObject.transform.LookAt(target.transform.position);  
            
            if(Kin.velocity.magnitude > maxAcc){
                Kin.velocity.Normalize();
                Kin.velocity = Kin.velocity * maxAcc;
            }

        }

        steeringOutput getSteering(steeringOutput steer){ 
            steer.linear = target.transform.position - gameObject.transform.position;
            steer.linear.Normalize();
            steer.linear = steer.linear * 2.0f;

            return steer;
        }
    }
}