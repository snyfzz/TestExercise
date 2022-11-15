using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base3Storage : MonoBehaviour
{
    [SerializeField] private int maxStorage = 10;
    public int base1Resourse;
    public int base2Resourse;

    [SerializeField] private int delaySeconds = 3;
    private int i = 0;

    [SerializeField] private GameObject storageInf;
    private TextMesh storageInfText;

    [SerializeField] private GameObject player;
    private PlayerInventory playerInventory;

    private bool inCollision = true;

    [SerializeField] private GameObject greyResoursePrefab;
    [SerializeField] private GameObject redResoursePrefab;
    private GameObject greyResourse;
    private GameObject redResourse;
    private bool greyPrefabCreate = false;
    private bool redPrefabCreate = false;
    private bool allPrefabCreate = false;
    [SerializeField] float moveSpeed = 3f;

    private void Start()
    {
        base1Resourse = 0;
        base2Resourse = 0;

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
        if (allPrefabCreate)
        {
            greyResourse.transform.position = Vector3.MoveTowards(greyResourse.transform.position, transform.position, moveSpeed * Time.deltaTime);
            redResourse.transform.position = Vector3.MoveTowards(redResourse.transform.position, transform.position, moveSpeed * Time.deltaTime);

            Invoke("PrefabDestroy", 0.5f);
        }
        else if (greyPrefabCreate)
        {
            greyResourse.transform.position = Vector3.MoveTowards(greyResourse.transform.position, transform.position, moveSpeed * Time.deltaTime);

            Invoke("PrefabDestroy", 0.5f);
        }
        else if (redPrefabCreate)
        {
            redResourse.transform.position = Vector3.MoveTowards(redResourse.transform.position, transform.position, moveSpeed * Time.deltaTime);

            Invoke("PrefabDestroy", 0.5f);
        }
    }

    void PrefabDestroy()
    {
        if (allPrefabCreate)
        {
            Destroy(greyResourse);
            Destroy(redResourse);
        }
        else if (greyPrefabCreate)
        {
            Destroy(greyResourse);
        }
        else if (redPrefabCreate)
        {
            Destroy(redResourse);
        }

        allPrefabCreate = false;
        greyPrefabCreate = false;
        redPrefabCreate = false;
    }

    public void UpdateText()
    {
        storageInfText.text = base1Resourse.ToString() + "/" + base2Resourse.ToString() + "/" + maxStorage.ToString();
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
        if (playerInventory.base1 > 0 || playerInventory.base2 > 0 && base1Resourse + base2Resourse < maxStorage)
        {
            if(playerInventory.base1 > 0 && playerInventory.base2 > 0)
            {
                playerInventory.base1--;
                playerInventory.base2--;

                base1Resourse++;
                base2Resourse++;

                greyResourse = Instantiate(greyResoursePrefab, new Vector3(player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z), Quaternion.identity);
                redResourse = Instantiate(redResoursePrefab, new Vector3(player.transform.position.x, player.transform.position.y - 0.2f, player.transform.position.z), Quaternion.identity);
                allPrefabCreate = true;
            }
            else if (playerInventory.base1 > 0 && playerInventory.base2 == 0)
            {
                playerInventory.base1--;

                base1Resourse++;

                greyResourse = Instantiate(greyResoursePrefab, player.transform.position, Quaternion.identity);
                greyPrefabCreate = true;
            }
            else if (playerInventory.base2 > 0 && playerInventory.base1 == 0)
            {
                playerInventory.base2--;

                base2Resourse++;

                redResourse = Instantiate(redResoursePrefab, player.transform.position, Quaternion.identity);
                redPrefabCreate = true;
            }

            i = 0;
            TakeDelay();
        }
    }
}
