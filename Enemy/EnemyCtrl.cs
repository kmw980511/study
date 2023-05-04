using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    private readonly string bulletTag = "BULLET";
    private readonly int hashFire = Animator.StringToHash("IsFire");
    private float curhp;
    private float InitHp = 1.0f;
    private Animator animator;
    [SerializeField] private GameObject enemy;

    private void Start()
    {
        animator = GetComponent<Animator>();
        curhp = InitHp;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == bulletTag)
        {
            animator.SetBool(hashFire, false);
            curhp -= col.gameObject.GetComponent<BulletCtrl>().damage;

            if (curhp <= 0)
            {
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
                Invoke("Enemygone", 5f);
            }
        }
    }

    void Enemygone()
    {
        Destroy(this.gameObject);
    }
}
