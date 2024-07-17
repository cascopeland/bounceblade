using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;


public class AIController : MonoBehaviour
{
    public Transform target;
    public float updateSpeed = 0.1f;
    public NavMeshAgent agent;
    private bool playerSeen = false;
    public Transform centrePoint;
    public float range;

    // Audio Variables // 
    AudioManager audioManager;


    private void Start()
    {
        // For audiomanager reference w. entity sounds.
        audioManager = GetComponent<AudioManager>();

        StartCoroutine(FollowTarget());

    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
        while( enabled)
        {
            Vector3 agentPos = agent.transform.position;
            Vector3 targetPos = target.transform.position;
            if (Vector3.Distance(agentPos, targetPos) < 10)
            {
                playerSeen = true;
            }
            if (playerSeen == true)
            {
                agent.SetDestination(target.transform.position);
            } else
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    Vector3 point;
                    if (RoamNpc(centrePoint.position, range, out point))
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                        agent.SetDestination(point);
                    }
                }
            }
            
            yield return wait;

        }
    }

    bool RoamNpc(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }


    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        // only need to do this because its a mouse
    //        if(Physics.Raycast(movePosition, out var hitInfo))
    //        {
    //            agent.SetDestination(hitInfo.point);
    //        }
    //    }
    //}
}
