using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Database;
using System;

public class Meniu : MonoBehaviour
{
    [SerializeField]
    public SceneInfo sceneInfo;

    public string QrCodeScanResult;
    public string DatabaseOutput;
    public string Cables;

    private DatabaseReference reference;

    void Start() 
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        GetArrayFromFirebase();
    }

    public void UseAugmentedReality()
    {
        SceneManager.LoadScene(3);
    }
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }

    public void GetArrayFromFirebase()
    {
        string GetDataFrom = sceneInfo.QrCodeScanResult.ToString();

        reference.Child(GetDataFrom).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("GET data failed");

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string json = snapshot.GetRawJsonValue();

                char[] delimeters = new char[] { ' ', ',', '"', ':', '{', '}' };
                int index = 0;
                foreach (var a in json.Split(delimeters, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (index % 2 != 0){
                        Cables = Cables + " " + a + " ";
                        }
                    index += 1;
                    }
                sceneInfo.DatabaseOutput = Cables.ToString();
            }
        });
    }
}
