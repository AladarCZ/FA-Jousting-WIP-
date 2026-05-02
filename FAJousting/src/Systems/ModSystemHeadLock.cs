using FAJousting.src.CollectibleBehaviors;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace FAJousting.src.Systems

{
    public class ModSystemHeadLock : ModSystem
    {
        private ICoreServerAPI? sapi;

        public override bool ShouldLoad(EnumAppSide forSide) => true;

        public override void StartServerSide(ICoreServerAPI api)
        {
            sapi = api;
            api.Event.RegisterGameTickListener(OnServerTick1s, 1000);
        }

        private void OnServerTick1s(float dt)
        {
            if (sapi?.World == null)
            {
                return;
            }

            foreach (IPlayer player in sapi.World.AllOnlinePlayers)
            {
                ProcessPlayer(player);
            }
        }

        private static void ProcessPlayer(IPlayer player)
        {
            if (player?.Entity is not EntityPlayer entity || !entity.Alive)
            {
                return;
            }

            IInventory inventory = player.InventoryManager.GetOwnInventory(GlobalConstants.characterInvClassName);
            if (inventory == null)
            {
                return;
            }

            bool lockHead = false;

            // Only loop until the first item that locks the head
            foreach (ItemSlot slot in inventory)
            {
                CollectibleBehaviorHeadLock? behavior = slot?.Itemstack?.Item?.GetBehavior<CollectibleBehaviorHeadLock>();

                if (behavior == null)
                {
                    continue;
                }

                if (behavior.LockHead)
                {
                    lockHead = true;
                    break;
                }
            }

            // Only update if changed
            if (entity.WatchedAttributes.GetBool("faJoustingLockHeadMovement") != lockHead)
            {
                entity.WatchedAttributes.SetBool("faJoustingLockHeadMovement", lockHead);
                entity.WatchedAttributes.MarkPathDirty("faJoustingLockHeadMovement");
            }
        }
    }
}
