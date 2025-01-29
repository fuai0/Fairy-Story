using System.Collections;
using UnityEngine;

public enum StatType
{
    strength,
    agility,
    vitality,
    fate,


    damage,
    cirtChance,
    cirtPower,

    maxHealth,
    armor,
    evasion
}

public class CharacterStats : MonoBehaviour
{
    [Header("Majior stats")]
    public Stat strength; // 每点提高1点伤害和1%暴击伤害
    public Stat agility; // 每点提高1点闪避和1%的暴击几率
    public Stat vitality; // 每点提高5点的生命值
    public Stat fate; // 每点影响敌人源点掉落数量

    [Header("Offensive stats")]
    public Stat damage; // 伤害
    public Stat cirtChance; // 暴击率
    public Stat cirtPower; // 暴击伤害,基础值为150

    [Header("Defencive stats")]
    public Stat maxHealth; // 最大生命值
    public Stat armor;  // 护甲
    public Stat evasion; // 闪避

    public int currentHealth;

    public System.Action onHealthChanged;

    [HideInInspector] public Entity entity;

    protected void Awake()
    {
        entity = GetComponent<Entity>();
        currentHealth = GetHealth();
    }

    protected virtual void Start()
    {
        cirtPower.SetDefaultValue(150);
    }

    protected virtual void Update()
    {
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (CanAvoidAttack(_targetStats))
            return;

        _targetStats.entity.Hitted();
        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCirt())
        {
            totalDamage = CirtDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealth(_damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int GetHealth() => maxHealth.GetValue() + vitality.GetValue() * 5;

    public virtual void IncreaseHealth(int _amount)
    {
        currentHealth += _amount;

        if (currentHealth > GetHealth())
            currentHealth = GetHealth();

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void DecreaseHealth(int _damage)
    {
        currentHealth -= _damage;

        if (onHealthChanged != null)
            onHealthChanged();

    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    #region 统计计算

    protected int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        totalDamage = Mathf.Clamp(totalDamage - _targetStats.armor.GetValue(), 0, int.MaxValue);
        return totalDamage;
    }

    protected bool CanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }

    protected bool CanCirt()
    {
        int totalCirtChance = cirtChance.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) < totalCirtChance)
        {
            return true;
        }
        return false;
    }

    protected int CirtDamage(int _damage)
    {
        float totalCirtPower = (cirtPower.GetValue() + strength.GetValue()) * .01f;
        float cirtDamage = _damage * totalCirtPower;

        return Mathf.RoundToInt(cirtDamage);
    }

    #endregion

    public Stat GetStat(StatType _statType)
    {
        switch (_statType)
        {
            case StatType.strength: return strength;
            case StatType.agility: return agility;
            case StatType.vitality: return vitality;

            case StatType.damage: return damage;
            case StatType.cirtChance: return cirtChance;
            case StatType.cirtPower: return cirtPower;

            case StatType.maxHealth: return maxHealth;
            case StatType.armor: return armor;
            case StatType.evasion: return evasion;
        }
        return null;
    }

    public virtual void IncreaseStat(int _modifier, float _duration, Stat _statToModify)
    {
        StartCoroutine(StatModCoroutine(_modifier, _duration, _statToModify));
    }

    IEnumerator StatModCoroutine(int _modifier, float _duration, Stat _statToModify)
    {
        _statToModify.AddModifier(_modifier);

        yield return new WaitForSeconds(_duration);

        _statToModify.RemoveModifier(_modifier);
    }
}
