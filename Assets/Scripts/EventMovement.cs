using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMovement : EnemyMovement
{
    public List<GameObject> orderedPoints = new List<GameObject>();
    int i = 0;
   public void BeginPatrol()
    {
        patrol = true;
        SetDestination(orderedPoints[i].transform.position);
        i++;
    }
    private void Update()
    {
        if (patrol)
        {
            if (nextPoint == Vector3.zero && !travelling && !waiting)
            {
                if (i < orderedPoints.Count)
                {
                    SetDestination(orderedPoints[i].transform.position);
                    i++;
                } else
                {
                    patrol = false;
                }
            }
            else if (travelling && agent.remainingDistance < 1.0f)
            {
                ReachDestination();
            }
        }
    }
}
