using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMain : MonoBehaviour
{
    [SerializeField]
    TitleGUIManager _titleGuiManager;

    void Start()
    {
        _titleGuiManager.StartButtonController.SetLabel("Touch start!");
        _titleGuiManager.StartButtonController.SetButtonOnClick(LoadMainScene);
    }

    void Update()
    {
        var col = _titleGuiManager.StartButtonController.LabelText.color;
        col.a = Mathf.Clamp01(Mathf.Sin(Time.time)+1f);
        _titleGuiManager.StartButtonController.LabelText.color = col;
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(SceneNames.MainScene);
    }
}
