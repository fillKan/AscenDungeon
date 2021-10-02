using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxTutorial : TutorialBase, ICombatable
{
    [SerializeField] private AbilityTable   _AbilityTable;
    [SerializeField] private Animation      _HitAnimation;

    [SerializeField] private int    _NeedHitCount;
    private int                     _StackHitCount;

    public AbilityTable GetAbility
    {
        get => _AbilityTable;
    }

    public void CastBuff(Buff buffType, IEnumerator castedBuff)
    {
        StartCoroutine(castedBuff);
    }

    public void Damaged(float damage, GameObject attacker)
    {
        ++_StackHitCount;

        _HitAnimation.Rewind();
        _HitAnimation.Play();

        var offset = new Vector3(0f, 0.6f, 0f);
        EffectLibrary.Instance.UsingEffect(EffectKind.Damage, offset + transform.position);

        if (_StackHitCount >= _NeedHitCount)
        {
            EndTutorial();
            // 2147483648 + n 대를 때리면 튜토리얼을 깰 수 있다!
            _StackHitCount = int.MinValue;
        }
    }

    public override void StartTutorial()
    {
        base.StartTutorial();
        gameObject.SetActive(true);
    }
}
