using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIItemsList", menuName = "CreateUIItemsList", order = 3)]
public class UIItemsListScrObj : ScriptableObject
{
    public Item[] items;
}

[System.Serializable]
public class Item
{
    public MergeItemType itemType;
    public Sprite sprite;
}

public enum MergeItemType
{
    Bell,
    Coin,
    Heart,
    Gem
}