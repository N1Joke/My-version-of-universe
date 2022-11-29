using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData
{
    public int treeCout;
    public int rockCount;
    public int boardCount;
    public int steelCount;
    public int coinCount;
    public int blueCrystalCount;
    public int purpleCrystalCount;
    public int garbageCount;
}

public class DataManager : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private UIItemsListScrObj _itemList;
    [SerializeField] private CraftRecipes _craftRecipes;

    private PlayerData _playerData;

    public UnityAction<int> OnTreeCountChanged;

    public static DataManager Instance;

    public UIItemsListScrObj ItemList => _itemList;
    public CraftRecipes CraftRecipesList => _craftRecipes;

    private void Awake()
    {
        _playerData = new PlayerData();
        Instance = this;
    }

    public List<TransitionCost> SubtractCostOfTransition(List<TransitionCost> transitionCost)
    {
        if (transitionCost.Count == 0)
        {
            Debug.Log("Empty transition cost");
            return null;
        }

        for (int i = transitionCost.Count - 1; i >= 0; i--)
        {
            switch (transitionCost[i].resourceType)
            {
                //Extractables
                case ResourceType.Tree:
                    if (_playerData.treeCout >= transitionCost[i].price)
                    {
                        _playerData.treeCout -= transitionCost[i].price;
                        transitionCost[i].price = 0;
                        transitionCost.Remove(transitionCost[i]);
                    }
                    else
                    {
                        transitionCost[i].price -= _playerData.treeCout;
                        _playerData.treeCout = 0;
                    }
                    _uIManager.SetResourceCount(ResourceType.Tree, _playerData.treeCout);
                    break;
                case ResourceType.Rock:
                    if (_playerData.rockCount >= transitionCost[i].price)
                    {
                        _playerData.rockCount -= transitionCost[i].price;
                        transitionCost[i].price = 0;
                        transitionCost.Remove(transitionCost[i]);
                    }
                    else
                    {
                        transitionCost[i].price -= _playerData.rockCount;
                        _playerData.rockCount = 0;
                    }
                    _uIManager.SetResourceCount(ResourceType.Rock, _playerData.rockCount);
                    break;
                case ResourceType.CrystalBlue:
                    if (_playerData.blueCrystalCount >= transitionCost[i].price)
                    {
                        _playerData.blueCrystalCount -= transitionCost[i].price;
                        transitionCost[i].price = 0;
                        transitionCost.Remove(transitionCost[i]);
                    }
                    else
                    {
                        transitionCost[i].price -= _playerData.blueCrystalCount;
                        _playerData.blueCrystalCount = 0;
                    }
                    _uIManager.SetResourceCount(ResourceType.CrystalBlue, _playerData.blueCrystalCount);
                    break;
                case ResourceType.CrystalPurple:
                    if (_playerData.purpleCrystalCount >= transitionCost[i].price)
                    {
                        _playerData.purpleCrystalCount -= transitionCost[i].price;
                        transitionCost[i].price = 0;
                        transitionCost.Remove(transitionCost[i]);
                    }
                    else
                    {
                        transitionCost[i].price -= _playerData.purpleCrystalCount;
                        _playerData.purpleCrystalCount = 0;
                    }
                    _uIManager.SetResourceCount(ResourceType.CrystalPurple, _playerData.purpleCrystalCount);
                    break;
                //Producebles
                case ResourceType.Board:
                    if (_playerData.boardCount >= transitionCost[i].price)
                    {
                        _playerData.boardCount -= transitionCost[i].price;
                        transitionCost[i].price = 0;
                        transitionCost.Remove(transitionCost[i]);
                    }
                    else
                    {
                        transitionCost[i].price -= _playerData.boardCount;
                        _playerData.boardCount = 0;
                    }
                    _uIManager.SetResourceCount(ResourceType.Board, _playerData.boardCount);
                    break;
                case ResourceType.Steel:
                    if (_playerData.steelCount >= transitionCost[i].price)
                    {
                        _playerData.steelCount -= transitionCost[i].price;
                        transitionCost[i].price = 0;
                        transitionCost.Remove(transitionCost[i]);
                    }
                    else
                    {
                        transitionCost[i].price -= _playerData.steelCount;
                        _playerData.steelCount = 0;
                    }
                    _uIManager.SetResourceCount(ResourceType.Steel, _playerData.steelCount);
                    break;
                case ResourceType.Coin:
                    if (_playerData.coinCount >= transitionCost[i].price)
                    {
                        _playerData.coinCount -= transitionCost[i].price;
                        transitionCost[i].price = 0;
                        transitionCost.Remove(transitionCost[i]);
                    }
                    else
                    {
                        transitionCost[i].price -= _playerData.coinCount;
                        _playerData.coinCount = 0;
                    }
                    _uIManager.SetResourceCount(ResourceType.Coin, _playerData.coinCount);
                    break;
                default: 
                    return null;
            }
        }

        return transitionCost;
    }
    
    public bool ChangeResourceCount(ResourceType resourceType, int value)
    {
        switch (resourceType)
        {
            //Extractables
            case ResourceType.Tree:
                if (value < 0 && Mathf.Abs(value) > _playerData.treeCout)
                    return false;
                _playerData.treeCout += value;
                _uIManager.SetResourceCount(resourceType, _playerData.treeCout);
                break;
            case ResourceType.Rock:
                if (value < 0 && Mathf.Abs(value) > _playerData.rockCount)
                    return false;
                _playerData.rockCount += value;
                _uIManager.SetResourceCount(resourceType, _playerData.rockCount);
                break;
            case ResourceType.CrystalBlue:
                if (value < 0 && Mathf.Abs(value) > _playerData.blueCrystalCount)
                    return false;
                _playerData.blueCrystalCount += value;
                _uIManager.SetResourceCount(resourceType, _playerData.blueCrystalCount);
                break;
            case ResourceType.CrystalPurple:
                if (value < 0 && Mathf.Abs(value) > _playerData.purpleCrystalCount)
                    return false;
                _playerData.purpleCrystalCount += value;
                _uIManager.SetResourceCount(resourceType, _playerData.purpleCrystalCount);
                break;
            case ResourceType.Garbage:
                if (value < 0 && Mathf.Abs(value) > _playerData.garbageCount)
                    return false;
                _playerData.garbageCount += value;
                _uIManager.SetResourceCount(resourceType, _playerData.garbageCount);
                break;
            //Producebles
            case ResourceType.Board:
                if (value < 0 && Mathf.Abs(value) > _playerData.boardCount)
                    return false;
                _playerData.boardCount += value;
                _uIManager.SetResourceCount(resourceType, _playerData.boardCount);
                break;
            case ResourceType.Steel:
                if (value < 0 && Mathf.Abs(value) > _playerData.steelCount)
                    return false;
                _playerData.steelCount += value;
                _uIManager.SetResourceCount(resourceType, _playerData.steelCount);
                break;
            case ResourceType.Coin:
                if (value < 0 && Mathf.Abs(value) > _playerData.coinCount)
                    return false;
                _playerData.coinCount += value;
                _uIManager.SetResourceCount(resourceType, _playerData.coinCount);
                break;
            default:
                return false;               
        }

        return true;
    }
}

public enum ResourceType
{
    Tree,
    Rock,
    Board,
    Steel,
    Coin,
    CrystalBlue,
    CrystalPurple,
    Garbage
}
