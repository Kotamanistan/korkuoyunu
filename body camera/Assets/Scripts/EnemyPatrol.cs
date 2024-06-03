using UnityEngine;
using System.Collections.Generic;

public class EnemyPatrol : MonoBehaviour
{
    public List<Transform> waypoints; // List of waypoints for the enemy to patrol
    public float speed = 2.0f; // Enemy movement speed
    public float waitTime = 1.0f; // Time to wait at each waypoint
    public float rotationSpeed = 5.0f; // Speed at which the enemy rotates to face the next waypoint
    public Transform exitPoint; // Exit point for the enemy

    private int currentWaypointIndex = 0;
    private float waitTimer;
    private Animator animator; // Animator component
    private bool isWalking = false;

    void Start()
    {
        if (waypoints.Count > 0)
        {
            transform.position = waypoints[0].position;
            waitTimer = waitTime;
        }
        else
        {
            Debug.LogError("No waypoints set for enemy patrol.");
        }

        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        if (waypoints.Count == 0)
        {
            return;
        }

        Patrol();

        // Update the Animator parameter
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
        }
    }

    void Patrol()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float step = speed * Time.deltaTime;

        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        // Rotate to face the waypoint
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        // Check if the enemy has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            if (waitTimer <= 0)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                waitTimer = waitTime;

                // Check if the enemy has reached the exit point
                if (waypoints[currentWaypointIndex] == exitPoint)
                {
                    ExitReached();
                }
            }
            else
            {
                waitTimer -= Time.deltaTime;
            }

            isWalking = false; // Set isWalking to false when waiting
        }
        else
        {
            isWalking = true; // Set isWalking to true when moving
        }
    }

    void ExitReached()
    {
        // Add your exit logic here if needed
        Debug.Log("Exit point reached. Performing exit actions.");
        // Example: Do not destroy the enemy, just log the message
    }
}


