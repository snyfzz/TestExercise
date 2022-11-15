using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base1GiveItem : MonoBehaviour
{
    private Base1Generator baseGenerator;

    [SerializeField] private int delaySeconds = 2;
    private int i = 0;
    private bool inCollision = false;

    [SerializeField] private GameObject player;
    private PlayerInventory playerInventory;

    [SerializeField] private GameObject greyResoursePrefab;
    private GameObject greyResourse;
    private bool prefabCreate = false;
    [SerializeField] float moveSpeed = 3f;

    private void Start()
    {
        baseGenerator = GetComponent<Base1Generator>();
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        PrefabMove();
    }

    void PrefabMove()
    {
        if (prefabCreate)
        {
            greyResourse.transform.position = Vector3.MoveTowards(greyResourse.transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            Invoke("PrefabDestroy", 0.5f);
        }
    }

    void PrefabDestroy()
    {
        Destroy(greyResourse);
        prefabCreate = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inCollision = true;
        TakeDelay();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inCollision = false;
        i = 0;
    }

    void TakeDelay()
    {
        if(inCollision)
        {
            Invoke("DelayChangeI", 1f);
        }
    }

    void DelayChangeI()
    {
        if (i < delaySeconds)
        {
            i++;
            TakeDelay();
        }
        else
        {
            CanGive();
        }
    }

    void CanGive()
    {
        if(baseGenerator.base1Resourse >= 1 && !playerInventory.inventoryFull)
        {
            baseGenerator.base1Resourse--;
            baseGenerator.UpdateText();
            playerInventory.base1++;
            i = 0;
            TakeDelay();

            greyResourse = Instantiate(greyResoursePrefab, transform.position, Quaternion.identity);
            prefabCreate = true;
        }
    }
}
