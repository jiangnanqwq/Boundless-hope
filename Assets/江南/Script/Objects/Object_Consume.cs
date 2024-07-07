
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Object_Consume", menuName = "Object/Consume")]
public class Object_Consume : ObjectBase
{
    public float hungerRecoverPoint;
    public DogEatAttribute[] dogEat;
    public Object_Consume()
    {
        category = ObjectCategory.Consume;
    }
}
[Serializable]
public class DogEatAttribute
{
    public float trustPoint;
}
