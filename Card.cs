using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Card : MonoBehaviour, iCard
{
    [Header("Back")]
    public SpriteRenderer cardBackImage;

    [Header("Frames")]
    public SpriteRenderer cardFrameFront;
    public SpriteRenderer cardFrameBack;

    [Header("Gem")]
    public bool useGem;
    public bool hideGemAfterPair;
    public List<GameObject> gemObjects = new List<GameObject>();

    [Header("Fake Shadow")]
    public bool useShadow;
    public SpriteRenderer shadow;
    public Color shadowColor;

    public event Action OnCardAnimate;
    DeckData deckType;
    CardData cardData;


    void Start()
    {
        if (!Application.isPlaying) return;

        if (!useGem) DisableGem();

        if (!useShadow)
            DisableShadow();
        else
            SetupShadowColor();
    }


    public void Animate(bool useSound)
    {
        OnCardAnimate();
        if (useSound && cardData) PlaySound(cardData.TalkSound);  
        if (useGem && hideGemAfterPair) DisableGem();
    }
    public void SetupCard(DeckData _deckType, CardData _cardData)
    {
        cardData = _cardData;
        deckType = _deckType;
        SetupCardImages(_deckType);
    }


    void SetupCardImages(DeckData _deckType)
    {
        if (!_deckType) return;
        if (cardBackImage) cardBackImage.sprite = deckType.CardBackImage;
        if (cardFrameFront) cardFrameFront.sprite = deckType.CardFrame;
        if (cardFrameBack) cardFrameBack.sprite = deckType.CardFrame;
    }
    void DisableGem()
    {
        foreach (var item in gemObjects)
        {
            item.SetActive(false);
        }
    }
    void DisableShadow() => shadow.gameObject.SetActive(false);
    void SetupShadowColor() => shadow.color = shadowColor;
    void PlaySound(AudioEvent audioEvent) => AudioManager.Instance.PlaySimpleEvent(audioEvent);

}
