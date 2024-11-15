using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFire : MonoBehaviour
{

    // 총알 파편 효과 공장(Prefab) 프리팹은 다 게임  오브젝트
    public GameObject bulletImpactFactory;

    // Raycast를  이용한 총알 발사에 검출되는 Layer 설정
    public LayerMask layerMask;

    // 폭탄 공장(Prefab)
    public GameObject bombFactory;
    


    void Start()
    {
        
    }

    void Update()
    {
        // 1번키를 누르면
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FireRay();
        }

        // 2번키를 누르면
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FireBomb();
        }

    }

    void FireBomb()
    {
        // 폭탄 공장(Prefab) 에서 폭탄을 만들자
        GameObject bomb = Instantiate(bombFactory);
        // 폭탄을 카메라 앞방향 1만큼 떨어진 위치 놓자.
        bomb.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1;
        // bomb.transform.position = Camera.main.transform.forward * 1;   이거는 안 된다. 멀리 떨어져 있어도 크기랑 방향이 같으면 같은 벡터니까 꼭 지정된  위치를 정하고 싶으면   더해야 한다.
        // 폭탄의 앞방향을 카메라의 앞방향으로 설정
        bomb.transform.forward = Camera.main.transform.forward;
    }

    void FireRay()
    {
        // 카메라 위치에서 카메라 앞방향으로 향하는 Ray를 만들자.
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // Ray를 발사해서 어딘가에 부딪혔다면
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))
        {
            // 총알 파편 효과를 생성하자.
            GameObject bulletImpact = Instantiate(bulletImpactFactory);
            // 생성된 효과에 부딪힌 위치에 놓자.
            bulletImpact.transform.position = hitInfo.point;

            // 생성된 효과의 앞방향을 부딪힌 위치의 normal 방향으로 설정
            bulletImpact.transform.forward = hitInfo.normal;

            // 반사각 구하자.
            Vector3 outDirection = Vector3.Reflect(ray.direction, hitInfo.normal);

            // 부딪힌 오브젝트의 이름과 부딪힌 위치를 출력해보자.
            // print(hitInfo.transform.name + "," + hitInfo.transform.position);
            // print(hitInfo.transform.name + "," + hitInfo.point);

            // hitInfo.distance;이런 것도 있다.
            // hitInfo.normal 법선벡터(면의 수직인 벡터)
        }
    }
}
