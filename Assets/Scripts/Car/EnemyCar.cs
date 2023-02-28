using System;
using Cannon;
using UnityEngine;
using Utilities;

namespace Car
{
    public class EnemyCar : MonoBehaviour, IDamagable
    {
        public int Hp { get; private set; }

        public event Action OnExplode;

        public void Init(int hp)
        {
            Hp = hp;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log(damage);
            Debug.Log("Before" + Hp);
            Hp -= damage;
            
            if (Hp <= 0)
            {
                Explode();
            }
            Debug.Log("After" + Hp);
        }

        public void Explode()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            OnExplode?.Invoke();
        }
    }
}
