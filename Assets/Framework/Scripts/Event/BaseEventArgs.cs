﻿using System;

namespace KitaFramework
{
    public abstract class BaseEventArgs : EventArgs
    {
        public abstract int ID { get; }
    }
}