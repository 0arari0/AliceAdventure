using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRolling : MonoBehaviour
{
    /* 20220814 작성자 : 김두현
     * 해당 스크립트는 배틀씬에서 배경 이미지가 무한 스크롤 되도록 만들어줍니다.
     */

    const float fixedRollingSpeed = 0.3f;
    MeshRenderer meshRenderer;
    float offset = 0;
    public float rollingSpeed = fixedRollingSpeed;
    public float RollingSpeed { set { rollingSpeed = value; } }
    // rollingSpeed 를 외부에서 수정할 수 있도록 하였으니 필요하실때 수정하시면 됩니다.
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        offset += Time.deltaTime * rollingSpeed;
        meshRenderer.material.mainTextureOffset = new Vector2(0, offset);
    }

    public void InitRollingSpeed()
    {
        // 이 함수는 rollingSpeed 를 변경한 이후에 다시 원래 속도로 만들고 싶을때 호출하면 됩니다.
        rollingSpeed = fixedRollingSpeed;
    }
}