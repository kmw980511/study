using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //적 캐릭터의 상태를 표현 하기 위한 열거형 변수 정의 
    public enum State
    {
        IDLE, TRACE, ATTACK, DIE
    }
    public State state = State.IDLE;

    private Transform playerTr;
    private Transform enemyTr;
    private Animator animator;
    public float attackDist = 8.0f;
    public float traceDist = 10.0f;
    public bool isDie = false;
    private WaitForSeconds ws;
    private EnemyMove enemyMove;
    private EnemyFire enemyFire;

    private readonly int hashMove = Animator.StringToHash("IsWalk");
    private readonly int hashFire = Animator.StringToHash("IsFire");
    private readonly int hashDie = Animator.StringToHash("Die");

    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTr = player.GetComponent<Transform>();

        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        enemyFire = GetComponent<EnemyFire>();
        ws = new WaitForSeconds(0.3f);
    }

    void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());
        Damage.OnPlayerDie += this.OnPlayerDie;
    }

    void OnDisable()
    {
        Damage.OnPlayerDie -= this.OnPlayerDie;
    }

    public void OnPlayerDie()
    {
        enemyMove.Stop();
        enemyFire.isFire = false;
        StopAllCoroutines();
        Time.timeScale = 0.0f;
        animator.SetBool(hashFire, false);
    }

    IEnumerator CheckState()
    {
        yield return new WaitForSeconds(1.0f);

        while (!isDie)
        {
            if (state == State.DIE) yield break;

            float dist = Vector3.Distance(playerTr.position, enemyTr.position);
            if (dist <= attackDist)
                state = State.ATTACK;
            else if (dist <= traceDist)
                state = State.TRACE;
            else
                state = State.IDLE;

            yield return ws;
        }
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;
            switch (state)
            {
                case State.IDLE:
                    enemyFire.isFire = false;
                    break;
                case State.TRACE:
                    enemyFire.isFire = false;
                    enemyMove.traceTarget = playerTr.position;
                    animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:
                    enemyMove.Stop();
                    animator.SetBool(hashMove, false);
                    if (enemyFire.isFire == false)
                        enemyFire.isFire = true;
                    break;
                case State.DIE:
                    Die();
                    break;

            }
        }
    }

    public void Die()
    {
        this.gameObject.tag = "ENEMY";
        isDie = true;
        enemyFire.isFire = false;
        enemyMove.Stop();
        animator.SetTrigger(hashDie);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<Rigidbody>().isKinematic = true;
        StopAllCoroutines();
        GameManager.gameManager.KillCount(1);
    }
}