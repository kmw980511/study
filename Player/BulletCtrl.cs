using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField] private Rigidbody rBullet;
    [SerializeField] private float bSpeed = 1000f;
    public float damage = 1.0f;

    void Start()
    {
        rBullet = GetComponent<Rigidbody>();
        rBullet.AddForce(transform.forward * bSpeed, ForceMode.Force);
        Destroy(this.gameObject, 3.0f);
    }
}
