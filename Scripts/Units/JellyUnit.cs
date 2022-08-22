using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyUnit : MonoBehaviour
{
    [Header("유닛명")]
    public string unitName;

    [Header("유닛 스테이터스")]
    public float atkDistance;
    public float moveSpeed;
    public int hp;
    public int atkDmg;
    public int skillDmg;

    [Header("쿨타임")]
    readonly public float atkCoolTime;
    readonly public float skillCoolTime;

    [Header("레이어 마스크")]
    public LayerMask isLayer;

    [Header("etc")]
    public GameObject bullet;

    float knockDis;
    float atkTimer;
    float skillTimer;

    ///<summary>  0 : normalJelly, 1 : giantJelly, 2 : burgerJelly, 3 : rangedJelly</summary>
    int unitNumber;

    CandyUnit rayHitUnit;

    private void Awake()
    {
        // 유닛명 상수화 (*비교 연산 속도 개선)
        switch(unitName)
        {
            case "normalJelly":
                unitNumber = 0;
                break;
            case "giantJelly":
                unitNumber = 1;
                break;
            case "burgerJelly":
                unitNumber = 2;
                break;
            case "rangedJelly":
                unitNumber = 3;
                break;
        }
    }

    // 유닛 사망 처리
    void UnitDie()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector3.right * 1, atkDistance, isLayer);
        if (raycast.collider != null) {
            switch(raycast.collider.tag)
            {
                case "Candy": // 공격
                    if (Mathf.Abs(transform.position.x - raycast.collider.transform.position.x) <= atkDistance) { // 공격 사정거리 계산
                        // ray 에 닿은 유닛 불러오기
                        rayHitUnit = raycast.collider.gameObject.GetComponent<CandyUnit>();

                        // 유닛별 공격 로직
                        switch (unitNumber)
                        {
                            case 0:
                                // 일반 공격
                                if (atkTimer <= 0) {
                                    rayHitUnit.hp -= atkDmg;
                                    atkTimer = atkCoolTime;
                                }
                                break;
                            case 1:
                                // 일반 공격
                                if (atkTimer <= 0) {
                                    rayHitUnit.hp -= atkDmg;
                                    atkTimer = atkCoolTime;
                                }
                                // 밀치기 공격
                                if (skillTimer <= 0) {
                                    rayHitUnit.hp -= skillDmg;
                                    raycast.rigidbody.AddForceAtPosition(transform.right * knockDis, raycast.collider.transform.position);
                                    skillTimer = skillCoolTime;
                                }
                                break;
                            case 2:
                                // 일반 공격
                                if (atkTimer <= 0) {
                                    rayHitUnit.hp -= atkDmg;
                                    atkTimer = atkCoolTime;
                                }
                                // 섭취 공격
                                if (skillTimer <= 0) {
                                    Destroy(raycast.collider);
                                    skillTimer = skillCoolTime;
                                }
                                break;
                            case 3:
                                // 일반 공격
                                if (atkTimer <= 0) {
                                    Instantiate(bullet, gameObject.transform.position, UnityEngine.Quaternion.identity);
                                    atkTimer = atkCoolTime;
                                }
                                break;
                        }
                    }
                    break;
                case "Jelly": // 이동
                    transform.position += Time.deltaTime * moveSpeed * new Vector3(1, 0, 0);
                    break;
            }
                
            
        } else { // 이동
            transform.position += Time.deltaTime * moveSpeed * new Vector3(1, 0, 0);
        }

        atkTimer -= Time.deltaTime;
        skillTimer -= Time.deltaTime;

        // 사망 처리
        if (hp <= 0) {
            UnitDie();
        }
    }
}
