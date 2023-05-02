using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPrompts : MonoBehaviour
{
    [Header("Interaction Canvas Elements")]

    [SerializeField]
    private TextMeshProUGUI primaryKey, secondaryKey, primaryPrompt, secondaryPrompt;

    private CanvasGroup primary, secondary, progressGroup;

    [SerializeField]
    private float fadeSpeed = 0.25f;

    [SerializeField]
    private Image progressBar;

    private float primaryAlpha = 0f;
    private float secondaryAlpha = 0f;

    private void Awake()
    {
        primary = primaryKey.GetComponentInParent<CanvasGroup>();
        secondary = secondaryKey.GetComponentInParent<CanvasGroup>();
        progressGroup = progressBar.GetComponentInParent<CanvasGroup>();
    }

    public void Update()
    {
        if(primary.alpha != primaryAlpha) { 
            primary.alpha = Mathf.Lerp(primary.alpha, primaryAlpha, fadeSpeed);
        }

        if (secondary.alpha != secondaryAlpha)
        {
            secondary.alpha = Mathf.Lerp(secondary.alpha, primaryAlpha, fadeSpeed);
        }
    }

    public void SetPrompt(Prompt p, string key)
    {
        if(p == Prompt.PRIMARY)
        {
            primaryKey.text = key;
            //primaryPrompt.text = text;
            primaryAlpha = 1f;
        } else
        {
            secondaryKey.text = key;
            //secondaryPrompt.text = text;
            secondaryAlpha = 1f;
        }

    }

    public void TogglePrompt(Prompt p, bool active)
    {
        if(p == Prompt.PRIMARY)
        {
            primaryAlpha = (active) ? 1f : 0f; ;
        } else
        {
            secondaryAlpha = (active) ? 1f : 0f; ;
        }
    }

    public void SetProgress(float progress) { 
        progressBar.fillAmount = Mathf.Clamp01(progress);
        progressGroup.alpha = (progress < 0f && progress < 1f) ? 1f : 0f;
    }

    public enum Prompt
    {
        PRIMARY,
        SECONDARY
    }
}
