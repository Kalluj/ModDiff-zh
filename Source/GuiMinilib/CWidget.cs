﻿// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModDiff.GuiMinilib
{
    class CWidget : CElement
    {
        public Action<Rect> Do;

        public override void DoContent()
        {
            Do?.Invoke(bounds);
        }
    }
}
