using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("능력치")]
    public int attack;
    public int shield;
    public int speed;
    public int hp;

    private void Update()
    {
        gameObject.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
    }
}
