using Meta.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Icons", menuName = "Config/Icons")]
    public class IconsSO : ScriptableObject
    {
        [BoxGroup("Bank")]
        [Header("Gold")] [HideLabel]
        [SerializeField, PreviewField(ObjectFieldAlignment.Left)]
        Sprite gold;
        [Header("Energy")]
        [BoxGroup("Bank")]
        [HideLabel] [SerializeField, PreviewField(ObjectFieldAlignment.Left)]
        Sprite energy;
        [BoxGroup("Bank")]
        [Header("Gem")] [HideLabel]
        [SerializeField, PreviewField(ObjectFieldAlignment.Left)]
        Sprite gem;

        [BoxGroup("Equip Categories")]
        [Header("Weapon")] [HideLabel] [SerializeField, PreviewField(ObjectFieldAlignment.Left)]
        Sprite weapon;
        [BoxGroup("Equip Categories")] [Header("Necklace")] [HideLabel]
        [SerializeField, PreviewField(ObjectFieldAlignment.Left)]
        Sprite necklace;
        [BoxGroup("Equip Categories")] [Header("Gloves")] [HideLabel]
        [SerializeField, PreviewField(ObjectFieldAlignment.Left)]
        Sprite gloves;
        [BoxGroup("Equip Categories")] [Header("Helm")] [HideLabel]
        [SerializeField, PreviewField(ObjectFieldAlignment.Left)]
        Sprite helm;
        [BoxGroup("Equip Categories")] [Header("Chest")] [HideLabel]
        [SerializeField, PreviewField(ObjectFieldAlignment.Left)]
        Sprite chest;
        [BoxGroup("Equip Categories")] [Header("Boots")] [HideLabel]
        [SerializeField, PreviewField(ObjectFieldAlignment.Left)]
        Sprite boots;

        //  [HorizontalLine(color: EColor.Gray)]
        public Sprite EquipCategory(EquipEnum equip) =>
            equip switch
            {
                EquipEnum.Weapon => weapon,
                EquipEnum.Necklace => necklace,
                EquipEnum.Gloves => gloves,
                EquipEnum.Helm => helm,
                EquipEnum.Vest => chest,
                EquipEnum.Boots => boots,
                _ => null
            };

        public Sprite BankIcon(BankCurrencyEnum type) =>
            type switch
            {
                BankCurrencyEnum.Gold => gold,
                BankCurrencyEnum.GEM => gem,
                BankCurrencyEnum.Energy => energy,
                _ => null
            };
    }
}