using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : Item
{
    [Header("Meteorh Property")]
    [SerializeField] private float _Speed;
    [SerializeField] private float _Radius;
    [Space()]
    [SerializeField] private Area[] _Meteores;

    private bool _IsAlreadyInit = false;
    private Coroutine _UpdateRoutine;

    private GameObject _Player;

    public override void AttackCancel()
    { }
    public override void OffEquipThis(SlotType offSlot)
    {
        if (offSlot == SlotType.Container) return;
        _UpdateRoutine.StopRoutine();

        for (int i = 0; i < 3; ++i) {
            _Meteores[i].gameObject.SetActive(false);
        }
    }
    public override void OnEquipThis(SlotType onSlot)
    {
        if (!_IsAlreadyInit)
        {
            _Player = GameObject.FindGameObjectWithTag("Player");

            _UpdateRoutine = new Coroutine(this);
            _IsAlreadyInit = true;

            for (int i = 0; i < 3; ++i)
            {
                _Meteores[i].SetEnterAction(enter => {

                    float damage = StatTable[ItemStat.AttackPower];
                    if (enter.TryGetComponent(out ICombatable combatable))
                    {
                        combatable.Damaged(damage, _Player);

                        Inventory.Instance.OnAttackEvent(_Player, combatable);
                        Inventory.Instance.ProjectionHit(enter, damage);
                    }
                });
            }
        }
        switch (onSlot)
        {
            case SlotType.Accessory:
                _UpdateRoutine.StartRoutine(UpdateRoutine(1));
                break;
            case SlotType.Weapon:
                _UpdateRoutine.StartRoutine(UpdateRoutine(3));
                break;
        }
    }
    private IEnumerator UpdateRoutine(int count)
    {
        float angle = 0f;

        for (int i = 0; i < count; ++i) {
            _Meteores[i].gameObject.SetActive(true);
        }
        while (true)
        {
            angle += Time.deltaTime * Time.timeScale * _Speed;
            angle = Mathf.Abs(angle) > 360f ? angle % 360f : angle;

            for (int i = 0; i < count; ++i)
            {
                float rot = (angle + 120f * i + 90f * Mathf.Sign(_Speed)) * Mathf.Deg2Rad;

                _Meteores[i].transform.localPosition =
                    new Vector2(Mathf.Cos(rot), Mathf.Sin(rot)) * _Radius;

                _Meteores[i].transform.localRotation =
                    Quaternion.AngleAxis(angle + 120f * i, Vector3.forward);
            }
            yield return null;
        }
    }
}
