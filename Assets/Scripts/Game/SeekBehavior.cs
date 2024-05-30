using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekBehavior : Steering
{
    public Transform player;
    [SerializeField] float followDistance = 5;
    [SerializeField] float attackDistance = 2;

    public override SteeringData GetSteering(SteeringBehaviorController steeringController)
    {
        SteeringData steering = new SteeringData();
        steering.linear = Vector3.zero;

        if (player == null)
            return new SteeringData();
        if ((player.transform.position.x >= transform.position.x - followDistance && player.transform.position.x <= transform.position.x - attackDistance) ||
            (player.transform.position.x <= transform.position.x + followDistance && player.transform.position.x >= transform.position.x + attackDistance))
        {
            steering.linear = (player.position - transform.position).normalized * steeringController.maxAcceleration;
            steering.angular = 0.0f;
        }

        return steering;
    }
}
