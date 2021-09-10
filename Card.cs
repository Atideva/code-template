using System;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour, iCard
{
    [Header("Back")]
    [SerializeField] SpriteRenderer cardBackImage;

    [Header("Frames")]
    [SerializeField] SpriteRenderer cardFrameFront;
    [SerializeField] SpriteRenderer cardFrameBack;

    [Header("Gem")]
    [SerializeField] bool useGem;
    [SerializeField] bool hideGemAfterPair;
    [SerializeField] List<GameObject> gemObjects = new List<GameObject>();

    [Header("Fake Shadow")]
    [SerializeField] bool useShadow;
    [SerializeField] SpriteRenderer shadow;
    [SerializeField] Color shadowColor;

    public event Action OnCardAnimate = delegate { };
    DeckData deckType;
    CardData cardData;


    void Start()
    {
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
    void PlaySound(SoundData sound) => AudioManager.Instance.PlaySound(sound);

}
