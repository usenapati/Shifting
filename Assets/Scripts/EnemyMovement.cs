using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyMovement : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject>();
    protected Rigidbody enemy;
    protected NavMeshAgent agent;
    public bool patrol;
    public float waitTime;
    protected Vector3 nextPoint;
    protected bool waiting = false;
    protected bool travelling = false;
    bool prevPatrol = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Rigidbody>();
        nextPoint = Vector3.zero;
        if (!patrol)
            enemy.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        /**if (patrol)
        {
            if (nextPoint == Vector3.zero && !travelling && !waiting)
            {
                travelling = true;
                nextPoint = points[(int)Random.Range(0, points.Count)].transform.position;
                nextPoint.y = transform.position.y;
                agent.SetDestination(nextPoint);
                agent.isStopped = false;
            }
            else if (travelling && agent.remainingDistance < 1.0f)
            {
                travelling = false;
                waiting = true;
                agent.isStopped = true;
                enemy.isKinematic = true;
                StartCoroutine(wait());
            }
        }*/
        
    }
    protected void SetDestination(Vector3 point)
    {
        travelling = true;
        nextPoint = point;
        nextPoint.y = transform.position.y;
        agent.SetDestination(nextPoint);
        agent.isStopped = false;
    }
    protected void ReachDestination()
    {
        travelling = false;
        waiting = true;
        agent.isStopped = true;
        enemy.isKinematic = true;
        StartCoroutine(wait());
    }
    public void StopPatrol()
    {
        prevPatrol = patrol;
        patrol = false;
        agent.isStopped = true;
        enemy.isKinematic = true;
    }
    public void ResumePatrol()
    {
        patrol = prevPatrol;
        agent.isStopped = false;
        enemy.isKinematic = false;
        nextPoint = Vector3.zero;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
        agent.isStopped = false;
        enemy.isKinematic = false;
        nextPoint = Vector3.zero;
        waiting = false;
        Debug.Log("stop wait");
    }

}
