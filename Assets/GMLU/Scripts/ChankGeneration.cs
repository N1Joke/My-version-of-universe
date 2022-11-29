using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ChankGeneration : MonoBehaviour
{
    [SerializeField] private Transform _startChank;
    [SerializeField] private int _levelWidth = 8;
    [SerializeField] private int _levelLength = 9;
    [SerializeField] private bool _generateLevel;
    [SerializeField] private GameObject _chankPrefab;
    [SerializeField] private float _chankStepLength = 7.625f;
    [SerializeField] private float _chankStepWidth = 13.125f;

    private void Update()
    {
        if (_generateLevel)
        {
            _generateLevel = false;
            GenerateLevel();
        }
    }

    private void GenerateLevel()
    {
        Vector3 currentChankPos = _startChank.position;

        bool upFlag = true;

        int chankCount = 0;

        for (int i = 0; i < _levelLength; i++)
        {
            for (int j = 0; j < _levelWidth; j++)
            {
                if (upFlag)
                {
                    currentChankPos.z += _chankStepLength;
                    upFlag = false;
                }
                else
                {
                    currentChankPos.z -= _chankStepLength;
                    upFlag = true;
                }

                currentChankPos.x += _chankStepWidth;

                var chank = Instantiate(_chankPrefab);
                chankCount++;
                chank.name = "Chank " + chankCount.ToString();                
                chank.transform.position = currentChankPos;
                chank.transform.SetParent(transform);
            }

            currentChankPos.z += _chankStepLength * 2f;

            currentChankPos.x = 0f;
        }
    }
}
