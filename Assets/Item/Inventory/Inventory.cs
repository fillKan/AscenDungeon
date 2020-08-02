﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public const float DEFAULT_RANGE = 1f;

    [SerializeField] private ItemSlot   mWeaponSlot;
    [SerializeField] private ItemSlot[] mAccessorySlot;
    [SerializeField] private ItemSlot[] mContainer;

    private void Awake()
    {
        mWeaponSlot.Init(SLOT_TYPE.WEAPON);

        for (int i = 0; i < mContainer.Length; ++i)
        {
            mContainer[i].Init(SLOT_TYPE.CONTAINER);

            if (i < mAccessorySlot.Length)
            {
                mAccessorySlot[i].Init(SLOT_TYPE.ACCESSORY);
            }
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < mContainer.Length; ++i)
        {
            if (mContainer[i].ContainItem == null)
            {
                mContainer[i].SetItem(item);

                return;
            }
        }
        Debug.Log("더이상 아이템을 담을수 없습니다!");
    }

    public float GetWeaponRange()
    {
        if (mWeaponSlot.ContainItem != null)
        {
            return mWeaponSlot.ContainItem.WeaponRange;
        }
        return DEFAULT_RANGE;
    }

    public void UseItem(ITEM_KEYWORD KEYWORD)
    {
        if (mWeaponSlot.ContainItem != null)
        {
            mWeaponSlot.ContainItem.WeaponUse(KEYWORD);
        }
        for (int i = 0; i < mAccessorySlot.Length; ++i)
        {
            if (mAccessorySlot[i].ContainItem != null)
            {
                mAccessorySlot[i].ContainItem.AccessoryUse(KEYWORD);
            }
        }
    }
    public void CUseItem(float power)
    {
        if (mWeaponSlot.ContainItem != null)
        {
            mWeaponSlot.ContainItem.CWeaponUse(power);
        }
        for (int i = 0; i < mAccessorySlot.Length; ++i)
        {
            if (mAccessorySlot[i].ContainItem != null)
            {
                mAccessorySlot[i].ContainItem.CAccessoryUse(power);
            }
        }
    }
}
