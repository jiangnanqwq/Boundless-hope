
using UnityEngine;

[CreateAssetMenu(fileName ="Object_Weapon",menuName ="Object/Weapon")]
public class Object_Weapon : ObjectBase
{
    public float attackPoint;
    public float attackRange;
    public Object_Weapon()
    {
        category = ObjectCategory.Weapon;
    }
}
