using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamelessSpear : Item
{
    private const int NeedAttackStack_Weapon    = 3;
    private const int NeedAttackStack_Accessory = 5;

    private const int Idle   = 0;
    private const int Attack = 1;

    private readonly Quaternion EffectRotate2Left  = Quaternion.Euler(0, 0, -90);
    private readonly Quaternion EffectRotate2Right = Quaternion.Euler(0, 0,  90);

    [Header("Item Action Property")]
    [SerializeField] private Animator _Animator;
    [SerializeField] private Area _CollisionArea;

    private Player _Player;

    private int _AnimHash;
    private int _AttackStack;

    private bool _IsAlreadyInit = false;

    public override void AttackCancel()
    {
        _Animator.SetInteger(_AnimHash, Idle);
        _Animator.Play("Idle");
    }
    public override void AttackAction(GameObject attacker, ICombatable combatable)
    {
        if (_Animator.GetInteger(_AnimHash) == Idle) {
            _Animator.SetInteger(_AnimHash, Attack);
        }
    }
    public override void OffEquipThis(SlotType offSlot)
    {
        switch (offSlot)
        {
            case SlotType.Accessory:
                {

                }
                break;
            case SlotType.Weapon:
                {

                }
                break;
        }
    }
    public override void OnEquipThis(SlotType onSlot)
    {
        _AttackStack = 0;

        if (!_IsAlreadyInit)
        {
            _AnimHash = _Animator.GetParameter(0).nameHash;
            _IsAlreadyInit = true;
        }
        switch (onSlot)
        {
            case SlotType.Accessory:
                {

                }
                break;
            case SlotType.Weapon:
                {
                    Debug.Assert(transform.root.TryGetComponent(out _Player));
                    _CollisionArea.SetEnterAction(HitAction);
                }
                break;
        }
    }
    private void HitAction(GameObject enter)
    {
        if (enter.TryGetComponent(out ICombatable combatable))
        {
            combatable.Damaged(StatTable[ItemStat.AttackPower], _Player.gameObject);
            Inventory.Instance.AttackAction(_Player.gameObject, combatable);
        }
    }
    protected override void AttackAnimationPlayOver()
    {
        ++_AttackStack;

        _Animator.SetInteger(_AnimHash, Idle);
        base.AttackAnimationPlayOver();
    }
    private void AE_UseAttackEffect()
    {
        MainCamera.Instance.Shake(0.4f, 1.2f);

        Effect effect = EffectLibrary.Instance.UsingEffect
            (EffectKind.SwordAfterImage, _EffectSummonPoint.position);

        effect.transform.rotation = _Player.IsLookAtLeft() ? 
            EffectRotate2Left : EffectRotate2Right;
    }
}
