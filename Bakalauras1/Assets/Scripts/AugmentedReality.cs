using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AugmentedReality : MonoBehaviour
{

    [SerializeField]
    public SceneInfo sceneInfo;
    [SerializeField]
    public TMP_Text Port1;
    [SerializeField]
    public TMP_Text Port2;
    [SerializeField]
    public TMP_Text Port3;
    [SerializeField]
    public TMP_Text Port4;
    [SerializeField]
    public TMP_Text Port5;

    public string QrCodeScanResult;

    // Start is called before the first frame update
    void Start()
    {
        SetTextValues();
    }

    // Setting values
    void SetTextValues()
    {
        string ports = sceneInfo.DatabaseOutput;
        char[] delimeters = new char[] {' '};
        int indexer = 1;
        foreach (var a in ports.Split(delimeters, StringSplitOptions.RemoveEmptyEntries))
        {
            if (indexer == 1)
            {
                Port1.text = a;
            }
            if (indexer == 2)
            {
                Port2.text = a;
            }
            if (indexer == 3)
            {
                Port3.text = a;
            }
            if (indexer == 4)
            {
                Port4.text = a;
            }
            if (indexer == 5)
            {
                Port5.text = a;
            }
            indexer += 1;
        }
                
    }
}
