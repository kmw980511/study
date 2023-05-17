using UnityEngine;
using UnityEngine.UI;

public class GunFire : MonoBehaviour
{
    [SerializeField] public GameObject _bullet;
    [SerializeField] private Transform firepos;
    [SerializeField] GameObject firepos1;
    [SerializeField] public Image hpbar;
    private Transform playerTr;
    private Transform gunTr;
    public bool IsFire = false;
    private float TimePrev;
    public float nextfire;
    private readonly float damping = 10f;
    LastBoss_Hp lastboss_HP;

    void Start()
    {
        TimePrev = Time.time;
        gunTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        lastboss_HP = GetComponent<LastBoss_Hp>();
    }

    void Update()
    {
        if (hpbar.fillAmount <= 0.0f)
        {
            IsFire = false;
        }
        else if (hpbar.fillAmount <= 0.6f)
        {
            if (Time.time >= nextfire)
            {
                Instantiate(_bullet, firepos.position, firepos.rotation);
                nextfire = Time.time + Random.Range(0.25f, 0.45f);
            }
        }
    }
}