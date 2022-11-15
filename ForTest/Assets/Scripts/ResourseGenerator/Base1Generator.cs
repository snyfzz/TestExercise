using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base1Generator : MonoBehaviour
{
    [SerializeField] private int maxStorage = 10;
    [SerializeField] private float delaySeconds = 2f;

    [SerializeField] private GameObject completeInf;
    private TextMesh completeInfText;

    public int base1Resourse;
    private bool delay = true;

    private void Start()
    {
        base1Resourse = 0;

        completeInfText = completeInf.GetComponent<TextMesh>();
        UpdateText();
    }

    private void Update()
    {
        ResourseGenerator();
    }

    void ResourseGenerator()
    {
        if (delay && base1Resourse < maxStorage)
        {
            delay = false;
            Invoke("Delay", delaySeconds);
        }
    }

    void Delay()
    {
        base1Resourse++;

        UpdateText();
        delay = true;
    }

    public void UpdateText()
    {
        completeInfText.text = base1Resourse + "/" + maxStorage;
    }
}
