using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float camSpeed = 0;
    public float halfMapSize = 0;
    float horizontalMove = 0;
    [System.NonSerialized]
    public bool isCameraMoving = false;

    void Update()
    {
        // 카메라 이동
        horizontalMove = Input.GetAxisRaw("Horizontal") * camSpeed;
        isCameraMoving = false;
        if( (Mathf.Abs(gameObject.transform.position.x) < halfMapSize) || (horizontalMove * gameObject.transform.position.x < 0) ) { // 카메라 범위 외 이동 방지
            isCameraMoving = true;
            gameObject.transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime;
        }
    }
}