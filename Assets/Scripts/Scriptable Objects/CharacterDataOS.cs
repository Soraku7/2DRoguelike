using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable Objects/new CharacterData")]
public class CharacterDataOS : ScriptableObject
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public Sprite Sprite { get; private set; }
     
    [field:SerializeField] public int PurchasePrice { get; private set; }


    [SerializeField] private int attack;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int criticalChance;
    [SerializeField] private int criticalPrecent;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int maxHealth;
    [SerializeField] private int range;
    [SerializeField] private int healthRecoverySpeed;
    [SerializeField] private int armor;
    [SerializeField] private int luck;
    [SerializeField] private int dodge;
    [SerializeField] private int lifeSteal;

    public Dictionary<Stat, float> BaseStats
    {
        get
        {
            return new Dictionary<Stat, float>()
            {
                {Stat.Attack , attack},
                {Stat.AttackSpeed , attackSpeed},
                {Stat.CriticalChance , criticalChance},
                {Stat.CriticalPrecent , criticalPrecent},
                {Stat.MoveSpeed , moveSpeed},
                {Stat.MaxHealth , maxHealth},
                {Stat.Range , range},
                {Stat.HealthRecoverySpeed , healthRecoverySpeed},
                {Stat.Armor , armor},
                {Stat.Luck , luck},
                {Stat.Dodge , dodge},
                {Stat.LifeSteal , lifeSteal}
            };
        }
        private set
        {
            
        }
    }
}