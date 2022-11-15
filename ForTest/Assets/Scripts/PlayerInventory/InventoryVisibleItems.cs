using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryVisibleItems : MonoBehaviour
{
    [SerializeField] private GameObject red;
    [SerializeField] private GameObject grey;

    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        red.SetActive(false);
        grey.SetActive(false);
    }

    private void Update()
    {
        VisibleEnable();
    }

    void VisibleEnable()
    {
        if (playerInventory.base1 > 0 && playerInventory.base2 > 0)
        {
            red.SetActive(true);
            grey.SetActive(true);
        }
        else if (playerInventory.base1 > 0)
        {
            grey.SetActive(true);
        }
        else if (playerInventory.base2 > 0)
        {
            red.SetActive(true);
        }
        else
        {
            red.SetActive(false);
            grey.SetActive(false);
        }
    }
}
