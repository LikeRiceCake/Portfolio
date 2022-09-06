using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    Transform targetP;
    [HideInInspector]
    public NavMeshAgent navAgent;
    [HideInInspector]
    public Animator anim;
    [SerializeField]
    EnemyPattern ep;
    [SerializeField]
    Switchboard sb;
    [SerializeField]
    public PlayerMove pm;

    AudioSource audios;
    [SerializeField]
    AudioClip run_walk;
    [SerializeField]
    AudioClip shouting;

    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool isSee;

    public float checkviewTime;

    public float chasingSpeed;

    public bool isDucking;

    Vector3 pos;

    // Start is called before the first frame update
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audios = GetComponent<AudioSource>();
        isSee = false;
        isDucking = false;
        checkviewTime = 0f;
        chasingSpeed = 1.7f;
    }

    void OnEnable()
    {
        StartCoroutine(RandomPos(isSee));
        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    public IEnumerator RandomPos(bool isSee)
    {
        if(!isSee)
        {
            int Floor = Random.Range(1, 3);

            if (Floor == 1)
            {
                Vector3 Pos = new Vector3(Random.Range(-8.959826f, 7.95904f), 0.5f, Random.Range(-15.26223f, 13.52791f));
                navAgent.SetDestination(Pos);
                navAgent.speed = 0.5f;
                anim.SetTrigger("isWalk");
                pos = Pos;
            }
            else if (Floor == 2)
            {
                Vector3 Pos = new Vector3(Random.Range(-8.98504f, 7.972198f), -0.6f, Random.Range(-15.26223f, 14.9864f));
                navAgent.SetDestination(Pos);
                navAgent.speed = 0.5f;
                anim.SetTrigger("isWalk");
                pos = Pos;
            }
            if (Vector3.Distance(transform.position, pos) <= 2f)
            {
                anim.SetTrigger("isIdle");
            }
            yield return new WaitForSeconds(30f);
            anim.SetTrigger("isIdle");
            StartCoroutine(RandomPos(isSee));
        }
    }

    public IEnumerator FindPlayer()
    {
        if(!isSee)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
            // Collider로 이루어진 배열 targets생성
            // Physics.OverlapSphere(기준점, 반경, 레이어설정[해당 레이어에 포함되는 것들만 체크])
            // 기준점을 기준으로 반경을 펼쳐 그 안에 있는 레이어설정에 포함되는 캡슐콜라이더를 수집
            for (int i = 0; i < targets.Length; i++)
            {
                Transform target = targets[i].transform;
                // 만들어진 배열에 있는 타겟들의 포지션을 가져옮
                Vector3 dirTotarget = (target.position - transform.position).normalized;
                // 해당 타겟의 방향(목표위치 - 내 위치)를 구하고 정규화
                if (Vector3.Angle(transform.forward, dirTotarget) < viewAngle / 2)
                // Vector3.Angle(방향, 방향) : 내적 구하기
                // 내가 바라보는 방향과 타겟의 방향의 내적을 구해서 시야각(현재 100)의 / 2보다 작을 경우 참
                // 100의 / 2는 50 즉 해당 오브젝트가 스크립트 오브젝트의 정면 기준 양 옆으로 50 각도씩 벌어지는 각도 안에 있으면 참
                {
                    float disTotarget = Vector3.Distance(transform.position, target.position);
                    // 스크립트 오브젝트와 해당 오브젝트의 거리를 구함 Distance이용
                    if (!Physics.Raycast(transform.position, dirTotarget, disTotarget, obstacleMask))
                    // 레이저 발사(transfomr.position위치에서, dirTotarget방향으로, disTotarget거리만큼, obstacleMask레이어에 포함된 것들만 맞음)
                    // 만약 해당 결과가 참이면(장애물[obstacle]이 있으면) 플레이어 발견 불가
                    // 만약 해당 결과가 거짓이면(장애물[obstacle]이 없으면) 플레이어 발견
                    {
                        anim.SetTrigger("isDiscovery");
                        transform.LookAt(target);
                        yield return new WaitForSeconds(1f);
                        isSee = true;
                        navAgent.speed = chasingSpeed;
                        break;
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FindPlayer());
    }

    void Update()
    {
        if(pm.isDie)
        {
            navAgent.speed = 0f;
        }

        if(!sb.switchBoard && !pm.isDie)
        {
            if (isSee && !isDucking)
            {
                navAgent.SetDestination(targetP.position);
                transform.LookAt(targetP);
                checkView();
            }
        }
        if (sb.switchBoard && !isDucking)
        {
            chasingSpeed = 2f;
        }
        if (sb.switchBoard)
        {
            if (Vector3.Distance(transform.position, targetP.position) < 7f && (pm.ifRun || pm.ifWalk))
            {
                anim.SetTrigger("isDiscovery");
                transform.LookAt(targetP);
                isSee = true;
                navAgent.speed = chasingSpeed;
                navAgent.SetDestination(targetP.position);
            }
        }
    }

    void checkView()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        // Collider로 이루어진 배열 targets생성
        // Physics.OverlapSphere(기준점, 반경, 레이어설정[해당 레이어에 포함되는 것들만 체크])
        // 기준점을 기준으로 반경을 펼쳐 그 안에 있는 레이어설정에 포함되는 캡슐콜라이더를 수집
        for (int i = 0; i < targets.Length; i++)
        {
            Transform target = targets[i].transform;
            // 만들어진 배열에 있는 타겟들의 포지션을 가져옮
            Vector3 dirTotarget = (target.position - transform.position).normalized;
            // 해당 타겟의 방향(목표위치 - 내 위치)를 구하고 정규화
            if (Vector3.Angle(transform.forward, dirTotarget) < viewAngle / 2)
            // Vector3.Angle(방향, 방향) : 내적 구하기
            // 내가 바라보는 방향과 타겟의 방향의 내적을 구해서 시야각(현재 100)의 / 2보다 작을 경우 참
            // 100의 / 2는 50 즉 해당 오브젝트가 스크립트 오브젝트의 정면 기준 양 옆으로 50 각도씩 벌어지는 각도 안에 있으면 참
            {
                float disTotarget = Vector3.Distance(transform.position, target.position);
                // 스크립트 오브젝트와 해당 오브젝트의 거리를 구함 Distance이용
                if (Physics.Raycast(transform.position, dirTotarget, disTotarget, obstacleMask))
                // 레이저 발사(transfomr.position위치에서, dirTotarget방향으로, disTotarget거리만큼, obstacleMask레이어에 포함된 것들만 맞음)
                // 만약 해당 결과가 참이면(장애물[obstacle]이 있으면) 플레이어 발견 불가
                // 만약 해당 결과가 거짓이면(장애물[obstacle]이 없으면) 플레이어 발견
                {
                    checkviewTime += Time.deltaTime;
                    if (checkviewTime >= 10f)
                    {
                        ep.isActive = false;
                        gameObject.SetActive(false);
                        isSee = false;
                        anim.SetTrigger("isTimeOut");
                        checkviewTime = 0f;
                        ep.chasingTime = ep.chasingTimeSet;
                    }
                }
                else if (!Physics.Raycast(transform.position, dirTotarget, disTotarget, obstacleMask))
                {
                    checkviewTime = 0;
                }
            }
        }
    }

    public void WalkSound()
    {
        audios.clip = run_walk;
        audios.Play();
    }

    public void RunSound()
    {
        audios.clip = run_walk;
        audios.Play();
    }

    public void ShoutingSound()
    {
        audios.clip = shouting;
        audios.Play();
    }

    public void EndDucking()
    {
        isDucking = false;
        if (isSee)
        {
            anim.SetTrigger("Run");
            navAgent.speed = 1.7f;
        }
        else
        {
            anim.SetTrigger("isWalk");
            navAgent.speed = 0.5f;
        }
    }
}
