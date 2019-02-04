using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    public static Alert m;
    public Text titleText;
    public Text infoText;
    public Text buttonText;
    public Button button;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        m = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // *** For alert notification screen
    public void Open(string errorTittle, string errorInfo, string buttonText, UnityAction buttonAction)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        titleText.text = errorTittle;
        infoText.text = errorInfo;
        this.buttonText.text = buttonText;
        button.onClick.RemoveAllListeners();
        if (buttonAction != null)
            button.onClick.AddListener(buttonAction);
        button.onClick.AddListener(Close);
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

}
