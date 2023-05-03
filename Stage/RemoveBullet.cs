using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    string bulletTag = "BULLET";
    string e_bulletTag = "E_BULLET";

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == bulletTag || col.collider.tag == e_bulletTag)
        {
            Destroy(col.gameObject);
        }
    }
}
