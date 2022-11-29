using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private MergeCell _parentCell;
    [SerializeField] private float _amimBackTime = 0.35f;
    [SerializeField] private UIItem _uIItem;

    private float _canvasScaleFactor;
    private RectTransform _rectTransform;
    private bool _canBeDraged = true;
    private bool _matches;

    public bool Matches => _matches;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();        
    }

    private void Start()
    {
        _canvasScaleFactor = UiService.Instance.GetCanvasScaleFactor();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvasScaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (!_canBeDraged)
            return;

        _canBeDraged = false;
        LeanTween.move(_rectTransform, Vector3.zero, _amimBackTime).setOnComplete(() => { _canBeDraged = true; });

        //Debug.Log("On end drag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.pointerDrag)
            return;

        var item = eventData.pointerDrag.GetComponent<UIItem>();
        if (_uIItem.MergeItem == item.MergeItem)
        {
            if (_uIItem != item)
            {                
                Destroy(eventData.pointerDrag);
                _uIItem.TransformItem();
            }
        }
    }
}
