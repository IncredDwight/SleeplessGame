using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class UpdatePath : MonoBehaviour
{
    private AstarPath _astarPath;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AstarPath.active.Scan(AstarPath.active.data.gridGraph);
    }
}
