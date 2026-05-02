using System.Text;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;

namespace FAJousting.src.CollectibleBehaviors
{
    public class CollectibleBehaviorHeadLock : CollectibleBehavior
    {
        public bool LockHead { get; private set; } = false;

        public CollectibleBehaviorHeadLock(CollectibleObject collectibleObject) : base(collectibleObject) { }

        public override void OnLoaded(ICoreAPI api)
        {
            base.OnLoaded(api);

            JsonObject? faJousting = collObj.Attributes?["faJousting"];
            if (faJousting != null && faJousting.Exists)
            {
                LockHead = faJousting["lockHead"].AsBool(false);
            }
        }
    }
}