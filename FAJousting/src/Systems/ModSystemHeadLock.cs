using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace FAJousting.src.Systems

{
    public class ModSystemHeadLock : ModSystem
    {
        private ICoreServerAPI? sapi;

        public override void StartServerSide(ICoreServerAPI api)
        {
            sapi = api;
            api.Event.RegisterGameTickListener(OnServerTick, 200);
        }

        private void OnServerTick(float dt)
        {
            if (sapi?.World?.AllOnlinePlayers == null)
            {
                return;
            }

            foreach (IPlayer player in sapi.World.AllOnlinePlayers)
            {
                UpdatePlayer(player);
            }
        }

        private static void UpdatePlayer(IPlayer player)
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
                if (slot.Itemstack?.Collectible is ItemWearable wearable && slot.Itemstack.Collectible.Attributes["lockHeadMovement"].AsBool(false))
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
