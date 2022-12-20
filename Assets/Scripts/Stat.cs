using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat
{
    [SerializeField] private int maxHealth;
    public int MaxHealth{
        get{
            return maxHealth;
        }
        set{
            maxHealth = value;
        }
    }
    [SerializeField] private int health;
    public int Health {
        get{
            return health;
        }
        set{
            if (value <= 0)
            {
                health = 0;
            }
            else if (value >= maxHealth)
            {
                health = maxHealth;
            }
            else{
                health = value;
            }
        }
    }
    [SerializeField] private int strength;
    public int Strength{
        get{
            return strength;
        }
        set{
            strength = value;
        }
    }
    [SerializeField] private int startCardInHand;
    public int StartCardInHand
    {
        get{
            return startCardInHand;
        }
        set{
            startCardInHand = value;
        }
    }


}
