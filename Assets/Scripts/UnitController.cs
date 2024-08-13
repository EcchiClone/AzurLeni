using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitSide
{
    User,
    Enemy,
    Neutral,
}

public enum UnitState
{
    Idle,
    Patrol,
    Move,
    Attack,
    Defend,
    Dead,
    Stunned,
    Flee,
    Interacting,
}

public class UnitController : MonoBehaviour
{
    // 유닛 관련 오브젝트 연결
    [SerializeField] private GameObject DisplayHp;
    [SerializeField] private GameObject DisplayName;
    // 유닛 정보 가져오기

    // 유닛 상태 관리
    // 유닛 움직임 관리
    // 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
