using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Items
{
    public enum EquipType
    {
        Top, Buttom
    }

    [Serializable]
    public struct AddingStat
    {
        //public StatSO targetStat;
        public float modifyValue;
    }

    [CreateAssetMenu(fileName = "EquipItemDataSO", menuName = "SO/Items/EquipItem")]
    public class EquipItemDataSO : ItemDataSO
    {
        public EquipType equipType;
        public List<AddingStat> addingStats;
        [TextArea]
        public string itemEffectDescription;

        private int _descriptionLength;

        public void AddModifier(/*EntityStat statCompo*/)
        {
            foreach (AddingStat stat in addingStats)
            {
                //StatSO targetStat = statCompo.GetStat(stat.targetStat);
                //if (targetStat != null)
                //{
                //    targetStat.AddModifier(itemName, stat.modifyValue);
                //}
            }
        }

        public void RemoveModifier(/*EntityStat statCompo*/)
        {
            foreach (AddingStat stat in addingStats)
            {
                //StatSO targetStat = statCompo.GetStat(stat.targetStat);
                //if (targetStat != null)
                //{
                //    targetStat.RemoveModifier(itemName);
                //}
            }
        }

        public override string GetDescription()
        {
            _stringBuilder.Clear();
            _descriptionLength = 0;
            foreach (var statValue in addingStats)
            {
                //AddItemDescription(statValue.targetStat.statName, statValue.modifyValue);
            }

            if (_descriptionLength < 5)
            {
                for (int i = _descriptionLength; i < 5; ++i)
                {
                    _stringBuilder.AppendLine();
                    _stringBuilder.Append("");
                }
            }

            if (!string.IsNullOrEmpty(itemEffectDescription))
            {
                _stringBuilder.AppendLine();
                _stringBuilder.Append(itemEffectDescription);
            }

            return _stringBuilder.ToString();
        }

        private void AddItemDescription(string statName, float value)
        {
            //multi locale service must be implement here
            if (value != 0)
            {
                if (_stringBuilder.Length > 0)
                {
                    _stringBuilder.AppendLine();
                }

                ++_descriptionLength;
                _stringBuilder.Append($"{statName} : {value.ToString()}");
            }
        }
    }
}
