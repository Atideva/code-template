using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/New Card")]
public class CardData : ScriptableObject
{
    [Header("Setup")]
    [SerializeField] GameObject cardPrefab;
    [SerializeField] SoundData talkSound;
    [SerializeField] ShopPrice shopPrice;

    [Header("Abilities")]
    public List<CardAbilityData> abilities;

    
    public ReadOnlyCollection<CardAbilityData> Abilities => abilities.AsReadOnly();
    public ShopPrice ShopPrice =>  ShopPrice();
    public GameObject CardPrefab  => cardPrefab;
    public SoundData TalkSound   => talkSound;

}
