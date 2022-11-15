using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base2Storage : MonoBehaviour
{
    [SerializeField] private int maxStorage = 10;
    public int base1Resourse;

    [SerializeField] private int delaySeconds = 3;
    private int i = 0;

    [SerializeField] private GameObject storageInf;
    private TextMesh storageInfText;

    [SerializeField] private GameObject player;
    private PlayerInventory playerInventory;

    private bool inCollision = true;

    [SerializeField] private GameObject greyResoursePrefab;
    private GameObject greyResourse;
    private bool prefabCreate = false;
    [SerializeField] float moveSpeed = 3f;

    private void Start()
    {
        base1Resourse = 0;

        playerInventory = player.GetComponent<PlayerInventory>();

        storageInfText = storageInf.GetComponent<TextMesh>();
        UpdateText();
    }

    private void Update()
    {
        UpdateText();
        PrefabMove();
    }

    void PrefabMove()
    {
        if (prefabCreate)
        {
            greyResourse.transform.position = Vector3.MoveTowards(greyResourse.transform.position, transform.position, moveSpeed * Time.deltaTime);

            Invoke("PrefabDestroy", 0.5f);
        }
    }

    void PrefabDestroy()
    {
        Destroy(greyResourse);
        prefabCreate = false;
    }

    public void UpdateText()
    {
        storageInfText.text = base1Resourse.ToString() + "/" + maxStorage.ToString();
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
            CanTake();
        }
    }

    void CanTake()
    {
        if (playerInventory.base1 > 0 && base1Resourse < maxStorage)
        {
            playerInventory.base1--;
            base1Resourse++;
            i = 0;
            TakeDelay();

            greyResourse = Instantiate(greyResoursePrefab, player.transform.position, Quaternion.identity);
            prefabCreate = true;
        }
    }
}
