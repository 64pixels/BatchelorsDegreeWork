using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QrCodeScript : MonoBehaviour
{
    [SerializeField]
    private RawImage _rawImageBackground;
    [SerializeField]
    private AspectRatioFitter _aspectRatioFitter;
    [SerializeField]
    private TextMeshProUGUI _textOut;
    [SerializeField]
    private RectTransform _scanZone;
    [SerializeField]
    public SceneInfo sceneInfo;

    private bool _isCamAvaible;
    private WebCamTexture _cameraTexture;

    public string QrCodeScanResult;

    void Start()
    {
        SetUpCamera();
    }

    void Update()
    {
        UpdateCameraRender();
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            _isCamAvaible = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                _cameraTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
                break;
            }
        }
        _cameraTexture.Play();
        _rawImageBackground.texture = _cameraTexture;
        _isCamAvaible = true;
    }

    private void UpdateCameraRender()
    {
        if (_isCamAvaible == false)
        {
            return;
        }
        float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = _cameraTexture.videoRotationAngle;
        orientation = orientation * 3;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }
    
    public void OnClickScan()
    {
        Scan();
    }
    
    public void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            string result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height).ToString();
            if (result != null)
            {
                _textOut.text = "Pavyko nuskenuoti";
                sceneInfo.QrCodeScanResult = result;
                SceneManager.LoadScene(1);
            }
            else
            {
                _textOut.text = "Nepavyko nuskenuoti QR kodo";
            }
        }
        catch
        {
            _textOut.text = "QR kodas nerastas";
        }
    }
}
