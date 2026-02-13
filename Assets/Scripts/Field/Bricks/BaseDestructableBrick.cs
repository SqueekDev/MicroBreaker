using System;
using Base;

namespace Field
{
    public abstract class BaseDestructableBrick : BaseBrick, IDamageable
    {
        public Action Triggered;

        public void TakeDamage()
        {
            Triggered?.Invoke();
        }
    }
}