using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    private MergeItemType _mergeItem;

    public MergeItemType MergeItem => _mergeItem;

    private void Start()
    {
        int index = Random.Range(0, DataManager.Instance.ItemList.items.Length);
        _mergeItem = DataManager.Instance.ItemList.items[index].itemType;
        _image.sprite = DataManager.Instance.ItemList.items[index].sprite;
    }

    public void TransformItem()
    {
        var craftList = DataManager.Instance.CraftRecipesList;
        for (int i = 0; i < craftList.recipes.Length; i++)
        {
            if (_mergeItem == craftList.recipes[i].inputItem)
            {
                _mergeItem = craftList.recipes[i].outputItem;
                
                for (int j = 0; j < DataManager.Instance.ItemList.items.Length; j++)
                {
                    if (DataManager.Instance.ItemList.items[j].itemType == _mergeItem)
                    {
                        _image.sprite = DataManager.Instance.ItemList.items[j].sprite;
                        return;
                    }
                }
            } 
        }
        
    }
}
