using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftRecipes", menuName = "CreateCraftRecipes", order = 4)]
public class CraftRecipes : ScriptableObject
{
    public Recipe[] recipes;
}

[System.Serializable]
public class Recipe
{
    public MergeItemType outputItem;
    public int outputValue = 1;
    public MergeItemType inputItem;
    public int inputPrice = 2;
}
