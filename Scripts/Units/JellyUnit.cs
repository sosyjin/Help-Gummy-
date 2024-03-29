﻿using System.Collections;
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
    public float atkCoolTime;
    public float skillCoolTime;

    [Header("레이어 마스크")]
    public LayerMask searchLayer;

    [Header("etc")]
    public GameObject bullet;
    public GameObject gameManagerObject;
    GameManager gameManager;

    float bulletForce;
    float atkTimer = 0f;
    float skillTimer = 0f;

    ///<summary>  0 : normalJelly, 1 : giantJelly, 2 : burgerJelly, 3 : rangedJelly</summary>
    public int unitNumber;
    const int baseUnitNumber = 4;
    CandyUnit rayHitUnit;
    Animator animator;

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
            case "rangedJelly":
                unitNumber = 2;
                break;
            case "burgerJelly":
                unitNumber = 3;
                break;
            case "jellyBase":
                unitNumber = 4;
                break;
            default:
                unitNumber = -1; // UnitName Error
                break;
        }

        if(unitNumber != baseUnitNumber) {
            animator = GetComponentInChildren<Animator>();
        } else {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
    }

    // 유닛 사망 처리
    void UnitDie()
    {
        gameObject.SetActive(false);

        if (unitNumber == baseUnitNumber) {
            gameManager.GameSet(false);
            hp = 50;
        }
    }

    void FixedUpdate()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector3.right, atkDistance, searchLayer);

        if (raycast.collider != null) {
            if (Mathf.Abs(transform.position.x - raycast.collider.transform.position.x) <= atkDistance) { // 공격 사정거리 계산
                // ray 에 닿은 유닛 불러오기
                rayHitUnit = raycast.collider.gameObject.GetComponent<CandyUnit>();

                if (unitNumber != baseUnitNumber) {
                    animator.SetBool("IsMoving", false);
                }

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
                        // 밀치기 공격
                        if (skillTimer <= 0 && rayHitUnit.unitNumber != baseUnitNumber) {
                            rayHitUnit.hp -= skillDmg;
                            raycast.rigidbody.AddForceAtPosition(transform.right * bulletForce, raycast.collider.transform.position);
                            skillTimer = skillCoolTime;
                        } else if (atkTimer <= 0) { // 일반 공격
                            rayHitUnit.hp -= atkDmg;
                            atkTimer = atkCoolTime;
                        }
                        break;
                    case 2:
                        // 일반 공격
                        if (atkTimer <= 0) {
                            Instantiate(bullet, gameObject.transform.position, UnityEngine.Quaternion.identity);
                            atkTimer = atkCoolTime;
                        }
                        break;
                    case 3:
                        if (skillTimer <= 0 && rayHitUnit.unitNumber != baseUnitNumber) { // 섭취 공격
                            raycast.collider.gameObject.SetActive(false);
                            skillTimer = skillCoolTime;
                        } else if (atkTimer <= 0) { // 일반 공격
                            rayHitUnit.hp -= atkDmg;
                            atkTimer = atkCoolTime;
                        }
                        break;
                }
            } else { // 사정거리까지 이동
                transform.position += Time.deltaTime * moveSpeed * new Vector3(1, 0, 0);
            }
        } else { // 이동
            transform.position += Time.deltaTime * moveSpeed * new Vector3(1, 0, 0);
            if (unitNumber != baseUnitNumber) {
                animator.SetBool("IsMoving", true);
            }
        }

        atkTimer -= Time.deltaTime;
        skillTimer -= Time.deltaTime;

        // 사망 처리
        if (hp <= 0) {
            UnitDie();
        }
    }
}
