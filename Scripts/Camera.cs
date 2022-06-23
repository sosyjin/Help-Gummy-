using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    float leftMove = 0;
    float rightMove = 0;
    float camMove = 0;
    public float camSpeedRatio = 0;
    public float halfMapSize = 0;

    void FixedUpdate()
    {
        leftMove = Input.GetAxisRaw("Horizontal") * camSpeedRatio;
        rightMove = Input.GetAxisRaw("Horizontal") * camSpeedRatio;
        camMove = leftMove + rightMove;

        if(Mathf.Abs(gameObject.transform.position.x) < halfMapSize) { // 카메라 이동 방지
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + camMove, gameObject.transform.position.y, gameObject.transform.position.z);
        } else if (camMove * gameObject.transform.position.x < 0) {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + camMove, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}
