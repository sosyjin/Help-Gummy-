using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAlgorithm : MonoBehaviour
{
    [Header("재화")]
    public int gold = 0;

    [Header("etc")]
    public float maxTimer = 0;
    public GameObject objectManagerObject;
    [System.NonSerialized]
    public int craftingCursor = 0;
    public int sugarValue = 0;
    public int juiceValue = 0;
    ObjectManager obejctManager;

    int sugar = 0;
    int juice = 0;
    float startTimer = 0;
    float currentTimer = 0;
    bool functionBump = false;

    private void Awake()
    {
        obejctManager = objectManagerObject.GetComponent<ObjectManager>();
        startTimer = Time.time;
    }

    private void Update()
    {
        CheckGoldTimer();
        CraftingTrigger();
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
    }

    void CraftingTrigger()
    {
        switch (gold) {
            case 500:
                if(!functionBump) {
                    if (RollHundredDice(30)) {
                        StartCoroutine(CraftUnit(30, 40, 60));
                    }
                    functionBump = true;
                }
                break;
            case 1100:
                if(!functionBump) {
                    if (RollHundredDice(30)) {
                        StartCoroutine(CraftUnit(35, 40, 60));
                    }
                    functionBump = true;
                }
                break;
            case 1500:
                if (!functionBump) {
                    if (RollHundredDice(30)) {
                        StartCoroutine(CraftUnit(40, 40, 60));
                    }
                    functionBump = true;
                }
                break;
            case 2100:
                if (!functionBump) {
                    StartCoroutine(CraftUnit(100, 50, 60));
                    functionBump = true;
                }
                break;
            default:
                functionBump = false;
                break;
        }

    }
    bool RollHundredDice(int successRate)
    {
        int random = Random.Range(1, 101);

        if(successRate <= random) {
            return true;
        } else {
            return false;
        }
    }

    int SetRandomUnitType()
    {
        int random = 0;

        while(true) {
            random = Random.Range(0, 4);

            if(gold >= random * 100) {
                gold -= random * 100;
                craftingCursor = random;
                return random;
            }
        }
    }

    // 코루틴으로 만들어서 시간차 조금씩 주기
    IEnumerator CraftUnit(int triggerSuccessRate, int craftingSuccessRate, int reinforceSuccessRate)
    {
        while(gold >= 400) {
            // 유닛 종류 설정
            SetRandomUnitType();

            if (RollHundredDice(triggerSuccessRate)) { // 트리거
                if (RollHundredDice(craftingSuccessRate)) { // 크래프팅 여부
                    while (RollHundredDice(reinforceSuccessRate) && gold >= 100) {
                        if (RollHundredDice(50)) {
                            sugarValue += 1;
                        } else {
                            juiceValue += 1;
                        }
                        gold -= 100;
                    }
                }

                // 유닛 생성
                obejctManager.SetGenerateUnit(false);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}