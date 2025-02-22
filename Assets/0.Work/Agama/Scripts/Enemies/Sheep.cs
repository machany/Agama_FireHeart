using Unity.Behavior;

namespace Agama.Scripts.Enemies
{
    public class Sheep : BehaviorEnemy
    {
        [BlackboardEnum]
        public enum SheepState
        {
            Patroll,
            Run,
            Hit,
            Dead
        }

        protected override void HandleHitEvent()
        {
        }
    }
}
