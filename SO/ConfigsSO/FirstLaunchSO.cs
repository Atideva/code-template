using System.Collections.Generic;
using Meta.Data;
using Sirenix.OdinInspector;
using SO.EquipmentSO;
using SO.PerksSO;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "FirstLaunch", menuName = "Config/First Launch")]
    public class FirstLaunchSO : ScriptableObject
    {
        [Space(20)]
        [InfoBox("First launch game preset")]
        [FoldoutGroup("User")] [SerializeField] BankData bank;
        [FoldoutGroup("User")] [SerializeField] OptionsData options;
        [FoldoutGroup("User")] [SerializeField] [InlineEditor] ScenesListSO unlockScenes;
        [FoldoutGroup("User")] [SerializeField] [InlineEditor] HeroesListSO heroes;

        [FoldoutGroup("Character Equipment")]
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] WeaponSO weapon;
        [FoldoutGroup("Character Equipment")]
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] NecklaceSO necklace;
        [FoldoutGroup("Character Equipment")]
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] GlovesSO gloves;
        [FoldoutGroup("Character Equipment")]
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] HelmSO helm;
        [FoldoutGroup("Character Equipment")]
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] VestSO vest;
        [FoldoutGroup("Character Equipment")]
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] BootsSO boots;

        [FoldoutGroup("Perks")]
        [SerializeField] List<ActivePerkSO> activePerks = new();
        [FoldoutGroup("Perks")]
        [SerializeField] List<PassivePerkSO> passivePerks = new();

        public ScenesListSO UnlockScenes => unlockScenes;

        public OptionsData Options
        {
            get
            {
                var newOptions = new OptionsData();
                foreach (var t in options.toggles)
                    newOptions.toggles.Add(new OptionToggleData(t.type, t.toggle));
                return newOptions;
            }
        }


        public BankData Bank
            => new()
            {
                gold = bank.gold,
                gem = bank.gem,
                energy = bank.energy
            };


        public CharacterEquipmentData CharacterEquip
            => new()
            {
                weapon = new EquipmentData {so = weapon},
                necklace = new EquipmentData {so = necklace},
                gloves = new EquipmentData {so = gloves},
                helm = new EquipmentData {so = helm},
                vest = new EquipmentData {so = vest},
                boots = new EquipmentData {so = boots}
            };

        public HeroesData Heroes
        {
            get
            {
                var data = new HeroesData();
                foreach (var perk in heroes.List)
                {
                    var hero = new HeroCardData
                    {
                        so = perk,
                        lvl = 1,
                        owned = true
                    };
                    data.heroes.Add(hero);
                }
                return data;
            }
        }

        public UnlockedPerksData UnlockedPerks
        {
            get
            {
                var data = new UnlockedPerksData();

                foreach (var perk in activePerks)
                {
                    var newData = new ActivePerkData {so = perk};
                    data.activePerks.Add(newData);
                }

                foreach (var perk in passivePerks)
                {
                    var newData = new PassivePerkData {so = perk};
                    data.passivePerks.Add(newData);
                }

                return data;
            }
        }
    }
}