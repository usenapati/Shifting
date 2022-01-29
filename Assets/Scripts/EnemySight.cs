using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySight : MonoBehaviour
{
    EnemyMovement movement;
    public LayerMask playerMask;
    public List<GameObject> restricteds = new List<GameObject>();
    [Range(0, 360)]
    public float viewAngle;
    public bool spotted;
    public Vector3 lastPos = Vector3.zero;
    public Slider sight;
    public float sightTime = 2.5f;
    float viewAdjust;
    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        viewAdjust = GetComponent<CapsuleCollider>().height / 4;
        sight.maxValue = sightTime;
    }
    private void FixedUpdate()
    {
        if (spotted)
        {
            transform.LookAt(lastPos);
            sight.value += Time.deltaTime;
            if (sight.value == sight.maxValue)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((playerMask | (1 << other.gameObject.layer)) == playerMask)
        {
            Vector3 pos = other.transform.position;
            Vector3 currentPos = transform.position+new Vector3(0,viewAdjust,0);
            Vector3 dirToTarget = (pos - currentPos).normalized;
            float yDir = dirToTarget.y;
            dirToTarget.y = 0;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle)
            {
                if (CheckInRoom(pos))
                {
                    dirToTarget.y = yDir;
                    float disToTarget = Vector3.Distance(currentPos, pos);
                    if (Physics.Raycast(new Vector3(currentPos.x, currentPos.y + viewAdjust, currentPos.z), dirToTarget, disToTarget, playerMask))
                    {
                        sight.gameObject.SetActive(true);
                        spotted = true;
                        movement.StopPatrol();
                        lastPos = pos;
                        lastPos.y = transform.position.y;
                    }
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if ((playerMask | (1 << other.gameObject.layer)) == playerMask)
        {
            Vector3 pos = other.gameObject.transform.position;
            Vector3 currentPos = transform.position+new Vector3(0,viewAdjust,0);
            Vector3 dirToTarget = (pos - currentPos).normalized;
            float yDir = dirToTarget.y;
            dirToTarget.y = 0;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle)
            {
                if (CheckInRoom(pos))
                {
                    dirToTarget.y = yDir;
                    float disToTarget = Vector3.Distance(currentPos, pos);
                    if (Physics.Raycast(currentPos, dirToTarget, disToTarget, playerMask))
                    {
                        sight.gameObject.SetActive(true);
                        spotted = true;
                        movement.StopPatrol();
                        lastPos = pos;
                        lastPos.y = transform.position.y;
                    }
                    else
                    {
                        Dissapeared();
                    }
                }
                else
                {
                    Dissapeared();
                }
            }
            else
            {
                Dissapeared();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Dissapeared();
    }
    private void Dissapeared()
    {
        sight.value = 0;
        sight.gameObject.SetActive(false);
        spotted = false;
        movement.ResumePatrol();
    }
    private bool CheckInRoom(Vector3 playerPos)
    {
        foreach (GameObject restricted in restricteds)
        {
            if (restricted.GetComponent<BoxCollider>().bounds.Contains(playerPos))
            {
                return true;
            }
        }
        return false;
    }
}
