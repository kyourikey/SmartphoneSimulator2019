using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button Button;
    public Text LabelText;
    public Image Image;
    public Action OnButtonClickAction { get; private set;}

    public void SetLabel(string labelText)
    {
        LabelText.text = labelText;
    }

    public void SetImage(Sprite image)
    {
        Image.sprite = image;
    }

    public void SetButtonOnClick(Action onClickAction)
    {
        OnButtonClickAction = onClickAction;
    }

    public void AddButtonOnClick(Action onClickAction)
    {
        OnButtonClickAction += onClickAction;
    }

    public void ResetButtonOnClick()
    {
        OnButtonClickAction = () => { };
    }

    void Awake()
    {
        OnButtonClickAction = () => { };
    }

    void Start()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        OnButtonClickAction();
    }
}
