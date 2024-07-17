using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCAnimatorScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public Transform target;
    public float updateSpeed = 0.1f;
    private bool playerSeen = false;
    public Transform centerPoint;
    public float range;




    private void Update()
    {
        Vector3 agentPos = agent.transform.position;
        Vector3 targetPos = target.transform.position;
        //print(targetPos);



        if (agent.hasPath)
        {
            var dir = (agent.steeringTarget - transform.position).normalized;
            var animationDir = transform.InverseTransformDirection(dir);
            var correctDir = Vector3.Dot(dir, transform.forward) > 0.5f;
            animator.SetFloat("velx", correctDir ? animationDir.x : 0, .5f, Time.deltaTime);
            animator.SetFloat("vely", correctDir ? animationDir.z : 0, .5f, Time.deltaTime);


            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir));
            float maxRotSpeed = 90f;
            float step = Mathf.Min(maxRotSpeed * Time.deltaTime, angle);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), 180 * Time.deltaTime); //10000 * Time.deltaTime
            //print("delta time" + Time.deltaTime);

            if (Vector3.Distance(transform.position, agent.destination) < agent.radius)
            {
                agent.ResetPath();
            }
        }
        else
        {
            animator.SetFloat("velx", 0, 0.25f, Time.deltaTime);
            animator.SetFloat("vely", 0, 0.25f, Time.deltaTime);
        }

        if (Vector3.Distance(agentPos, targetPos) < 10)
        {
            playerSeen = true;
            
            
        }
        if (Vector3.Distance(agentPos, targetPos) < 1.15)
        {
            animator.SetBool("isPunching", true);
        }
        else
        {
            animator.SetBool("isPunching", false);
        }
        if (playerSeen == true)
        {
            agent.destination = (target.transform.position);
            if (Vector3.Distance(agentPos, targetPos) > 5 && Vector3.Distance(agentPos, targetPos) < 15)
            {
                GetComponent<SkeletonNPC_Combat>().AttackPlayer(target);
            }
        }
        else
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Vector3 point;
                if (RoamNpc(centerPoint.position, range, out point))
                {
                    agent.destination = (point);
                }
            }
        }

        //yield return wait;
    }


    private void Start()
    {
        //StartCoroutine(FollowTarget());
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

}