using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeCell : MonoBehaviour, IDropHandler
{
    private RectTransform _rectTransform;

    [SerializeField] private UIItem _currentItem;
       

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
       
    public void OnDrop(PointerEventData eventData)
    {
        

        //Debug.Log("It cell drop");
    }


}
