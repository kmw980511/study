using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;  //타겟
    [SerializeField]
    private float Height = 10f;  //카메라 높이
    [SerializeField]
    private float distance = 7f; //카메라 거리
    public float moveDamping = 10f;  //이동시 속도 계수(부드러운값)
    public float rotDamping = 15f;   //회전시 속도 계수
    private Transform tr;
    [Header("Camera Obstacle Setting")]//카메라장애물세팅
    public float heightAboveObstacle = 15f; //카메라가 올라갈 높이
    public float castOffset = 1.0f; //주인공에 투사할 레이캐스트 높이 오프셋
    public float originHeight;
    public float overDamping = 3f;

    private AudioSource source;
    private AudioClip bgmClip;

    void Start()
    {
        source = GetComponent<AudioSource>();
        bgmClip = Resources.Load("Sounds/BGM") as AudioClip;
        originHeight = Height; //나중에 되돌리기 위함
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 castTarget = target.position + (target.up * castOffset);
        Vector3 castDir = (castTarget - tr.position).normalized; //타겟에서 자기자신의 위치를 빼면 방향이 나옴
        RaycastHit hit;
        if (Physics.Raycast(tr.position, castDir, out hit, Mathf.Infinity))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                Height = Mathf.Lerp(Height, heightAboveObstacle, Time.deltaTime * overDamping);
                //안맞았으면 원래 Height값에서 15까지 올라가는데 부드럽게 5만큼
            }
            else
            {
                Height = Mathf.Lerp(Height, originHeight, Time.deltaTime * overDamping);
            }
        }
    }

    void LateUpdate() //카메라가 플레이어 따라가고 이동함
    {
        var camPos = target.position - (target.forward * distance) + (target.up * Height);

        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotDamping);
        tr.LookAt(target.position + (target.up * 1f));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //추적 및 시야를 맞출 위치표시
        Gizmos.DrawWireSphere(target.position + (target.up * 2.0f), 0.1f);
        //Gizmos.DrawLine(target.position + (target.up * 2.0f), tr.position);
    }
}

