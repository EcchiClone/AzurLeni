using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public Dictionary<UnitSide, List<Transform>> EntityPosition = new Dictionary<UnitSide, List<Transform>>(); // 참조 타입 Transform
        
    public Transform SearchCloseEntity(Transform originT, UnitSide[] sides)
    {
        Transform targetT = null;
        float minDistance = float.MaxValue;
        foreach (UnitSide side in sides)
        {
            if (!EntityPosition.ContainsKey(side))
                continue;

            foreach (Transform t in EntityPosition[side])
            {
                float distance = Vector3.Distance(t.position, originT.position);
                if (distance < minDistance)
                {
                    targetT = t;
                    minDistance = distance;
                }
            }
        }
        return targetT;
    }

    public void AddEntityTransform(UnitSide _side, Transform _t)
    {
        if (!EntityPosition.ContainsKey(_side))
        {
            EntityPosition[_side] = new List<Transform>();
        }
        EntityPosition[_side].Add(_t);
    }

    public void ClearAll()
    {
        EntityPosition.Clear();
    }

}
