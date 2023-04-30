using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PopupManager : MonoBehaviour
{

    public static PopupManager Instance { get; private set; }

    [SerializeField]
    [Range(0f, 2f)]
    private float fadeTime = 1.0f;

    [SerializeField]
    private TextMeshProUGUI title, description, value;

    [SerializeField]
    private RawImage icon;

    //These might change so keep them separate
    [SerializeField]
    private TextMeshProUGUI primaryAction, secondaryAction;

    private CanvasGroup popup;

    private float desiredAlpha = 0f;

    private string symbol;

    private void Awake()
    {
        if(Instance !=  null)
        {
            Destroy(this);
            return;
        }

        symbol = value.text;    
        popup = GetComponent<CanvasGroup>();
        popup.alpha = 0f;
        popup.interactable = false;

        Instance = this;
    }

    public void ShowPopup(Item item)
    {
        title.text = item.itemName;
        description.text = item.description;
        icon.texture = item.Icon;
        value.text = item.value + symbol;

        //animate in
        desiredAlpha = 1f;
    }

    public void HidePopup()
    {
        desiredAlpha = 0f;
    }

    private void Update()
    {
        if(Mathf.Clamp01(popup.alpha) != Mathf.Clamp01(desiredAlpha))
        {
            popup.alpha = Mathf.Lerp(popup.alpha, desiredAlpha, Time.deltaTime * fadeTime);
        }
    }
}
