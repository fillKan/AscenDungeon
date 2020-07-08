﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLibrary : Singleton<RoomLibrary>
{
    private Dictionary<ROOM_NUMBER, Room> mLibrary = new Dictionary<ROOM_NUMBER, Room>();

    [SerializeField]
    private List<Room> mRooms = new List<Room>();

    private void Awake()
    {
        foreach (Room iterator in mRooms)
        {
            if (!mLibrary.ContainsKey(iterator.RoomNumber))
            {
                mLibrary.Add(iterator.RoomNumber, iterator);

                Debug.Log(iterator.gameObject.name);
            }
        }
    }
}
