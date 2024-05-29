﻿
using System.Collections.Generic;

namespace SaltLands;

public enum SaltEvents
{
    LoadHomeScreen
}

public struct SaltEventsComparer : IEqualityComparer<SaltEvents>
{
    public bool Equals(SaltEvents x, SaltEvents y)
    {
        return x == y;
    }


    public int GetHashCode(SaltEvents obj)
    {
        return (int)obj;
    }
}