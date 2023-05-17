using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastBoss_Hp : MonoBehaviour
{
    private readonly string bulletTag = "BULLET";
    [SerializeField] public Image hpbar;
    private readonly float InitHp = 500f;
    public float curHp;
    public bool LastBossisDie = false;
    private bool isLastBossGone = false;
    [SerializeField] private GameObject FlameThrower2page;
    [SerializeField] private GameObject FlameThrower3page;
    [SerializeField] private GameObject SmallExprosion;
    [SerializeField] private GameObject Boss;
    LastBoss_Fire lastboss_Fire;

    void Start()
    {
        hpbar.color = Color.green;
        curHp = InitHp;
        lastboss_Fire = GetComponent<LastBoss_Fire>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == bulletTag)
        {
            Destroy(col.gameObject);
            curHp -= 1.0f;
        }

        curHp = Mathf.Clamp(curHp, 0f, 500f);
        hpbar.fillAmount = curHp / InitHp;
    }

    public void Update()
    {
        if (hpbar.fillAmount <= 0.0f)
        {
            LastBossisDie = true;
            GetComponent<LastBoss_Fire>().IsFire = false;
            hpbar.GetComponent<Image>().color = Color.clear;
            SmallExprosion.SetActive(true);
            Invoke("LastBossGone", 10);
        }
        else if (hpbar.fillAmount <= 0.4f)
        {
            hpbar.color = Color.red;
            FlameThrower3page.SetActive(true);
            lastboss_Fire.page_3();
        }
        else if (hpbar.fillAmount <= 0.6f)
        {
            hpbar.color = Color.yellow;
            FlameThrower2page.SetActive(true);
        }
    }

    void LastBossGone()
    {
        if (isLastBossGone) return;

        isLastBossGone = true;
        Boss.SetActive(false);
        GameManager.gameManager.KillCount(2000);
        Invoke("LoadEndScene", 10f);
    }

    void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
