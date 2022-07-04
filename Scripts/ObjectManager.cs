using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        // 아군 유닛 풀
        bearJellyPool = new GameObject[30];
        normalJellyPool = new GameObject[30];
        longJellyPool = new GameObject[30];
        archJellyPool = new GameObject[30];
        burgerJellyPool = new GameObject[30];

        // 적군 유닛 풀
        aPool = new GameObject[30];
        bPool = new GameObject[30];
        cPool = new GameObject[30];
        dPool = new GameObject[30];
        ePool = new GameObject[30];

        GeneratePool();
    }

    void GeneratePool()
    {
        for(int i = 0; i < bearJellyPool.Length; i++) {
            bearJellyPool[i] = Instantiate(bearJellyPrefab);
            bearJellyPool[i].SetActive(false);
        }
        for (int i = 0; i < normalJellyPool.Length; i++) {
            normalJellyPool[i] = Instantiate(normalJellyPrefab);
            normalJellyPool[i].SetActive(false);
        }
        for (int i = 0; i < bearJellyPool.Length; i++) {
            longJellyPool[i] = Instantiate(longJellyPrefab);
            longJellyPool[i].SetActive(false);
        }
        for (int i = 0; i < bearJellyPool.Length; i++) {
            archJellyPool[i] = Instantiate(archJellyPrefab);
            archJellyPool[i].SetActive(false);
        }
        for (int i = 0; i < bearJellyPool.Length; i++) {
            burgerJellyPool[i] = Instantiate(burgerJellyPrefab);
            burgerJellyPool[i].SetActive(false);
        }

        for (int i = 0; i < aPool.Length; i++) {
            aPool[i] = Instantiate(aPrefab);
            aPool[i].SetActive(false);
        }
        for (int i = 0; i < bPool.Length; i++) {
            bPool[i] = Instantiate(bPrefab);
            bPool[i].SetActive(false);
        }
        for (int i = 0; i < cPool.Length; i++) {
            cPool[i] = Instantiate(cPrefab);
            cPool[i].SetActive(false);
        }
        for (int i = 0; i < dPool.Length; i++) {
            dPool[i] = Instantiate(dPrefab);
            dPool[i].SetActive(false);
        }
        for (int i = 0; i < ePool.Length; i++) {
            ePool[i] = Instantiate(ePrefab);
            ePool[i].SetActive(false);
        }
    }

    GameObject [] targetPool;
    public GameObject GenerateUnitObject(string objectName)
    {
        switch(objectName)
        {
            case "bearJelly":
                targetPool = bearJellyPool;
                break;
            case "noramlJelly":
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
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
}
