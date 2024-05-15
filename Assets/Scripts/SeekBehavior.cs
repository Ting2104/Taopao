using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekBehavior : Steering
{
    public Transform player;
    float distance = 5;

    public override SteeringData GetSteering(SteeringBehaviorController steeringController)
    {
        SteeringData steering = new SteeringData();
        steering.linear = Vector3.zero;

        if (player == null)
            return new SteeringData();
        if (player.transform.position.x >= transform.position.x - distance && player.transform.position.x <= transform.position.x - 2)
        {
            steering.linear = (player.position - transform.position).normalized * steeringController.maxAcceleration;
            steering.angular = 0.0f;
        }
        else if (player.transform.position.x <= transform.position.x + distance && player.transform.position.x >= transform.position.x + 2)
        {
            steering.linear = (player.position - transform.position).normalized * steeringController.maxAcceleration;
            steering.angular = 0.0f;
        }

        return steering;
    }
}
