using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    private List<int> modifiers = new List<int>();

    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public int GetBaseValue()
    {
        return baseValue;
    }

    public int GetModifiersValue()
    {
        int finalValue = 0;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }

    public void AddBaseValue(int addValue)
    {
        baseValue += addValue;
    }

    public void SetBaseValue(int addValue)
    {
        baseValue = addValue;
    }

    public void RemoveBaseValue(int removeValue)
    {
        baseValue -= removeValue;
    }

    public void NewBaseValue(int newValue)
    {
        baseValue = newValue;
    }

}