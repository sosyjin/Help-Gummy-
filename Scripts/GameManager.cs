﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 패럴렉스 적용을 위한 2차원 배열 선언 (*인스펙터 표시) | https://m.blog.naver.com/kijw24/221631136622 참조
[System.Serializable]
public class BackgroundList
{
    public GameObject[] backgroundImages;
}

public class GameManager : MonoBehaviour
{
    [Header ("재화")]
    public Text goldText;
    public Text sugarText;
    public float maxTimer = 0;
    int gold = 0;
    // int sugar = 0;
    float startTimer = 0;
    float currentTimer = 0;

    [Header("배경")]
    public BackgroundList [] backgroundList;
    public GameObject mainCamera;
    public float parallaxSpeed;
    Camera mainCameraScript;
    int stageLevel = 0;

    [Header("오브젝트 풀링")]
    public Transform objectPool;
    [Range(30, 100)]
    public int poolSize;



    private void Awake()
    {
        mainCameraScript = mainCamera.gameObject.GetComponent<Camera>();
        startTimer = Time.time;
    }
    private void Update()
    {
        CheckGoldTimer();
        Parallax();
    }

    //패럴렉스
    void Parallax()
    {
        float pivotValue = -Input.GetAxisRaw("Horizontal");
        for(int i = 0; i < backgroundList[stageLevel].backgroundImages.Length; i++) {
            if (mainCameraScript.isCameraMoving) {
                int parallaxSpeedRatio = int.Parse(backgroundList[stageLevel].backgroundImages[i].name.Split('_')[1]);
                backgroundList[stageLevel].backgroundImages[i].gameObject.transform.position += new Vector3(pivotValue, 0, 0) * parallaxSpeedRatio * parallaxSpeed * Time.deltaTime;
            }
        }
    }
    // 골드 타이머
    void CheckGoldTimer()
    {
        currentTimer = Time.time - startTimer;
        if (currentTimer >= maxTimer) {
            ResetGoldTimer();
            gold += 100;
        }
    }
    void ResetGoldTimer()
    {
        startTimer = Time.time;
        goldText.text = gold.ToString();
    }

    // 스테이지 초기화
    public void SetStageLevel(int inputStageLevel) {
        // 골드 초기화
        startTimer = Time.time;
        gold = 0;
        goldText.text = gold.ToString();

        // 스테이지 레벨 설정
        stageLevel = inputStageLevel;
        for (int i = 0; i < backgroundList.Length; i++) { // 스테이지 배경 비활성화
            for(int j = 0; j < backgroundList[i].backgroundImages.Length; j++)
                backgroundList[i].backgroundImages[j].SetActive(false);
        }
        for (int i = 0; i < backgroundList[stageLevel].backgroundImages.Length; i++) // 선택된 스테이지 배경 활성화
            backgroundList[stageLevel].backgroundImages[i].SetActive(true);
    }
}
