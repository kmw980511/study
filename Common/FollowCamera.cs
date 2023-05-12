using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;  //Ÿ��
    [SerializeField]
    private float Height = 10f;  //ī�޶� ����
    [SerializeField]
    private float distance = 7f; //ī�޶� �Ÿ�
    public float moveDamping = 10f;  //�̵��� �ӵ� ���(�ε巯�)
    public float rotDamping = 15f;   //ȸ���� �ӵ� ���
    private Transform tr;
    [Header("Camera Obstacle Setting")]//ī�޶���ֹ�����
    public float heightAboveObstacle = 15f; //ī�޶� �ö� ����
    public float castOffset = 1.0f; //���ΰ��� ������ ����ĳ��Ʈ ���� ������
    public float originHeight;
    public float overDamping = 3f;

    private AudioSource source;
    private AudioClip bgmClip;

    void Start()
    {
        source = GetComponent<AudioSource>();
        bgmClip = Resources.Load("Sounds/BGM") as AudioClip;
        originHeight = Height; //���߿� �ǵ����� ����
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 castTarget = target.position + (target.up * castOffset);
        Vector3 castDir = (castTarget - tr.position).normalized; //Ÿ�ٿ��� �ڱ��ڽ��� ��ġ�� ���� ������ ����
        RaycastHit hit;
        if (Physics.Raycast(tr.position, castDir, out hit, Mathf.Infinity))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                Height = Mathf.Lerp(Height, heightAboveObstacle, Time.deltaTime * overDamping);
                //�ȸ¾����� ���� Height������ 15���� �ö󰡴µ� �ε巴�� 5��ŭ
            }
            else
            {
                Height = Mathf.Lerp(Height, originHeight, Time.deltaTime * overDamping);
            }
        }
    }

    void LateUpdate() //ī�޶� �÷��̾� ���󰡰� �̵���
    {
        var camPos = target.position - (target.forward * distance) + (target.up * Height);

        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotDamping);
        tr.LookAt(target.position + (target.up * 1f));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //���� �� �þ߸� ���� ��ġǥ��
        Gizmos.DrawWireSphere(target.position + (target.up * 2.0f), 0.1f);
        //Gizmos.DrawLine(target.position + (target.up * 2.0f), tr.position);
    }
}

