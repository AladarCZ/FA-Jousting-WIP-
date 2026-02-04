using HarmonyLib;
using Vintagestory.API.Common;

namespace FAJousting
{
    public class FA_JoustingModSystem : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            new Harmony("fajousting.headlock").PatchAll();
        }
    }
}
