using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header ("재화")]
    public Text goldText;
    public Text sugarText;
    public float maxTimer = 0;
    int gold = 0;
    int sugar = 0;
    float startTimer = 0;
    float currentTimer = 0;

    [Header("배경")]
    public GameObject [] backImages;
    int stageLevel = 0;

    private void Awake()
    {
        startTimer = Time.time;
    }
    private void Update()
    {
        CheckGoldTimer();
    }

    // 골드 타이머
    void CheckGoldTimer ()
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
        for (int i = 0; i < backImages.Length; i++) // 스테이지 배경 비활성화
            backImages[i].SetActive(false);
        backImages[stageLevel].SetActive(true); // 선택된 스테이지 배경 활성화
    }
}
