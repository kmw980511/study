using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireCtrl : MonoBehaviour
{
    public enum fireLv { LV1, LV2, LV3 }
    public fireLv fireLvState = fireLv.LV1;
    private GameObject _bullet;
    private Transform[] firePos;
    Damage damage;
    private const string bossTag = "BOSS";
    private bool isFire = false;
    private float timePrev;
    private AudioSource source;
    private AudioClip fireSfx;
    private Text scoreTxt;
    public int fireLvNum;

    void Start()
    {
        source = GetComponent<AudioSource>();
        fireSfx = Resources.Load("Sounds/FireSfx") as AudioClip;
        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        firePos = GameObject.Find("FirePoints").GetComponentsInChildren<Transform>();
        scoreTxt = GameObject.Find("Text-Lv").GetComponent<Text>();
        timePrev = Time.time;
        fireLvState = fireLv.LV1;
        LoadFireLvData();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButton(0))
        {
            if (Time.time - timePrev >0.1f)
            {                
                isFire = true;
                Fire();
                timePrev = Time.time;
            }            
        }
        else
        {
            isFire = false;
        }

        FireLevelUp();
    }

    void FireLevelUp()
    {
        if (GameManager.gameManager.KillCnt >= 1000)
        {
            fireLvState = fireLv.LV3;
        }
        else if (GameManager.gameManager.KillCnt >= 10 && GameManager.gameManager.KillCnt < 1000)
        {
            fireLvState = fireLv.LV2;
        }
    }

    void Fire()
    {
        switch(fireLvState)
        {
            case fireLv.LV1:
                fireLvNum = 1;
                FireLvOne();
                PlayerPrefs.SetInt("FireLevel", fireLvNum);
                break;
            case fireLv.LV2:
                fireLvNum = 2;
                FireLvTwo();
                PlayerPrefs.SetInt("FireLevel", fireLvNum);
                scoreTxt.text = "LV2";
                break;
            case fireLv.LV3:
                fireLvNum = 3;
                FireLvThree();
                PlayerPrefs.SetInt("FireLevel", fireLvNum);
                scoreTxt.text = "LV3";
                break;
        }
    }

    void FireLvOne()
    {
        firePos[0].gameObject.SetActive(true);
        Instantiate(_bullet, firePos[1].position, firePos[1].rotation);
        firePos[0].gameObject.SetActive(false);
    }

    void FireLvTwo()
    {
        for(int i=1; i<4; i++)
        {
            firePos[i].gameObject.SetActive(true);
            Instantiate(_bullet, firePos[i].position, firePos[i].rotation);
            firePos[i].gameObject.SetActive(false);
        }
    }

    void FireLvThree()
    {
        for (int i = 1; i < 6; i++)
        {
            firePos[i].gameObject.SetActive(true);
            Instantiate(_bullet, firePos[i].position, firePos[i].rotation);
            firePos[i].gameObject.SetActive(false);
        }
    }

    void LoadFireLvData()
    {
        fireLvNum = PlayerPrefs.GetInt("FireLevel");
    }
}
