using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 1.0f;
    private int currentWaypointIndex = 0;
    

    // Update is called once per frame
    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the GameObject has reached the current waypoint
            if (transform.position == targetPosition)
            {
                currentWaypointIndex++; // Move to the next waypoint
            }
        }else
        {
            currentWaypointIndex = 0; // Go back to the first waypoint
        }
    }
}
