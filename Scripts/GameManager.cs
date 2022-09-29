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
    public float maxTimer;
    [System.NonSerialized]
    public int gold = 0;
    [System.NonSerialized]
    public int sugar = 0;
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
    public Text gameAccelText;

    [Header("크래프팅 UI")]
    public GameObject craftingPanelObject;
    public Image [] craftingImages;
    public Text jellyNameText;
    public Text craftingSugarText;
    public Text craftingJuiceText;
    [System.NonSerialized]
    public int craftingCursor = 0;
    Animator craftingAnim;

    [Header("상점 UI")]
    public GameObject shopPanelObject;
    public GameObject[] craftingLockPanels;
    public Image[] shopingImages;
    public Image soldOutImage;
    int shopingCursor = 0;
    Animator shopAnim;

    [Header("etc")]
    public GameObject optionGameObject;
    public Text noticeText;
    public Animator noticeAnim;
    public Image gameSpeedUIImage;
    public Sprite [] gameSpeedUIImageSources;

    private void Awake()
    {
        mainCameraScript = mainCamera.gameObject.GetComponent<Camera>();
        startTimer = Time.time;
        craftingAnim = craftingPanelObject.GetComponent<Animator>();
        shopAnim = shopPanelObject.GetComponent<Animator>();
    }
    private void Update()
    {
        CheckGoldTimer();
        Parallax();
    }

    //패럴렉스
    void Parallax()
    {
        float pivotValue = -mainCameraScript.horizontalMove;
        for (int i = 0; i < backgroundList[stageLevel].backgroundImages.Length; i++) {
            if (mainCameraScript.isCameraMoving) {
                int parallaxSpeedRatio = int.Parse(backgroundList[stageLevel].backgroundImages[i].name.Split('_')[1]);
                backgroundList[stageLevel].backgroundImages[i].gameObject.transform.position += new Vector3(pivotValue, 0, 0) * parallaxSpeedRatio * parallaxSpeed * Time.deltaTime;
            }
        }
    }

    // 스테이지 버튼
    public void SetStageLevel(int inputStageLevel)
    {
        // 골드 초기화
        startTimer = Time.time;
        gold = 0;
        goldText.text = gold.ToString();

        // 전체 스테이지 배경 비활성화
        for (int i = 0; i < backgroundList.Length; i++) {
            for (int j = 0; j < backgroundList[i].backgroundImages.Length; j++)
                backgroundList[i].backgroundImages[j].SetActive(false);
        }
        // 스테이지 레벨 설정
        if (inputStageLevel == -1)
            return;
        // 선택된 스테이지 배경 활성화
        stageLevel = inputStageLevel;
        for (int i = 0; i < backgroundList[stageLevel].backgroundImages.Length; i++)
            backgroundList[stageLevel].backgroundImages[i].SetActive(true);
    }

    // 골드 타이머
    void CheckGoldTimer()
    {
        currentTimer = Time.time - startTimer;
        if (currentTimer >= maxTimer) {
            gold += 100;
            ResetGoldTimer();
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
        if (Time.timeScale < 3) {
            gameSpeedUIImage.sprite = gameSpeedUIImageSources[(int)Time.timeScale++];
            gameAccelText.text = int.Parse(gameAccelText.text + 1).ToString();
        } else {
            Time.timeScale = 1f;
            gameSpeedUIImage.sprite = gameSpeedUIImageSources[0];
            gameAccelText.text = "1";
        }
    }
    // 게임 정지
    public void TimeStop()
    {
        if (Time.timeScale != 0) {
            Time.timeScale = 0;

            // 옵션 UI 활성화
            optionGameObject.SetActive(true);
        } else {
            Time.timeScale = int.Parse(gameAccelText.text);

            // 옵션 UI 비활성화
            optionGameObject.SetActive(false);
        }
    }

    // 크래프팅, 상점 - 젤리 이미지 전환
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
                jellyNameText.text = "푸딩 젤리";
                break;
            case 3:
                jellyNameText.text = "버거 젤리";
                break;
        }
    }
    public void ChangeShopingImages(string buttonType)
    {
        // 모든 이미지 비활성화
        for (int i = 0; i < shopingImages.Length; i++)
            shopingImages[i].gameObject.SetActive(false);

        // 선택된 이미지 활성화
        switch (buttonType)
        {
            case "left":
                if (shopingCursor == 0)
                    shopingCursor = shopingImages.Length - 1;
                else
                    shopingCursor -= 1;
                break;
            case "right":                                             
                if (shopingCursor == shopingImages.Length - 1)        
                    shopingCursor = 0;
                else
                    shopingCursor += 1;
                break;
        }
        shopingImages[shopingCursor].gameObject.SetActive(true);

        if (!craftingLockPanels[shopingCursor].activeSelf) {
            soldOutImage.enabled = true;
        } else {
            soldOutImage.enabled = false;
        }
    }

    // 크래프팅, 상점 - UI 애니메이션
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
        if (shopAnim.GetBool("Appear")) {
            shopAnim.SetBool("Appear", false);
            shopAnim.SetBool("Disappear", true);
        } else {
            shopAnim.SetBool("Appear", true);
            shopAnim.SetBool("Disappear", false);
        }
    }

    // 크래프팅 - 설탕, 과즙 수치 UI
    public void ChangeCraftingStatus(string buttonValue)
    {
        string targetType = buttonValue.Split('_')[0];
        string buttonType = buttonValue.Split('_')[1];
        Text targetText;
        int textValue;

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

    // 스테이지 초기화
    public void ResetStage()
    {
        // 풀 내(inside)의 오브젝트 비활성화
        for(int i = 0; i < objectPool.childCount; i++) {
            objectPool.GetChild(i).gameObject.SetActive(false);
        }

        // 재화 초기화
        gold = 0;
        goldText.text = gold.ToString();
        sugar = 0;
        sugarText.text = sugar.ToString();

        // 레시피 초기화
        for(int i = 0; i < craftingLockPanels.Length; i++) {
            craftingLockPanels[i].SetActive(true);
            soldOutImage.enabled = false;
        }

        // Base 스탯 초기화

        // 게임 재생 속도 초기화
        gameSpeedUIImage.sprite = gameSpeedUIImageSources[0];
        gameAccelText.text = "1";
        Time.timeScale = 1f;

        // UI 위치 초기화
        if (craftingAnim.GetBool("panelAppear")) {
            craftingAnim.SetBool("panelAppear", false);
            craftingAnim.SetBool("panelDisappear", true);
            craftingPanelObject.transform.position = new Vector3(500f, craftingPanelObject.transform.position.y, craftingPanelObject.transform.position.z);
        }
        if (shopAnim.GetBool("Appear")) {
            shopAnim.SetBool("Appear", false);
            shopAnim.SetBool("Disappear", true);
            shopPanelObject.transform.position = new Vector3(-500f, shopPanelObject.transform.position.y, shopPanelObject.transform.position.z);
        }
    }

    // 지정된 씬으로 이동
    public void GoToScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);

        // 게임 속도 초기화
        Time.timeScale = 1f;
    }

    // 레시피 구매 
    public void RecipePurchase()
    {
        // 재구매 방지
        if (!craftingLockPanels[shopingCursor].activeSelf) {
            noticeText.text = "이미 구매하신 품목입니다.";
            noticeAnim.ResetTrigger("notice");
            noticeAnim.SetTrigger("notice");
            return;
        }

        // 레시피 가격 불러오기
        int rcprice;
        switch (shopingCursor)
        {
            case 0:
                rcprice = 100;
                break;
            case 1:
                rcprice = 200;
                break;
            case 2:
                rcprice = 300;
                break;
            case 3:
                rcprice = 400;
                break;
            default:
                rcprice = 0;
                break;
        }

        if (gold >= rcprice) {
            gold -= rcprice;
            goldText.text = gold.ToString();
            craftingLockPanels[shopingCursor].SetActive(false);
            soldOutImage.enabled = true;
        } else {
            noticeText.text = "골드가 부족합니다! (*요구 : " + rcprice.ToString() + ")";
            noticeAnim.ResetTrigger("notice");
            noticeAnim.SetTrigger("notice");
        }
        return;
    }
}
