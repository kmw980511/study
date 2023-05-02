using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float h, v, r;
    public float moveSpeed = 10f;
    public float turnSpeed = 150f;
    private Transform tr;
    private Rigidbody rbody;
    private Vector3 prevPosition;

    void Start()
    {
        tr = GetComponent<Transform>();
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");
        Vector3 moveDir = (Vector3.right * h) + (Vector3.forward * v);
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        tr.Rotate(Vector3.up * r * turnSpeed * Time.deltaTime);

        prevPosition = tr.position;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("WALL") || 
            col.gameObject.layer == LayerMask.NameToLayer("OBSTACLE"))
        {
            rbody.velocity = Vector3.zero;
            transform.position = prevPosition;
        }
    }
}
