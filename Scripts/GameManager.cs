using System.Collections;
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
    [Header("재화")]
    public Text goldText;
    public Text sugarText;
    public float maxTimer = 0;
    int gold = 0;
    // int sugar = 0;
    float startTimer = 0;
    float currentTimer = 0;

    [Header("배경")]
    public BackgroundList[] backgroundList;
    public GameObject mainCamera;
    public float parallaxSpeed;
    Camera mainCameraScript;
    int stageLevel = 0;

    [Header("오브젝트 풀")]
    public Transform objectPool;

    [Header("게임 재생, 시간")]
    public Text gamePlayText;
    public Text gameAccelText;

    [Header("크래프팅 UI")]
    public Animator craftingAnim;
    public Animator shopAnim;
    public Image [] craftingImages;
    public Text jellyNameText;
    public Text craftingSugarText;
    public Text craftingJuiceText;
    int craftingCursor = 0;

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
        for (int i = 0; i < backgroundList[stageLevel].backgroundImages.Length; i++) {
            if (mainCameraScript.isCameraMoving) {
                int parallaxSpeedRatio = int.Parse(backgroundList[stageLevel].backgroundImages[i].name.Split('_')[1]);
                backgroundList[stageLevel].backgroundImages[i].gameObject.transform.position += new Vector3(pivotValue, 0, 0) * parallaxSpeedRatio * parallaxSpeed * Time.deltaTime;
            }
        }
    }

    // 스테이지 초기화
    public void SetStageLevel(int inputStageLevel)
    {
        // 골드 초기화
        startTimer = Time.time;
        gold = 0;
        goldText.text = gold.ToString();

        // 스테이지 레벨 설정
        stageLevel = inputStageLevel;
        for (int i = 0; i < backgroundList.Length; i++) { // 전체 스테이지 배경 비활성화
            for (int j = 0; j < backgroundList[i].backgroundImages.Length; j++)
                backgroundList[i].backgroundImages[j].SetActive(false);
        }
        for (int i = 0; i < backgroundList[stageLevel].backgroundImages.Length; i++) // 선택된 스테이지 배경 활성화
            backgroundList[stageLevel].backgroundImages[i].SetActive(true);
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

    // 게임 가속
    public void TimeAccelalation()
    {
        if (Time.timeScale == 0) {
            return;
        } else if (Time.timeScale < 4) {
            Time.timeScale *= 2;
            gameAccelText.text = "x" + Time.timeScale.ToString();
        } else {
            Time.timeScale = 1;
            gameAccelText.text = "x1";
        }
    }
    // 게임 정지
    public void TimeStop()
    {
        if (Time.timeScale != 0) {
            gamePlayText.text = "▶";
            Time.timeScale = 0;
        } else {
            gamePlayText.text = "||";
            Time.timeScale = int.Parse(gameAccelText.text.Replace("x", string.Empty)); // "x숫자" -> "숫자" | 가공해서 시간에 대입
        }
    }

    // 크래프팅 젤리 이미지 전환
    public void ChangeCraftingImage(string buttonType)
    {
        // 모든 이미지 비활성화
        for (int i = 0; i < craftingImages.Length; i++)
            craftingImages[i].gameObject.SetActive(false);

        // 선택된 이미지 활성화
        switch(buttonType) {
            case "left":
                if(craftingCursor == 0)
                    craftingCursor = craftingImages.Length - 1;
                else
                    craftingCursor -= 1;
                break;
            case "right":
                if (craftingCursor == craftingImages.Length - 1)
                    craftingCursor = 0;
                else
                    craftingCursor += 1;
                break;
        }
        craftingImages[craftingCursor].gameObject.SetActive(true);

        // 젤리 이름 텍스트 변경
        switch(craftingCursor) {
            case 0:
                jellyNameText.text = "곰 젤리";
                break;
            case 1:
                jellyNameText.text = "젤리빈";
                break;
            case 2:
                jellyNameText.text = "왕 꿈틀이";
                break;
            case 3:
                jellyNameText.text = "버거 젤리";
                break;
            case 4:
                jellyNameText.text = "젤리 푸딩";
                break;
        }
    }

    // 크래프팅 UI 애니메이션
    public void PlayCraftingPanelAnimation()
    {
        if(craftingAnim.GetBool("panelAppear")) {
            craftingAnim.SetBool("panelAppear", false);
            craftingAnim.SetBool("panelDisappear", true);
        } else {
            craftingAnim.SetBool("panelAppear", true);
            craftingAnim.SetBool("panelDisappear", false);
        }
    }
    public void PlayShopPanelAnimation()
    {
        if (shopAnim.GetBool("Appear"))
        {   shopAnim.SetBool("Appear", false);
            shopAnim.SetBool("Disappear", true);
        }
        else
        {   shopAnim.SetBool("Appear", true);
            shopAnim.SetBool("Disappear", false);
        }
    }

    // 크래프팅 - 설탕, 과즙 수치 UI
    public void ChangeCraftingStatus(string buttonValue)
    {
        string targetType = buttonValue.Split('_')[0];
        string buttonType = buttonValue.Split('_')[1];
        Text targetText;
        int textValue = 0;

        switch (targetType) {
            case "sugar":
                targetText = craftingSugarText;
                textValue = int.Parse(targetText.text);
                switch (buttonType)
                {
                    case "down":
                        if (textValue == 0)
                            break;
                        else
                            targetText.text = (textValue - 1).ToString();
                        break;
                    case "up":
                        if (textValue == 10)
                            break;
                        else
                            targetText.text = (textValue + 1).ToString();
                        break;
                }
                break;
            case "juice":
                targetText = craftingJuiceText;
                textValue = int.Parse(targetText.text);
                switch (buttonType)
                {
                    case "down":
                        if (textValue == 0)
                            break;
                        else
                            targetText.text = (textValue - 1).ToString();
                        break;
                    case "up":
                        if (textValue == 10)
                            break;
                        else
                            targetText.text = (textValue + 1).ToString();
                        break;
                }
                break;
        }
    }
}
