using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chank : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _timeChankPopup = 0.5f;

    public void ActivateChank(Transform player)
    {
        StartCoroutine(ChankPopup(player));
    }

    private IEnumerator ChankPopup(Transform player)
    {
        var baseScale = transform.localScale;
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, baseScale, _timeChankPopup).setEaseOutBack();
        yield return new WaitForSeconds(_timeChankPopup + 0.01f); ;
        ActivateEnemy(player);
    }

    public void ActivateEnemy(Transform player)
    {
        if (_enemy)
        {
            _enemy.gameObject.SetActive(true);
            _enemy.SetTarget(player);
        }
    }
}
