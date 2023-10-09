using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerState", menuName = "State/PlayerState")]
public class PlayerState : ScriptableObject
{
    public Vector3 position { get; set; }
    public bool alive { get; set; }
    public bool grounded { get; set; }
    public bool isOnIce { get; set; }
}