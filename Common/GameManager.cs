using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private List<Transform> obsSpawnPoints;
    [SerializeField] private Transform[] obsPoints;
    [SerializeField] private List<Transform> e_Points;
    [SerializeField] private GameObject enemy;
    public bool IsGameover = false;
    public int MaxEnemy = 10;
    private int maxObstacleCnt = 6;
    private int randomIdx;
    private bool IsPaused;
    public Image PauseMenu;
    public RectTransform SoundOption;
    [SerializeField] private GameObject PlayerObj;
    [SerializeField] private Text KillCntTxt;
    public int KillCnt=0;
    public static GameManager gameManager;
    [SerializeField] EnemyAI enemyAI;
    private FireCtrl fireCtrl;
    private Damage damage;
    public GameObject Portal;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(this.gameObject);
        }

        PlayerObj = GameObject.Find("Player").GetComponent<GameObject>();
        obstaclePrefab = Resources.Load<GameObject>("Prefabs/SideBlock_1");
        enemy = Resources.Load<GameObject>("ENEMY");
        KillCntTxt = GameObject.Find("Text-Score").GetComponent<Text>();
        PauseMenu = GameObject.Find("Panel-MenuBG").transform.GetChild(0).GetComponent<Image>();

        if (PlayerPrefs.HasKey("SCORE"))
        {
            KillCnt = PlayerPrefs.GetInt("SCORE");
            KillCntTxt.text = "Score : " + KillCnt.ToString();
        }

        DontDestroyOnLoad(gameManager);
    }

    void Start()
    {
        GameObject enemyGroup = GameObject.Find("EnemyPoint");

        if (enemyGroup != null)
        {
            enemyGroup.GetComponentsInChildren<Transform>(e_Points);
        }
        e_Points.RemoveAt(0);

        StartCoroutine(Createnemy());
        
        PlayerPrefs.DeleteAll();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BeforeBossScene" || scene.name == "MiddleBossScene" || scene.name == "LastBossScene")
        {
            GameObject obsGroup = GameObject.Find("ObstacleSP");
            ObsRandomSpawn(obsGroup);
        }

        if (scene.name == "StartScene")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused)
        {
            if (Damage.playerdie == true) return;

            OnPauseClick();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && IsPaused)
        {
            if (Damage.playerdie == true) return;

            OnPauseClick();
        }

        if(KillCnt >= 10)
        {
            Portal.SetActive(true);
        }

        if (damage.CurHp <= 0)
        {
            OnPauseClick();
        }

        if(IsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void KillCount(int score)
    {
        KillCnt += score;
        PlayerPrefs.SetInt("SCORE", KillCnt);
        KillCntTxt.text = "Score : " + KillCnt.ToString();
    }

    private void ObsRandomSpawn(GameObject obsGroup)
    {
        if (obsGroup != null)
        {
            obsGroup.GetComponentsInChildren<Transform>(obsSpawnPoints);
        }
        obsSpawnPoints.RemoveAt(0);

        randomIdx = Random.Range(0, obsPoints.Length);
        obsPoints = obsSpawnPoints.ToArray();

        List<int> indices = new List<int>();
        for (int i = 0; i < obsPoints.Length; i++)
        {
            if (i != randomIdx)
            {
                indices.Add(i);
            }
        }

        for (int i = 0; i < maxObstacleCnt; i++)
        {
            int randomIndex = Random.Range(0, indices.Count);
            int idx = indices[randomIndex];
            indices.RemoveAt(randomIndex);
            Instantiate(obstaclePrefab, obsPoints[idx].position, obsPoints[idx].rotation);
        }
    } 

    IEnumerator Createnemy()
    {
        while (!IsGameover)
        {
            if (IsGameover) yield break;
            int enemcount = (int)GameObject.FindGameObjectsWithTag("ENEMY").Length;
            if (enemcount < MaxEnemy)
            {
                yield return new WaitForSeconds(10f);
                int idx = Random.Range(0, e_Points.Count);
                Instantiate(enemy, e_Points[idx].position, e_Points[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("BeforeBossScene");
    }

    public void OnPauseClick()
    {
        IsPaused = !IsPaused;
        Time.timeScale = (IsPaused) ? 0.0f : 1.0f;
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        var playerScripts = playerObj.GetComponents<MonoBehaviour>();
        foreach (var script in playerScripts)
        {
            script.enabled = !IsPaused;
        }
        Pause();
    }

    public void OnRestartBtnClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void EndSceneBtn()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void Pause()
    {
        if (PauseMenu.gameObject.activeInHierarchy == false)
        {
            PauseMenu.gameObject.SetActive(true);
        }
        else
        {
            PauseMenu.gameObject.SetActive(false);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}