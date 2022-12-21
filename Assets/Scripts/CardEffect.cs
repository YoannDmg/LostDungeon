using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect
{

    [SerializeField] private string name;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    [SerializeField] private string description;
    public string Description
    {
        get
        {
            return description;
        }
        set
        {
            description = value;
        }
    }

    [SerializeField]
    public GameObject Effect;



    [SerializeField] private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    [SerializeField] private int block;
    public int Block
    {
        get
        {
            return block;
        }
        set
        {
            block = value;
        }
    }

    [SerializeField] private int heal;
    public int Heal
    {
        get
        {
            return heal;
        }
        set
        {
            heal = value;
        }
    }

    [SerializeField] private int energyCost;
    public int EnergyCost
    {
        get
        {
            return energyCost;
        }
        set
        {
            energyCost = value;
        }
    }
    [SerializeField] private int energyGain;
    public int EnergyGain
    {
        get
        {
            return energyGain;
        }
        set
        {
            energyGain = value;
        }
    }

}
