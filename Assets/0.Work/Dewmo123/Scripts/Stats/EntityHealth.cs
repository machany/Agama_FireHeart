namespace Scripts.Stats
{
    public class EntityHealth : Stat
    {
        public override void AfterHitFeedbacks()
        {
            _entity.OnHitEvent?.Invoke();
            base.AfterHitFeedbacks();
        }
    }
}
