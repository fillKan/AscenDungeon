using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Meteorite : Item
{
    [Header("Meteorh Property")]
    [SerializeField] private float _Speed;
    [SerializeField] private float _Radius;
    [Space()]
    [SerializeField] private Area _Meteore;
    [SerializeField] private Transform _OrbitObject;

    private GameObject _Player;
    private IEnumerator _Resolving;

    private bool _IsAlreadyInit = false;

    public override void AttackCancel()
    { }
    public override void OffEquipThis(SlotType offSlot)
    {
        switch (offSlot)
        {
            case SlotType.Accessory:
                {
                    DisableMeteorite();
                }
                break;
            case SlotType.Weapon:
                {
                    DisableMeteorite(3);
                }
                break;
        }
    }
    public override void OnEquipThis(SlotType onSlot)
    {
        InitMethod();

        switch (onSlot)
        {
            case SlotType.Accessory:
                {
                    EnableMeteorite();
                }
                break;
            case SlotType.Weapon:
                {
                    EnableMeteorite(3);
                }
                break;
        }
    }
    private void InitMethod()
    {
        if (!_IsAlreadyInit)
        {
            _Player = GameObject.FindGameObjectWithTag("Player");

            _MeteoPool.Init(3, _Meteore, meteo =>
            {
                meteo.SetEnterAction(enter => {

                    float damage = StatTable[ItemStat.AttackPower];
                    if (enter.TryGetComponent(out ICombatable combatable))
                    {
                        combatable.Damaged(damage, _Player);

                        Inventory.Instance.OnAttackEvent(_Player, combatable);
                        Inventory.Instance.ProjectionHit(enter, damage);
                    }
                });
            });
            _IsAlreadyInit = true;
            _OrbitObject.SetParent(null);
        }
    }
    private IEnumerator ResolvingRoutine()
    {
        float angle = 0f;

        _OrbitObject.gameObject.SetActive(true);
        do
        {
            float angleDistance = 360f / _EnabledMeteoList.Count;

            angle += Time.deltaTime * Time.timeScale * _Speed * Inventory.Instance.AttackSpeed;
            angle = Mathf.Abs(angle) > 360f ? angle % 360f : angle;

            for (int i = 0; i < _EnabledMeteoList.Count; ++i)
            {
                float rot = (angle + angleDistance * i + 90f * Mathf.Sign(_Speed)) * Mathf.Deg2Rad;

                _EnabledMeteoList[i].transform.localPosition =
                    new Vector2(Mathf.Cos(rot), Mathf.Sin(rot)) * _Radius;

                _EnabledMeteoList[i].transform.localRotation =
                    Quaternion.AngleAxis(angle + angleDistance * i, Vector3.forward);
            }
            yield return null;

            _OrbitObject.localPosition = _Player.transform.localPosition;

        } while (_EnabledMeteoList.Count != 0);

        _Resolving = null;
        _OrbitObject.gameObject.SetActive(false);
    }
}

public partial class Meteorite
{
    private Pool<Area> _MeteoPool 
        = new Pool<Area>();

    private List<Area> _EnabledMeteoList
         = new List<Area>();

    private void EnableMeteorite(int count = 1)
    {
        var meteor = _MeteoPool.Get();

        _EnabledMeteoList.Add(meteor);

        meteor.transform.parent = _OrbitObject;
        meteor.transform.localScale = Vector3.one;

        if (--count > 0)
        {
            EnableMeteorite(count);
        }
        if (_Resolving == null)
        {
            StartCoroutine(_Resolving = ResolvingRoutine());
        }
    }
    private void DisableMeteorite(int count = 1)
    {
        _MeteoPool.Add(_EnabledMeteoList[0]);
        _EnabledMeteoList.RemoveAt(0);

        if (--count > 0)
        {
            DisableMeteorite(count);
        }
        return;
    }
}