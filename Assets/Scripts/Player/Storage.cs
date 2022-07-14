using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public List<GameObject> items;
    public int requireAmount;
    public bool isEmpty()
    {
        return items.Count == 0;
    }
    public bool isFull()
    {
        return items.Count == requireAmount;
    }
    public void addItem(GameObject item)
    {
        items.Add(item);
    }

    public void removeItem(GameObject item)
    {
        items.Remove(item);
    }

    public GameObject getItem(GameObject item)
    {
        removeItem(item);
        return item;
    }

    public void clearItemList()
    {
        items.Clear();
    }
}
