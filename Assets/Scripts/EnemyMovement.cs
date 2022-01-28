using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject>();
    NavMeshAgent agent;
    public bool patrol;
    public float waitTime;
    Vector3 nextPoint = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol && nextPoint == Vector3.zero)
        {
            nextPoint = points[(int)Random.Range(0, points.Count)].transform.position;
            nextPoint.y = transform.position.y;
            agent.SetDestination(nextPoint);
            agent.isStopped = false;
        }
        else if (patrol && agent.remainingDistance < 1)
        {
            agent.isStopped = true;
            wait();
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
        nextPoint = Vector3.zero;
    }

}
