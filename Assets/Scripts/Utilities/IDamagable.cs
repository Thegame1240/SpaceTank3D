using System;

namespace Utilities
{
    public interface IDamagable
    {
        event Action OnExplode;
        void TakeDamage(int damage);
        void Explode();
    }
}