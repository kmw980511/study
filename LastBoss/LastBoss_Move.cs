using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LastBoss_Move : MonoBehaviour
{
    private Transform bossTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private readonly float damping = 10.0f;
    public List<Transform> waypoints;
    public int nextIdx;
    LastBoss_Hp lastboss_HP;

    void Start()
    {
        lastboss_HP = GetComponent<LastBoss_Hp>();
        bossTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        var group = GameObject.Find("LastBossWayPoints");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(waypoints);
            waypoints.RemoveAt(0);
        }

        MoveWayPoint();
    }

    void MoveWayPoint()
    {
        if (lastboss_HP.LastBossisDie == true) return;
        if (agent.isPathStale) return;

        agent.destination = waypoints[nextIdx].position;
        agent.isStopped = false;
    }

    void Update()
    {
        Quaternion rot = Quaternion.LookRotation(playerTr.position - bossTr.position);
        bossTr.rotation = Quaternion.Slerp(bossTr.rotation, rot, Time.deltaTime * damping);

        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            nextIdx = ++nextIdx % waypoints.Count;
            MoveWayPoint();
        }
    }
}
