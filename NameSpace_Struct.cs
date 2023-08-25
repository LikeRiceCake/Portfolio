using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PublicStructs
{
    namespace Encyclopedia
    {
        public struct CollectionInfo
        {
            public CollectionInfo(string _name, Sprite _sprite, string _description, int _numbering, string _skillDescription)
            {
                name = _name;
                sprite = _sprite;
                description = _description;
                numbering = _numbering;
                skillDescription = _skillDescription;
            }

            public string name;
            public Sprite sprite;
            public string description;
            public int numbering;
            public string skillDescription;
        }
    }

    namespace Character
    {
        public struct CharacterStat
        {
            public int maxHp { get; set; }
            public int currentHp {get; set;}
            public int[] damage {get; set;}
            public float speed {get; set;}
        }

        public struct EnemyStat
        {
            public int maxHp;
            public int currentHp;
            public int damage;
            public float speed;
            public float attackRange;
            public float sight;
            public float attackCool;
            public float currentAttackCool;
        }
    }
}