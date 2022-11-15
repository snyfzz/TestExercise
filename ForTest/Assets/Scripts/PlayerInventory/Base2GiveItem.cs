using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base2GiveItem : MonoBehaviour
{
    private Base2Generator baseGenerator;

    [SerializeField] private int delaySeconds = 3;
    private int i = 0;
    private bool inCollision = false;

    [SerializeField] private GameObject player;
    private PlayerInventory playerInventory;

    [SerializeField] private GameObject redResoursePrefab;
    private GameObject redResourse;
    private bool prefabCreate = false;
    [SerializeField] float moveSpeed = 1f;

    private void Start()
    {
        baseGenerator = GetComponent<Base2Generator>();
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
            redResourse.transform.position = Vector3.MoveTowards(redResourse.transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            Invoke("PrefabDestroy", 0.5f);
        }
    }

    void PrefabDestroy()
    {
        Destroy(redResourse);
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
        if (inCollision)
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
        if (baseGenerator.base2Resourse >= 1 && !playerInventory.inventoryFull)
        {
            baseGenerator.base2Resourse--;
            baseGenerator.UpdateText();
            playerInventory.base2++;
            i = 0;
            TakeDelay();

            redResourse = Instantiate(redResoursePrefab, transform.position, Quaternion.identity);
            prefabCreate = true;
        }
    }
}
