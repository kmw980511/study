using UnityEngine;
using UnityEngine.SceneManagement;
public class StatBtn : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("BeforeBossScene");
        }
    }
}
