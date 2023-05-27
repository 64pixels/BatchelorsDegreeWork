using UnityEngine;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "Info")]
public class SceneInfo : ScriptableObject
{
    public string QrCodeScanResult;
    public string DatabaseOutput;
}
