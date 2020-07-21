// 작성자 감귤오렌지
// http://blog.naver.com/cdw0424

using UnityEngine;

public class MonoTest : MonoBehaviour
{
    [SerializeField] GameObject Prefab = null;

    [Header("생성할 오브젝트 갯수")]
    [SerializeField] int Count = 100;

    private void Start()
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject obj = Instantiate(Prefab, new Vector3(Random.Range(0, 1000), Random.Range(0, 1000), Random.Range(0, 1000)), Quaternion.identity);

            obj.GetComponent<Rotator>().InitObj();
        }
    }
}
