using FAJousting.src.CollectibleBehaviors;
using HarmonyLib;
using Vintagestory.API.Common;

namespace FAJousting
{
    public class FAJoustingModSystem : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            api.RegisterCollectibleBehaviorClass($"{Mod.Info.ModID}:HeadLock", typeof(CollectibleBehaviorHeadLock));
            new Harmony("fajousting.headlock").PatchAll();
        }
    }
}
