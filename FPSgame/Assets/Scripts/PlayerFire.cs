using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //발사 위치
    public GameObject bombFactory; //투척 무기 오브젝트
    public float throwPower = 15f; //투척 파워
    public GameObject bulletEffect; //피격 이펙트 오브젝트
    ParticleSystem ps; //피격 에픽트 파티클 시스템
    public int weaponPower = 5; //발사 무기 공격력

    void Start()
    {
        //피격 이펙트 오브젝트에서 파티클 시스템 컴포넌트 
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //게임 상태가 '게임 중' 상태일 때만 조작할 수 있게 한다
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // 마우스 오른쪽 버튼을 누르면 시선이 바라보는 방향으로 수류탄을 던진다

        //1. 마우스 오른쪽 버튼 입력
        if (Input.GetMouseButtonDown(1))
        {
            //수류탄 오브젝트를 생성한 후 수류탄의 생성 위치를 발사 위치로 한다
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            //수류탄 오브젝트의 Rigidbody 컴포넌트 
            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            //카메라의 정면 방향으로 수류탄에 물리적 힘을 가함
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }

        //마우스 왼쪽 버튼을 누르면 시선이 바라보는 방향으로 총 발사

        //마우스 왼쪽 버튼 입력
        if(Input.GetMouseButtonDown(0))
        {
            //레이를 생성한 후 발사될 위치와 진행 방향 설정
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //레이가 부딪힌 대상의 정보를 저장할 변수를 생성
            RaycastHit hitInfo = new RaycastHit();

            //레이를 발사한 후 만일 부딪힌 물체가 있으면 피격 이벤트 실행
            if(Physics.Raycast(ray, out hitInfo))
            {
                //만일 레이에 부딪힌 대상의 레이어가 Enemy라면 데미지 함수 실행
                if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                }
                //그렇지 않다면 레이에 부딪힌 지점에 피격 이펙트 플레이
                else
                {
                    //피격 이펙트의 위치를 레이가 부딪힌 지점으로 이동
                    bulletEffect.transform.position = hitInfo.point;

                    //피격 이펙트의 forward 방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다
                    bulletEffect.transform.forward = hitInfo.normal;

                    //피격 이펙트 플레이
                    ps.Play();
                }
            }
        }
    }
}
