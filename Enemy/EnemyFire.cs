using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private Animator animator;
    private Transform playerTr;
    private Transform enemyTr;
    private readonly int hashFire = Animator.StringToHash("IsFire");
    EnemyAI enemyai;
    private float nextFire = 0f;
    private float fireRate = 0.1f;
    private readonly float damping = 10f;
    public bool isFire = false;
    public GameObject Bullet;
    GameObject FIREPOS;
    public Transform firePos;
    Damage damage;

    void Start()
    {
        damage = GetComponent<Damage>();
        animator = GetComponent<Animator>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTr = GetComponent<Transform>();
        enemyai = GetComponent<EnemyAI>();
        FIREPOS = transform.GetChild(5).GetChild(1).GetChild(0).gameObject;
    }

    void Update()
    {
        if (enemyai.isDie) return;

        if (!isFire)
        {
            if (Time.time >= nextFire && !enemyai.isDie)
            {
                Fire();
                nextFire = Time.time + fireRate + Random.Range(0.1f, 0.5f);
            }
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }

        if (enemyai.isDie)
        {
            animator.SetBool(hashFire, false);
            FIREPOS.SetActive(false);
        }
    }

    void Fire()
    {
        animator.SetBool(hashFire, true);
        GameObject _bullet = Instantiate(Bullet, firePos.position, firePos.rotation);
        Destroy(_bullet, 5.0f);
    }
}