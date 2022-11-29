using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshManager : MonoBehaviour
{
    private NavMeshSurface _navMesh;

    public static NavMeshManager Instance;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshSurface>();

        if (Instance == null)
            Instance = this;
    }

    public void UpdateNavMeshSurface()
    {
        _navMesh.BuildNavMesh();
    }
}
