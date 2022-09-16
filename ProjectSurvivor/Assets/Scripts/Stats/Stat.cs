using System;
using UnityEngine;

[Serializable]
public class Stat
{
    public StatConfigureSO statConfigure;
    public int value;

    public virtual void Upgrade(int amount) 
    {
        value += amount;
        Debug.Log(value);
    }
}
