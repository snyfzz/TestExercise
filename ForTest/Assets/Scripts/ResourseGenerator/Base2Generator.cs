using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base2Generator : MonoBehaviour
{
    [SerializeField] private int maxStorage = 10;
    [SerializeField] private float delaySeconds = 4f;

    [SerializeField] private GameObject completeInf;
    private TextMesh completeInfText;

    public int base2Resourse;
    private bool delay = true;

    [SerializeField] private GameObject Base2Storage;
    private Base2Storage base2Storage;
    

    private void Start()
    {
        base2Resourse = 0;
        base2Storage = Base2Storage.GetComponent<Base2Storage>();

        completeInfText = completeInf.GetComponent<TextMesh>();
        UpdateText();
    }

    private void Update()
    {
        ResourseGenerator();
    }

    void ResourseGenerator()
    {
        if (delay && base2Resourse < maxStorage && base2Storage.base1Resourse >= 1)
        {
            delay = false;
            Invoke("Delay", delaySeconds);
        }
    }

    void Delay()
    {
        base2Resourse++;
        base2Storage.base1Resourse--;

        UpdateText();
        delay = true;
    }

    public void UpdateText()
    {
        completeInfText.text = base2Resourse + "/" + maxStorage;
    }
}
