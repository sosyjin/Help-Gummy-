using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("유닛 프리팹")]
    public GameObject giantJellyPrefab;
    public GameObject normalJellyPrefab;
    public GameObject rangedJellyPrefab;
    public GameObject burgerJellyPrefab;
    [Space]
    public GameObject aPrefab;
    public GameObject bPrefab;
    public GameObject cPrefab;
    public GameObject dPrefab;

    [Header("오브젝트 풀")]
    public GameObject objectPool;
    [Range(30, 100)]
    public int poolSize;

    GameObject [] giantJellyPool;
    GameObject [] normalJellyPool;
    GameObject [] rangedJellyPool;
    GameObject [] burgerJellyPool;

    GameObject [] aPool;
    GameObject [] bPool;
    GameObject [] cPool;
    GameObject [] dPool;

    [Header("크래프팅")]
    public Text sugarText;
    public Text juiceText;

    [Header("etc")]
    public GameObject gameManagerObject;
    public Text noticeText;
    public GameObject[] panels;
    public GameObject enemyAlgorithmObejct;
    GameManager gameManager;
    EnemyAlgorithm enemyAlgorithm;
    Animator noticeAnim;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        enemyAlgorithm = enemyAlgorithmObejct.GetComponent<EnemyAlgorithm>();
        noticeAnim = noticeText.GetComponent<Animator>();

        // 아군 유닛 풀
        giantJellyPool = new GameObject[poolSize];
        normalJellyPool = new GameObject[poolSize];
        rangedJellyPool = new GameObject[poolSize];
        burgerJellyPool = new GameObject[poolSize];

        // 적군 유닛 풀
        aPool = new GameObject[poolSize];
        bPool = new GameObject[poolSize];
        cPool = new GameObject[poolSize];
        dPool = new GameObject[poolSize];

        // 풀 생성
        GeneratePool();
    }

    // 오브젝트 풀링
    void GeneratePool()
    {
        // UI 가림 방지
        SpriteRenderer spriteRenderer;

        for(int i = 0; i < poolSize; i++) {
            giantJellyPool[i] = Instantiate(giantJellyPrefab, new Vector2(-6.5f, giantJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            giantJellyPool[i].SetActive(false);
            giantJellyPool[i].transform.parent = objectPool.transform;
            spriteRenderer = giantJellyPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;

            normalJellyPool[i] = Instantiate(normalJellyPrefab, new Vector2(-6.5f, normalJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            normalJellyPool[i].SetActive(false);
            normalJellyPool[i].transform.parent = objectPool.transform;
            spriteRenderer = normalJellyPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;

            rangedJellyPool[i] = Instantiate(rangedJellyPrefab, new Vector2(-6.5f, rangedJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            rangedJellyPool[i].SetActive(false);
            rangedJellyPool[i].transform.parent = objectPool.transform;
            spriteRenderer = rangedJellyPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;

            burgerJellyPool[i] = Instantiate(burgerJellyPrefab, new Vector2(-6.5f, burgerJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            burgerJellyPool[i].SetActive(false);
            burgerJellyPool[i].transform.parent = objectPool.transform;
            spriteRenderer = burgerJellyPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;

            // ================================================================================================================ //

            aPool[i] = Instantiate(aPrefab, new Vector2(6.5f, aPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            aPool[i].SetActive(false);
            aPool[i].transform.parent = objectPool.transform;
            spriteRenderer = aPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;

            bPool[i] = Instantiate(bPrefab, new Vector2(6.5f, bPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            bPool[i].SetActive(false);
            bPool[i].transform.parent = objectPool.transform;
            spriteRenderer = bPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;

            cPool[i] = Instantiate(cPrefab, new Vector2(6.5f, cPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            cPool[i].SetActive(false);
            cPool[i].transform.parent = objectPool.transform;
            spriteRenderer = cPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;

            dPool[i] = Instantiate(dPrefab, new Vector2(6.5f, dPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            dPool[i].SetActive(false);
            dPool[i].transform.parent = objectPool.transform;
            spriteRenderer = dPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;
        }
    }
    GameObject [] targetPool;
    GameObject targetPrefab;
    JellyUnit targetPrefabJellyUnit;
    CandyUnit targetPrefabCandyUnit;
    public void GenerateUnitObject(string objectName)
    {
        if (Time.timeScale == 0)
            return;

        switch (objectName) {
            case "giantJelly":
                if (panels[0].activeSelf) { // 레시피 미구매 시
                    noticeText.text = "젤리를 생산하려면 레시피를 구매해야 합니다. (*상점)";
                    noticeAnim.speed = 1 / Time.timeScale;
                    noticeAnim.ResetTrigger("notice");
                    noticeAnim.SetTrigger("notice");
                    return;
                }
                targetPool = giantJellyPool;
                targetPrefab = giantJellyPrefab;
                targetPrefabJellyUnit = giantJellyPrefab.GetComponent<JellyUnit>();
                break;
            case "normalJelly":
                if (panels[1].activeSelf) { // 레시피 미구매 시
                    noticeText.text = "젤리를 생산하려면 레시피를 구매해야 합니다. (*상점)";
                    noticeAnim.speed = 1 / Time.timeScale;
                    noticeAnim.ResetTrigger("notice");
                    noticeAnim.SetTrigger("notice");
                    return;
                }
                targetPool = normalJellyPool;
                targetPrefab = normalJellyPrefab;
                targetPrefabJellyUnit = normalJellyPrefab.GetComponent<JellyUnit>();
                break;
            case "rangedJelly":
                if (panels[2].activeSelf) { // 레시피 미구매 시
                    noticeText.text = "젤리를 생산하려면 레시피를 구매해야 합니다. (*상점)";
                    noticeAnim.speed = 1 / Time.timeScale;
                    noticeAnim.ResetTrigger("notice");
                    noticeAnim.SetTrigger("notice");
                    return;
                }
                targetPool = rangedJellyPool;
                targetPrefab = rangedJellyPrefab;
                targetPrefabJellyUnit = rangedJellyPrefab.GetComponent<JellyUnit>();
                break;
            case "burgerJelly":
                if (panels[3].activeSelf) { // 레시피 미구매 시
                    noticeText.text = "젤리를 생산하려면 레시피를 구매해야 합니다. (*상점)";
                    noticeAnim.speed = 1 / Time.timeScale;
                    noticeAnim.ResetTrigger("notice");
                    noticeAnim.SetTrigger("notice");
                    return;
                }
                targetPool = burgerJellyPool;
                targetPrefab = burgerJellyPrefab;
                targetPrefabJellyUnit = burgerJellyPrefab.GetComponent<JellyUnit>();
                break;

            case "caneCandy":
                targetPool = aPool;
                targetPrefab = aPrefab;
                targetPrefabCandyUnit = aPrefab.GetComponent<CandyUnit>();
                break;
            case "normalCandy":
                targetPool = bPool;
                targetPrefab = bPrefab;
                targetPrefabCandyUnit = bPrefab.GetComponent<CandyUnit>();
                break;
            case "rockCandy":
                targetPool = cPool;
                targetPrefab = cPrefab;
                targetPrefabCandyUnit = cPrefab.GetComponent<CandyUnit>();
                break;
            case "rangedCandy":
                targetPool = dPool;
                targetPrefab = dPrefab;
                targetPrefabCandyUnit = dPrefab.GetComponent<CandyUnit>();
                break;
            default:
                Debug.Log("Error!\nObejctName : " + objectName);
                return;
        }
        for (int i = 0; i < targetPool.Length; i++) {
            if (!targetPool[i].activeSelf) {
                if(objectName.Contains("Jelly")) {
                    // 재화 계산
                    int sugarValue = int.Parse(sugarText.text);
                    int juiceValue = int.Parse(juiceText.text);
                    int demandGold = gameManager.craftingCursor * 100 + sugarValue * 100 + juiceValue * 100;
                    // int demandSugar = ;
                    if (gameManager.gold >= demandGold) {
                        // 재화 소모
                        gameManager.gold -= demandGold;
                        gameManager.goldText.text = gameManager.gold.ToString();

                        // 오브젝트 초기화
                        targetPool[i].transform.position = targetPrefab.transform.position;
                        JellyUnit targetUnit = targetPool[i].GetComponent<JellyUnit>();
                        targetUnit.hp = targetPrefabJellyUnit.hp;
                        targetUnit.moveSpeed = targetPrefabJellyUnit.moveSpeed;
                        targetUnit.atkDmg = targetPrefabJellyUnit.atkDmg;

                        // 오브젝트 활성화
                        targetPool[i].SetActive(true);
                    } else {
                        // 재화 부족 알림
                        noticeText.text = "골드가 부족합니다! (*요구 : " + demandGold.ToString() + ")";
                        noticeAnim.speed = 1 / Time.timeScale;
                        noticeAnim.ResetTrigger("notice");
                        noticeAnim.SetTrigger("notice");
                        return;
                    }

                    // 크래프팅 설정 적용
                    JellyUnit jellyUnit = targetPool[i].gameObject.GetComponent<JellyUnit>();
                    jellyUnit.atkDmg += juiceValue;
                    jellyUnit.hp += sugarValue;

                    // 유닛 생성 위치 조정
                    jellyUnit.gameObject.transform.localScale = new Vector2(0.15f + sugarValue * 0.01f, 0.15f + sugarValue * 0.01f);
                    jellyUnit.gameObject.transform.position = new Vector3(-7.5f, sugarValue * 0.05f - 1.2f, i);
                } else {
                    // 오브젝트 초기화
                    targetPool[i].transform.position = targetPrefab.transform.position;
                    CandyUnit targetUnit = targetPool[i].GetComponent<CandyUnit>();
                    targetUnit.hp = targetPrefabCandyUnit.hp;
                    targetUnit.moveSpeed = targetPrefabCandyUnit.moveSpeed;
                    targetUnit.atkDmg = targetPrefabCandyUnit.atkDmg;

                    // 크래프팅 적용
                    targetUnit.atkDmg += enemyAlgorithm.juiceValue;
                    targetUnit.hp += enemyAlgorithm.sugarValue;

                    // 유닛 생성 위치 조정
                    targetUnit.gameObject.transform.localScale = new Vector2(0.15f + enemyAlgorithm.sugarValue * 0.01f, 0.15f + enemyAlgorithm.sugarValue * 0.01f);
                    targetUnit.gameObject.transform.position = new Vector3(7.5f, enemyAlgorithm.sugarValue * 0.05f - 1.2f, -i);

                    // 오브젝트 활성화
                    targetPool[i].SetActive(true);
                }
                
                return;
            }
        }
        return;
    }

    // Generate Unit 설정
    public void SetGenerateUnit(bool isUnitTypeJelly)
    {
        int craftingCursor = isUnitTypeJelly ? gameManager.craftingCursor : enemyAlgorithm.craftingCursor + 4;
        string objectName = "";

        switch(craftingCursor) {
            case 0:
                objectName = "giantJelly";
                break;
            case 1:
                objectName = "normalJelly";
                break;
            case 2:
                objectName = "rangedJelly";
                break;
            case 3:
                objectName = "burgerJelly";
                break;

            case 4:
                objectName = "caneCandy";
                break;
            case 5:
                objectName = "normalCandy";
                break;
            case 6:
                objectName = "rockCandy";
                break;
            case 7:
                objectName = "rangedCandy";
                break;
        }

        GenerateUnitObject(objectName);
    }
}
