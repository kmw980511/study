using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPortal : MonoBehaviour
{
    private readonly string playerTag = "Player";

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag) && GameManager.gameManager.KillCnt >= 2)
        {
            SceneManager.LoadScene("MiddleBossScene");
        }
    }
}
