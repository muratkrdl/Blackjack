using UnityEngine;

namespace Runtime.Utilities
{
    public static class CacheUtilities
    {
#region Vector2
        
        public static readonly Vector2 Zero2 = Vector2.zero;
        public static readonly Vector2 One2 = Vector2.one;

#endregion
        
#region Vector3
        
        public static readonly Vector3 Zero3 = Vector3.zero;
        public static readonly Vector3 One3 = Vector3.one;
        
#endregion

#region LayerMask
        
        public static readonly int PlayerMask = LayerMask.GetMask("Player");
        public static readonly int EnemyAndObstacleMask = LayerMask.GetMask("Enemy", "Obstacle");

#endregion

    }
}
