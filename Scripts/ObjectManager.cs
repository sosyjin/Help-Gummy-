using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("유닛 프리팹")]
    public GameObject bearJellyPrefab;
    public GameObject normalJellyPrefab;
    public GameObject longJellyPrefab;
    public GameObject archJellyPrefab;
    public GameObject burgerJellyPrefab;
    [Space]
    public GameObject aPrefab;
    public GameObject bPrefab;
    public GameObject cPrefab;
    public GameObject dPrefab;
    public GameObject ePrefab;

    [Header("오브젝트 풀")]
    public GameObject objectPool;
    [Range(30, 100)]
    public int poolSize;

    GameObject [] bearJellyPool;
    GameObject [] normalJellyPool;
    GameObject [] longJellyPool;
    GameObject [] archJellyPool;
    GameObject [] burgerJellyPool;

    GameObject [] aPool;
    GameObject [] bPool;
    GameObject [] cPool;
    GameObject [] dPool;
    GameObject [] ePool;

    [Header("크래프팅")]
    public Text sugarText;
    public Text juiceText;

    [Header("etc")]
    public GameObject gameManagerObject;
    public Text noticeText;
    GameManager gameManager;
    Animator noticeAnim;

    private void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();

        // 아군 유닛 풀
        bearJellyPool = new GameObject[poolSize];
        normalJellyPool = new GameObject[poolSize];
        longJellyPool = new GameObject[poolSize];
        archJellyPool = new GameObject[poolSize];
        burgerJellyPool = new GameObject[poolSize];

        // 적군 유닛 풀
        aPool = new GameObject[poolSize];
        bPool = new GameObject[poolSize];
        cPool = new GameObject[poolSize];
        dPool = new GameObject[poolSize];
        ePool = new GameObject[poolSize];

        // 풀 생성
        GeneratePool();
    }

    // 오브젝트 풀링
    void GeneratePool()
    {
        // UI 가림 방지
        SpriteRenderer spriteRenderer;

        for(int i = 0; i < poolSize; i++) {
            bearJellyPool[i] = Instantiate(bearJellyPrefab, new Vector2(-6.5f, bearJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            bearJellyPool[i].SetActive(false);
            bearJellyPool[i].transform.parent = objectPool.transform;
            spriteRenderer = bearJellyPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;

            normalJellyPool[i] = Instantiate(normalJellyPrefab, new Vector2(-6.5f, normalJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            normalJellyPool[i].SetActive(false);
            normalJellyPool[i].transform.parent = objectPool.transform;
            spriteRenderer = normalJellyPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;

            longJellyPool[i] = Instantiate(longJellyPrefab, new Vector2(-6.5f, longJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            longJellyPool[i].SetActive(false);
            longJellyPool[i].transform.parent = objectPool.transform;
            spriteRenderer = longJellyPool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;

            archJellyPool[i] = Instantiate(archJellyPrefab, new Vector2(-6.5f, archJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            archJellyPool[i].SetActive(false);
            archJellyPool[i].transform.parent = objectPool.transform;
            spriteRenderer = archJellyPool[i].gameObject.GetComponent<SpriteRenderer>();
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

            ePool[i] = Instantiate(ePrefab, new Vector2(6.5f, ePrefab.transform.position.y), UnityEngine.Quaternion.identity);
            ePool[i].SetActive(false);
            ePool[i].transform.parent = objectPool.transform;
            spriteRenderer = ePool[i].gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;
        }
    }
    GameObject [] targetPool;
    public void GenerateUnitObject(string objectName)
    {
        if (Time.timeScale == 0)
            return;
        switch(objectName) {
            case "bearJelly":
                targetPool = bearJellyPool;
                break;
            case "normalJelly":
                targetPool = normalJellyPool;
                break;
            case "longJelly":
                targetPool = longJellyPool;
                break;
            case "archJelly":
                targetPool = archJellyPool;
                break;
            case "burgerJelly":
                targetPool = burgerJellyPool;
                break;

            case "a":
                targetPool = aPool;
                break;
            case "b":
                targetPool = bPool;
                break;
            case "c":
                targetPool = cPool;
                break;
            case "d":
                targetPool = dPool;
                break;
            case "e":
                targetPool = ePool;
                break;
        }
        for (int i = 0; i < targetPool.Length; i++) {
            if (!targetPool[i].activeSelf) {
                // 재화 소모
                int sugarValue = int.Parse(sugarText.text);
                int juiceValue = int.Parse(juiceText.text);
                int demandGold = sugarValue * 100 + juiceValue * 100;
                // int demandSugar = ;
                if (gameManager.gold >= demandGold) {
                    gameManager.gold -= demandGold;
                    gameManager.goldText.text = gameManager.gold.ToString();
                    targetPool[i].SetActive(true);
                } else {
                    // 경고 텍스트
                    noticeText.text = "골드가 부족합니다! (*요구 : " + demandGold.ToString() + ")";
                    noticeAnim = noticeText.gameObject.GetComponent<Animator>();
                    noticeAnim.ResetTrigger("notice");
                    noticeAnim.SetTrigger("notice");
                    return;
                }

                // 크래프팅 설정 적용
                Unit unit = targetPool[i].gameObject.GetComponent<Unit>();
                unit.attack += juiceValue;
                unit.hp += sugarValue;
                unit.gameObject.transform.localScale = new Vector3(0.4f + sugarValue * 0.03f, 0.4f + sugarValue * 0.03f, 1);
                unit.gameObject.transform.position += new Vector3(0, sugarValue * 0.024f, 0);
                
                return;
            }
        }
        return;
    }

    // Generate Unit 설정
    public void SetGenerateUnit()
    {
        int craftingCursor = gameManager.craftingCursor;
        string objectName = "";

        switch(craftingCursor) {
            case 0:
                objectName = "bearJelly";
                break;
            case 1:
                objectName = "normalJelly";
                break;
            case 2:
                objectName = "longJelly";
                break;
            case 3:
                objectName = "archJelly";
                break;
            case 4:
                objectName = "burgerJelly";
                break;
        }

        GenerateUnitObject(objectName);
    }
}
