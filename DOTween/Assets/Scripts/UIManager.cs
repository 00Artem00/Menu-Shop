using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float fadetime = 1f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public List<GameObject> items = new List<GameObject>();
    public AudioClip sound;
    public Image image;
    private Color[] colors = new Color[2];

    private AudioSource audioSource;

    private void Start()
    {
        colors[0] = new Color(0.5294f, 0.8078f, 0.9515f);
        colors[1] = new Color(0.9568f, 0.7568f, 0.7568f);
        Sequence s = DOTween.Sequence();
        s.Append(image.DOColor(colors[0], 1f).SetEase(Ease.Linear));
        s.Append(image.DOColor(colors[1], 1f).SetEase(Ease.Linear));
        s.SetLoops(-1, LoopType.Yoyo);
        audioSource = GetComponent<AudioSource>();
    }

    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0f;
        rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadetime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadetime);
        StartCoroutine("ItemsAnimation");
    }

    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1f;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -1000f), fadetime, false).SetEase(Ease.InOutQuint);
        canvasGroup.DOFade(0, fadetime);
    }

    IEnumerator ItemsAnimation()
    {
        foreach (var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }
        foreach (var item in items)
        {
            audioSource.PlayOneShot(sound);
            item.transform.DOScale(1f, fadetime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
