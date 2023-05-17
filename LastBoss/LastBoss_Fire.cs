using UnityEngine;

public class LastBoss_Fire : MonoBehaviour
{
    [SerializeField] public GameObject _bullet;
    [SerializeField] private Transform[] Firepos1page;
    [SerializeField] private Transform[] Firepos2page;
    [SerializeField] private Transform[] Firepos3page;
    int RandomPos;

    private Transform bossTr;
    private Transform playerTr;
    public bool IsFire = false;
    private float TimePrev;
    public float nextfire;
    public float nextfire2;
    public float nextfire3;
    private float fireRate = 0.1f;
    private readonly float damping = 10f;
    LastBoss_Hp lastboss_HP;

    void Start()
    {
        TimePrev = Time.time;
        bossTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        lastboss_HP = GetComponent<LastBoss_Hp>();
    }

    void Update()
    {
        if (!IsFire || lastboss_HP.LastBossisDie == false)
        {
            if (Time.time >= nextfire)
            {
                page_1();
                nextfire = Time.time + fireRate + Random.Range(0.15f, 0.35f);
            }
        }
    }

    void page_1()
    {
        if (lastboss_HP.LastBossisDie == true) return;

        RandomPos = Random.Range(0, 4);
        GameObject bullet = Instantiate(_bullet,
            Firepos1page[RandomPos].position, Firepos1page[RandomPos].rotation);
    }

    public void page_3()
    {
        if (lastboss_HP.LastBossisDie == true) return;

        if (Time.time >= nextfire3)
        {
            for (int i = 1; i < 6; i++)
            {
                Instantiate(_bullet, Firepos3page[i].position, Firepos3page[i].rotation);
                nextfire3 = Time.time + Random.Range(1.0f, 1.2f);
            }
        }
    }
}
