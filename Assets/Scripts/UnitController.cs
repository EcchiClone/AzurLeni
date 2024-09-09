using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum UnitSide
{
    User,
    Enemy,
    Neutral,
}

public enum UnitState
{
    Idle,
    Search,
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
    // UI 오브젝트 연결
    protected UIDocument uiDocument;

    // 유닛 정보


    // 유닛 상태 관리
    [SerializeField] protected UnitSide side;
    [SerializeField] protected UnitState state;
    // 유닛 움직임 관리

    public void ReturnToPool()
    {
        if (Game.Field.EntityPosition.ContainsKey(side))
        {
            if (Game.Field.EntityPosition[side].Contains(transform))
            {
                Game.Field.EntityPosition[side].Remove(transform);
            }
        }
        if (Game.Field.EntityPosition[side].Contains(transform))
        {
            Game.Field.EntityPosition[side].Remove(transform);
        }
        Game.Pool["Unit"].Release(gameObject);
    }

    void OnDestroy()
    {
        if(Game.Field.EntityPosition.ContainsKey(side))
        {
            if (Game.Field.EntityPosition[side].Contains(transform))
            {
                Game.Field.EntityPosition[side].Remove(transform);
            }
        }
    }

}
