﻿using Agama.Scripts.Entities;

namespace Scripts.Items
{
    public interface IUsable
    {
        public void ChoiceItem(Entity entity);
        public void UseItem(Entity entity);
    }
}
