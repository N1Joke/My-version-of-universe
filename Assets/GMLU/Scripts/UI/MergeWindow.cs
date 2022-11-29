using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeWindow : Window
{
    [SerializeField] private List<MergeRow> _mergeColumns;

    protected override void CloseWindow()
    {
        base.CloseWindow();


    }
}


