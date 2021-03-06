﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronClient.Module
{
    public static class Hitbox
    {
        public static Modules.ModuleType ModuleType = new Modules.ModuleType("Hitbox", "Combat", true, ref _ToggleState, new Action(OnEnable), new Action(OnDisable), null, null, null, null, new Action(Refresh), new Action(Toggle));

        public static void OnLoad()
        {
            Modules.ModuleManagment.ValueRegister.RegisterModule(ModuleType);
        }

        public static bool _ToggleState = false;

        public static bool ToggleState
        {
            get { return _ToggleState; }
            set
            {
                if (_ToggleState != value)
                {
                    if (value)
                    {
                        OnEnable();
                        _ToggleState = value;
                    }
                    else if (!value)
                    {
                        OnDisable();
                        _ToggleState = value;
                    }
                }
            }
        }

        public static void OnEnable()
        {
            // Nop Memory
            Memory0.mem.PatchMemory("Minecraft.Windows.exe", 0x121A420, new byte[] { 0xF3, 0x0F, 0x10, 0x05, 0xA0, 0x6A, 0xE6, 0x00 });
        }

        public static void OnDisable()
        {
            // Restore Memory
            Memory0.mem.PatchMemory("Minecraft.Windows.exe", 0x121A420, new byte[] { 0xF3, 0x0F, 0x10, 0x81, 0x4C, 0x04, 0x00, 0x00 });
        }

        public static void Toggle()
        {
            _ToggleState = !_ToggleState;
        }

        public static void Refresh()
        {
            if (ToggleState)
            {
                OnEnable();
            }
            else if (!ToggleState)
            {
                OnDisable();
            }
        }
    }
}
