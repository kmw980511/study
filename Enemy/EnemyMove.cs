using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private readonly float traceSpeed = 4.0f;
    private NavMeshAgent agent;
    private Transform enemyTr;
    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            TraceTarget(_traceTarget);
        }
    }
    public float speed
    {
        get { return agent.velocity.magnitude; }
    }

    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;
        agent.destination = pos;
        agent.isStopped = false;
    }

    void Start()
    {
        enemyTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.updateRotation = false;
    }

    void Update()
    {
        if (GetComponent<EnemyAI>().isDie == true) return;
        if (agent.isStopped == false)
        {   
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot,
                                             Time.deltaTime);
        }
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
