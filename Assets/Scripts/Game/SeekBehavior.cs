using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekBehavior : Steering
{
    public Transform player;
    [SerializeField] float followDistance = 5;
    [SerializeField] float attackDistance = 2;

    public Transform[] _patrolPoints;
    public int _currentPoint = 0;
    public override SteeringData GetSteering(SteeringBehaviorController steeringController)
    {
        SteeringData steering = new SteeringData();
        steering.linear = Vector3.zero;

        if (player == null)
            return steering;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= followDistance && distanceToPlayer > attackDistance)
        {
            steering.linear = (player.position - transform.position).normalized * steeringController.maxAcceleration;
            steering.angular = 0.0f;
        }
        else if (distanceToPlayer <= attackDistance)
            steering.linear = Vector3.zero;
        else
        {
            Vector3 goToPatrolPoint = _patrolPoints[_currentPoint].position - transform.position;
            steering.linear = goToPatrolPoint.normalized * steeringController.maxAcceleration;
            steering.angular = 0.0f;

            if(goToPatrolPoint.magnitude < 1f)
                _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;
                if (_currentPoint > _patrolPoints.Length)
                    _currentPoint = 0;
        }

        return steering;
    }
}
