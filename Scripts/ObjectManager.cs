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

    private void Awake()
    {
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

        GeneratePool();
    }

    void GeneratePool()
    {
        for(int i = 0; i < poolSize; i++) {
            bearJellyPool[i] = Instantiate(bearJellyPrefab, new Vector2(-6.5f, bearJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            bearJellyPool[i].SetActive(false);
            bearJellyPool[i].transform.parent = objectPool.transform;

            normalJellyPool[i] = Instantiate(normalJellyPrefab, new Vector2(-6.5f, normalJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            normalJellyPool[i].SetActive(false);
            normalJellyPool[i].transform.parent = objectPool.transform;

            longJellyPool[i] = Instantiate(longJellyPrefab, new Vector2(-6.5f, longJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            longJellyPool[i].SetActive(false);
            longJellyPool[i].transform.parent = objectPool.transform;

            archJellyPool[i] = Instantiate(archJellyPrefab, new Vector2(-6.5f, archJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            archJellyPool[i].SetActive(false);
            archJellyPool[i].transform.parent = objectPool.transform;

            burgerJellyPool[i] = Instantiate(burgerJellyPrefab, new Vector2(-6.5f, burgerJellyPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            burgerJellyPool[i].SetActive(false);
            burgerJellyPool[i].transform.parent = objectPool.transform;

            // ================================================================================================================ //

            aPool[i] = Instantiate(aPrefab, new Vector2(6.5f, aPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            aPool[i].SetActive(false);
            aPool[i].transform.parent = objectPool.transform;

            bPool[i] = Instantiate(bPrefab, new Vector2(6.5f, bPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            bPool[i].SetActive(false);
            bPool[i].transform.parent = objectPool.transform;

            cPool[i] = Instantiate(cPrefab, new Vector2(6.5f, cPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            cPool[i].SetActive(false);
            cPool[i].transform.parent = objectPool.transform;

            dPool[i] = Instantiate(dPrefab, new Vector2(6.5f, dPrefab.transform.position.y), UnityEngine.Quaternion.identity);
            dPool[i].SetActive(false);
            dPool[i].transform.parent = objectPool.transform;

            ePool[i] = Instantiate(ePrefab, new Vector2(6.5f, ePrefab.transform.position.y), UnityEngine.Quaternion.identity);
            ePool[i].SetActive(false);
            ePool[i].transform.parent = objectPool.transform;
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
                targetPool[i].SetActive(true);
                return;
            }
        }
        return;
    }
}
