// 작성자 감귤오렌지
// http://blog.naver.com/cdw0424

using UnityEngine;
using Unity.Rendering;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;

public class ECS_Manager : MonoBehaviour
{
    [SerializeField] Mesh mesh;
    [SerializeField] Material material;

    [Header("생성할 엔티티 갯수")]
    [SerializeField] int Count;

    [Header("엔티티 간 거리 계수")]
    [SerializeField] float Ditance = 0.01f;

    void Start()
    {
        // 엔티티 월드에 접근하여 엔티티 매니저를 가져와 캐싱.
        EntityManager EM = World.DefaultGameObjectInjectionWorld.EntityManager;

        // 아키타입 생성 후 캐싱.
        // 렌더링을 위해서는 반드시 아래의 4가지 컴포넌트들을 포함해야만 함.
        // Translation  - 위치 지정. (using Unity.Transforms;)
        // RenderMesh   - 렌더링. (using Unity.Rendering;)
        // LocalToWorld - 지역 좌표를 월드좌표로 바꿔주는 역할. (using Unity.Rendering;)
        // RenderBounds - 메쉬의 경계면 처리. (using Unity.Rendering;)
        EntityArchetype AT = EM.CreateArchetype(
            typeof(Translation), 
            typeof(Rotation), 
            typeof(Scale), 
            typeof(RenderMesh), 
            typeof(LocalToWorld), 
            typeof(RenderBounds)
            );

        // Count개짜리 네이티브 배열 생성. 메모리는 1프레임 안에 해제 예정.
        NativeArray<Entity> entities = new NativeArray<Entity>(Count, Allocator.Temp);

        // AT라는 아키타입을 가지는 엔티티를 배열크기만큼 생성하여 각 배열 index마다 할당. 
        EM.CreateEntity(AT, entities);

        // 네이티브 배열에 할당된 엔티티들의 정보 수정.
        foreach (Entity entity in entities)
        {
            // 필요에 따라 이처럼 동적으로 컴포넌트를 추가할 수도있음.
            // EM.AddComponentData();
            // EM.AddSharedComponentData();

            // x,y,z 각각 0~Count사이의 랜덤한 위치로 지정.(GameObject로 치면 Position의 역할.)
            EM.SetComponentData(entity, new Translation
            {
                Value = new float3(UnityEngine.Random.Range(0, Count* Ditance),
                UnityEngine.Random.Range(0, Count* Ditance),
                UnityEngine.Random.Range(0, Count* Ditance))
            });

            // 크기 지정.
            EM.SetComponentData(entity, new Scale { Value = 10f });

            // 렌더링
            // 메쉬와 머티리얼은 보통 하나의 리소스를 공유해서 사용하기때문에 SetSharedComponentData라는 함수를 이용해야함.
            // 고맙게도 SetComponentData라고 작성하면 컴파일 에러 띄워 줌.
            EM.SetSharedComponentData(entity, new RenderMesh { mesh = mesh, material = material });
        }

        // entities 네이티브 어레이 메모리 해제.
        entities.Dispose();

    }

}
