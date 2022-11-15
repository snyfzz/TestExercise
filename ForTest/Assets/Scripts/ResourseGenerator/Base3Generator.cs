using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base3Generator : MonoBehaviour
{
    [SerializeField] private int maxStorage = 10;
    [SerializeField] private float delaySeconds = 7f;

    private int base3Resourse;
    private bool delay = true;

    [SerializeField] private GameObject Base3Storage;
    private Base3Storage base3Storage;

    [SerializeField] private GameObject completeInf;
    private TextMesh completeInfText;

    private void Start()
    {
        base3Resourse = 0;
        base3Storage = Base3Storage.GetComponent<Base3Storage>();

        completeInfText = completeInf.GetComponent<TextMesh>();
        UpdateText();
    }

    private void Update()
    {
        ResourseGenerator();
    }

    void ResourseGenerator()
    {
        if (delay && base3Resourse < maxStorage && base3Storage.base1Resourse >= 1 && base3Storage.base2Resourse >= 1)
        {
            delay = false;
            Invoke("Delay", delaySeconds);
        }
    }

    void Delay()
    {
        base3Resourse++;
        base3Storage.base1Resourse--;
        base3Storage.base2Resourse--;

        UpdateText();
        delay = true;
    }

    void UpdateText()
    {
        completeInfText.text = base3Resourse + "/" + maxStorage;
    }
}
