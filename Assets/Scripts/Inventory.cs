using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int metal;
    [SerializeField] int greenGoo;
    [SerializeField] int alienMeat;
    // Start is called before the first frame update
    void Start()
    {
        metal = 0;
        greenGoo = 0;
        alienMeat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void addMetal(int amount)
    {
        metal += amount;
    }

    public void addGreenGoo(int amount)
    {
        greenGoo += amount;
    }

    public void addAlienMeat(int amount)
    {
        alienMeat += amount;
    }

    public bool removeMetal(int amount)
    {
        if (metal - amount >= 0)
        {
            metal -= amount;
            return true;
        }
        return false;
    }

    public bool removeGreenGoo(int amount)
    {
        if (greenGoo - amount >= 0)
        {
            greenGoo -= amount;
            return true;
        }
        return false;
    }

    public bool removeMetalAndGreenGoo(int metalAmount, int greenGooAmount)
    {
        if (metal - metalAmount >= 0 && greenGoo - greenGooAmount >= 0)
        {
            metal -= metalAmount;
            greenGoo -= greenGooAmount;
            return true;
        }
        return false;
    }

    public bool removeAlienMeat(int amount)
    {
        if (alienMeat - amount >= 0)
        {
            alienMeat -= amount;
            return true;
        }
        return false;
    }
}
