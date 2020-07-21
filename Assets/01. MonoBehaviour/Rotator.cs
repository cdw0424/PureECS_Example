// 작성자 감귤오렌지
// http://blog.naver.com/cdw0424

using UnityEngine;


public class Rotator : MonoBehaviour
{
    Transform _transform;

    public void InitObj()
    {
        // transform 캐싱.
        _transform = this.gameObject.transform;

        Debug.Log("초기화 됨");
    }

    // Update is called once per frame
    void Update()
    {
        _transform.rotation = Quaternion.Euler(0, Time.time*10, 0);
    }
}
