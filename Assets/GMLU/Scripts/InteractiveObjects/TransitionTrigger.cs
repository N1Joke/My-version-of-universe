using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransitionTrigger : MonoBehaviour
{
    [SerializeField] private List<TransitionCost> _transitionPrice = new List<TransitionCost>();
    [SerializeField] private List<GameObject> _connectedObjects;
    [SerializeField] private List<GameObject> _connectedObjectsToDisable;
    [SerializeField] private TextMeshPro _resourcePrice;

    private void Start()
    {
        UpdateText();
        _resourcePrice.gameObject.SetActive(false);
    }

    private void UpdateText()
    {
        if (!_resourcePrice)
            return;

        _resourcePrice.text = "";
        //var LF : String = "/n";
        for (int i = 0; i < _transitionPrice.Count; i++)
        {
            _resourcePrice.text += _transitionPrice[i].resourceType.ToString() + ": " + _transitionPrice[i].price.ToString() + "\n";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var list = DataManager.Instance.SubtractCostOfTransition(_transitionPrice);

            UpdateText();

            if (list != null && list.Count == 0)
            {
                for (int i = 0; i < _connectedObjects.Count; i++)
                {
                    _connectedObjects[i].SetActive(true);
                    if (_connectedObjects[i].TryGetComponent(out Chank chank))
                        chank.ActivateChank(collision.transform);
                }

                for (int j = 0; j < _connectedObjectsToDisable.Count; j++)
                {
                    //_connectedObjectsToDisable[j].SetActive(false);
                    StartCoroutine(DisableWithAnimation(_connectedObjectsToDisable[j]));
                    if (_connectedObjectsToDisable[j].TryGetComponent(out Chank chank))
                        chank.ActivateEnemy(collision.transform);
                }

                Destroy(this.gameObject);
            }

            _resourcePrice.gameObject.SetActive(true);
        }
    }

    private IEnumerator DisableWithAnimation(GameObject gObj)
    {
        LeanTween.scale(gObj, Vector3.zero, 0.5f).setEaseInBack();
        yield return new WaitForSeconds(0.5f + 0.01f);
        gObj.SetActive(false);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _resourcePrice.gameObject.SetActive(false);
        }
    }  
}