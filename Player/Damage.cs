using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private const string eBulletTag = "E_BULLET";
    private const string enemyTag = "ENEMY";
    private int InitHp = 3;
    private int CurHp;
    private Image hpIcon1, hpIcon2, hpIcon3, gameOverImg;
    public bool playerdie = false;
    FireCtrl fireCtrl;
    GameManager gameManager;

    void Start()
    {
        hpIcon1 = GameObject.Find("Panel-HpCount").transform.GetChild(0).GetChild(0).GetComponent<Image>();
        hpIcon2 = GameObject.Find("Panel-HpCount").transform.GetChild(1).GetChild(0).GetComponent<Image>();
        hpIcon3 = GameObject.Find("Panel-HpCount").transform.GetChild(2).GetChild(0).GetComponent<Image>();
        gameOverImg = GameObject.Find("Canvas-UI").transform.GetChild(2).GetComponent<Image>();
        CurHp = InitHp;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == eBulletTag)
        {
            Destroy(col.gameObject);
            CurHp -= 1;
            CurHp = Mathf.Clamp(CurHp, 0, InitHp);

            if(CurHp == 2)
            {
                hpIcon3.gameObject.SetActive(false);
            }            
            if(CurHp == 1)
            {
                hpIcon2.gameObject.SetActive(false);
            }
            if(CurHp <= 0)
            {
                hpIcon1.gameObject.SetActive(false);
                gameOverImg.gameObject.SetActive(true);
                Time.timeScale = 0;
                PlayerDie();
            }
        }
    }

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    public void PlayerDie()
    {
        playerdie = true;
        OnPlayerDie();
        if(Time.timeScale == 0)
        {
            Cursor.visible = true;
        }
    }
}
