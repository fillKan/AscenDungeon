﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLightningTotem : MonoBehaviour
{
    public bool IsOver
    { get; private set; }

    [SerializeField] private GameObject TotemUser;
    [SerializeField] private GameObject SpecialTotem;

    [SerializeField] private float SkillCastingPoint;

    [Header("Special Lightning")]
    [SerializeField] private float Damage;
    [SerializeField] private Area Lightning;

    public void Init()
    {
        IsOver = false;

        void GiveDamage(GameObject target)
        {
            if (target.TryGetComponent(out ICombatable combatable)) {
                combatable.Damaged(Damage, TotemUser);
            }
        }
        Lightning.SetEnterAction(GiveDamage);
    }
    public void Cast(Vector2 castPoint)
    {
        IsOver = false;

        transform.position = castPoint;
        SpecialTotem.SetActive(true);

        StartCoroutine(WaitSkillCasting());
    }
    private IEnumerator WaitSkillCasting()
    {
        yield return new WaitForSeconds(SkillCastingPoint);
        Lightning.gameObject.SetActive(true);

        yield return new WaitWhile(() => Lightning.gameObject.activeSelf);
        IsOver = true;
    }
}
