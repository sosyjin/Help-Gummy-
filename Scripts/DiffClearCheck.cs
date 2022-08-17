using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스테이지 클리어 여부를 확인하고 클리어 뱃지를 가시화
public class DiffClearCheck : MonoBehaviour 
{
    
    //public Image[] clearBadges;

    public GameObject LockPanel_Normal;
    public GameObject LockPanel_Hard;

    private void Awake()
    {

        // 배지 비가시화 (에러 방지)
        /*
        clearBadges[0].enabled = false;
        clearBadges[1].enabled = false;
        clearBadges[2].enabled = false;
        */

        LockPanel_Normal.SetActive(true);
        LockPanel_Hard.SetActive(true);
    }

    void OnEnable()
    {
        // 클리어 여부 확인
        bool isEasyCleared = PlayerPrefs.GetInt("isEasyCleared") == 1;
        bool isNormalCleared = PlayerPrefs.GetInt("isNormalCleared") == 1;
        bool isHardCleared = PlayerPrefs.GetInt("isHardCleared") == 1;

        // 배지 가시화
        if (isEasyCleared) {
            //clearBadges[0].enabled = true;
            LockPanel_Normal.SetActive(false);
        } if(isNormalCleared) {
            //clearBadges[1].enabled = true;
            LockPanel_Hard.SetActive(false);
        } if(isHardCleared) {
            //clearBadges[2].enabled = true;
        }
    }
}