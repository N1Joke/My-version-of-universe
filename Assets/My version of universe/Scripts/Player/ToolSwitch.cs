using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _axe;
    [SerializeField] private GameObject _pickaxe;
    [SerializeField] private GameObject _sword;

    public InstrumentalState instrumentalState { get; private set; }

    private void Start()
    {
        _axe.SetActive(false);
        _pickaxe.SetActive(false);
        _sword.SetActive(false);
    }

    public void SwitchState(InstrumentalState newInstrumentalState)
    {
        if (newInstrumentalState == instrumentalState)
            return;

        instrumentalState = newInstrumentalState;

        switch (instrumentalState)
        {
            case InstrumentalState.None:
                _axe.SetActive(false);
                _pickaxe.SetActive(false);
                _sword.SetActive(false);
                break;
            case InstrumentalState.Axe:
                _axe.SetActive(true);
                _pickaxe.SetActive(false);
                _sword.SetActive(false);
                break;
            case InstrumentalState.Pickaxe:
                _axe.SetActive(false);
                _pickaxe.SetActive(true);
                _sword.SetActive(false);
                break;
            case InstrumentalState.Sword:
                _axe.SetActive(false);
                _pickaxe.SetActive(false);
                _sword.SetActive(true);
                break;
        }
    }
}

public enum InstrumentalState
{
    None,
    Axe,
    Pickaxe,
    Sword
}
