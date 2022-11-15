using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxStorage = 4;
    private int usedStorage = 0;
    public int base1 = 0;
    public int base2 = 0;
    public bool inventoryFull = false;

    [SerializeField] private Text base1UI;
    [SerializeField] private Text base2UI;
    [SerializeField] private Text isFull;

    private void Start()
    {
        isFull.enabled = false;
    }

    private void Update()
    {
        UsedStorage();
    }

    void UsedStorage()
    {
        usedStorage = base1 + base2;

        base1UI.text = base1.ToString();
        base2UI.text = base2.ToString();

        if (usedStorage >= maxStorage)
        {
            isFull.enabled = true;
            inventoryFull = true;
        }
        else
        {
            isFull.enabled = false;
            inventoryFull = false;
        }
    }
}
