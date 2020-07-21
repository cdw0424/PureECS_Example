// 작성자 감귤오렌지
// http://blog.naver.com/cdw0424

using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RotatorSystem : SystemBase
{
    protected override void OnCreate()
    {
        Debug.Log("시스템 실행");
    }

    protected override void OnUpdate()
    {
        // 메인스레드 접근 오류 방지를 위한 인스턴싱.
        var time = UnityEngine.Time.time;

        // 버스트컴파일
        // Rotation 컴포넌트를 참조형태로 전부 가져옴.
        // 회전.
        // 병렬처리.
        Entities.WithBurst().ForEach((ref Rotation rotation) => {
            rotation.Value = quaternion.Euler(0, time * 10, 0);
        }).ScheduleParallel();
    }

}
