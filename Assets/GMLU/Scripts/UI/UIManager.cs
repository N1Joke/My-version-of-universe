using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private MergeWindow _mergeWindow;
    [SerializeField] private Canvas _canvas;

    [Header("Resource holder")]
    [SerializeField] private GameObject _tree;
    [SerializeField] private GameObject _rock;
    [SerializeField] private GameObject _board;
    [SerializeField] private GameObject _steel;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _blueCristal;
    [SerializeField] private GameObject _purpleCristal;
    [SerializeField] private GameObject _garbage;

    [Header("Count")]
    [SerializeField] private TextMeshProUGUI _treeCountText;
    [SerializeField] private TextMeshProUGUI _rockCountText;
    [SerializeField] private TextMeshProUGUI _boardCountText;
    [SerializeField] private TextMeshProUGUI _steelCountText;
    [SerializeField] private TextMeshProUGUI _coinCountText;
    [SerializeField] private TextMeshProUGUI _blueCristalText;
    [SerializeField] private TextMeshProUGUI _purpleCristalText;
    [SerializeField] private TextMeshProUGUI _garbageText;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _button.onClick.AddListener(OnClick);
        _mergeWindow.gameObject.SetActive(false);
    }

    private void OnClick()
    {
        DataManager.Instance.ChangeResourceCount(ResourceType.Garbage, 6);
        _mergeWindow.gameObject.SetActive(true);
    }

    public float GetCanvasScaleFactor()
    {
        return _canvas.scaleFactor;
    }

    public void SetResourceCount(ResourceType resourceType, int value)
    {
        switch (resourceType)
        {
            case ResourceType.Tree:
                _treeCountText.text = value.ToString();
                if (value == 0)
                    _tree.SetActive(false);
                else
                    _tree.SetActive(true);
                break;
            case ResourceType.Rock:
                _rockCountText.text = value.ToString();
                if (value == 0)
                    _rock.SetActive(false);
                else
                    _rock.SetActive(true);
                break;
            case ResourceType.Board:
                _boardCountText.text = value.ToString();
                if (value == 0)
                    _board.SetActive(false);
                else
                    _board.SetActive(true);
                break;
            case ResourceType.Steel:
                _steelCountText.text = value.ToString();
                if (value == 0)
                    _steel.SetActive(false);
                else
                    _steel.SetActive(true);
                break;
            case ResourceType.Coin:
                _coinCountText.text = value.ToString();
                if (value == 0)
                    _coin.SetActive(false);
                else
                    _coin.SetActive(true);
                break;
            case ResourceType.CrystalBlue:
                _blueCristalText.text = value.ToString();
                if (value == 0)
                    _blueCristal.SetActive(false);
                else
                    _blueCristal.SetActive(true);
                break;
            case ResourceType.CrystalPurple:
                _purpleCristalText.text = value.ToString();
                if (value == 0)
                    _purpleCristal.SetActive(false);
                else
                    _purpleCristal.SetActive(true);
                break;
            case ResourceType.Garbage:
                _garbageText.text = value.ToString();
                if (value == 0)
                    _garbage.SetActive(false);
                else
                    _garbage.SetActive(true);
                break;
        }
    }
}
