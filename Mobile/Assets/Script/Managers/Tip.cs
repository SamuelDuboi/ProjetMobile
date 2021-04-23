
using UnityEngine;

[CreateAssetMenu(fileName ="NewTip", menuName ="Tip")]
public class Tip : ScriptableObject
{
    public bool upsideDown;
    public CamDirection camDirection;
    public bool zoom;
    public bool done;
}
