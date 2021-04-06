using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if ( (other.gameObject.transform.name).ToString()  == "onClickHitBox" || (this.transform.name).ToString() == "onClickHitBox" ) {
            return;
        }
        if ( int.Parse(other.gameObject.transform.name) > int.Parse(this.transform.name) ) {
            Destroy(other.gameObject);
            print("Destroyed");
            
            Rigidbody rb = this.GetComponent<Rigidbody>();
            Rigidbody rb2 = other.gameObject.GetComponent<Rigidbody>();
            Vector3 rbMomentum = rb.mass * rb.velocity; 
            Vector3 rb2Momentum = rb2.mass * rb2.velocity;
            Vector3 resultantMomentum = rbMomentum + rb2Momentum;
			float resultantMass = rb.mass + rb2.mass;
            
            Vector3 resultantVelocity = resultantMomentum / resultantMass;  

            rb.AddForce(resultantVelocity);

            this.transform.localScale += other.gameObject.transform.localScale;
            return;
        }
    }
}
