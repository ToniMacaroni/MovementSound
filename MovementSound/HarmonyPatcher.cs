using System;
using System.Reflection;
using HarmonyLib;

namespace MovementSound
{
    public class HarmonyPatcher : IDisposable
    {
        public bool IsActive { get; private set; }

        private readonly Harmony _harmony;

        public HarmonyPatcher(string id)
        {
            _harmony = new Harmony(id);
        }

        public void Patch()
        {
            if (IsActive) return;
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            IsActive = true;
        }

        public void Unpatch()
        {
            if (!IsActive) return;
            _harmony.UnpatchAll();
            IsActive = false;
        }

        public void Dispose()
        {
            Unpatch();
        }
    }
}