
using UnityEngine;

[CreateAssetMenu(fileName ="Object",menuName ="Object/Object")]
public class ObjectBase : ScriptableObject
{
    public int ID;
    public string objName;
    [TextArea] public string description;
    public Sprite sprite;
    public ObjectCategory category;
    public ObjectBase()
    {
        category = ObjectCategory.Normal;
    }
}
public enum ObjectCategory
{
    Normal,Weapon,Consume
}