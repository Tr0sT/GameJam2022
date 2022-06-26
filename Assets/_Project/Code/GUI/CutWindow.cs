#nullable enable

using System.Collections;
using System.Collections.Generic;
using NuclearBand;
using UnityEngine;

public class CutWindow : FridgeWindow
{
    #region Creation
    private const string Path = "GUI/CutWindow/CutWindow";

    public static CutWindow CreateWindow()
    {
        return (CutWindow)WindowsManager.CreateWindow(Path, window =>
        {
            var w = (CutWindow)window;
        }).Window;
    }
    #endregion

    private int _curImage;

    [SerializeField] 
    private List<GameObject> _images = new();
    
    public override void Init()
    {
        base.Init();
        SetImage(0);
        AudioManager.PlayMusic("RockerChicks", true);
    }

    void SetImage(int num)
    {
        foreach (var image in _images)
        {
            image.SetActive(false);
        }

        if (num == _images.Count)
        {
            Close();
            return;
        }

        _images[num].SetActive(true);
    }

    public void OnClick()
    {
        _curImage++;
        SetImage(_curImage);
    }

    public override void Close()
    {
        base.Close();
        GameController.Instance.StartGame();
    }
}