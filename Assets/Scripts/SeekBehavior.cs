using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekBehavior : Steering
{
    public Transform target;

    public override SteeringData GetSteering(SteeringBehaviorController steeringController)
    {
        SteeringData steering = new SteeringData();
        steering.linear = Vector3.zero;

        if (target == null)
            return new SteeringData();
        if(target.position.x + 5 >= transform.position.x)
        {
            steering.linear = (target.position - transform.position).normalized * steeringController.maxAcceleration;
            steering.angular = 0.0f;
        }

        return steering;
    }
}
