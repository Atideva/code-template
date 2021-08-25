using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/New Card")]
public class CardData : ScriptableObject
{
    [Header("Setup")]
    [SerializeField] GameObject cardPrefab;
    [SerializeField] AudioEvent talkSound;

    [Header("Shop price")]
    public ShopPrice shopPrice;

    [Header("Abilities")]
    public List<CardAbilitySetup> abilities;

    public GameObject CardPrefab { get => cardPrefab; }
    public AudioEvent TalkSound { get => talkSound; }

}
