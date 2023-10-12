using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerState", menuName = "State/PlayerState")]
public class PlayerState : ScriptableObject
{
    public Vector3 position { get; set; }
    public bool isOnIce { get; set; }
    public int coinsRequired { get; set; }
    public int coins { get; set; }
    public int level { get; set; }
    public int gobPosition { get; set; }
    public int professorPosition { get; set; }
    public List<GameObject> gobs { get; set; }
    public GameObject spawnerObject { get; set; }
    public bool wings { get; set; }
    public bool bounce { get; set; }
    public bool glue { get; set; }
    public bool spawnGlue { get; set; }
    public bool spawnBounce { get; set; }
    public bool spawnWings { get; set; }
    public Vector3 WingsPosition { get; set; }
    public Vector3 BouncePosition { get; set; }
    public Vector3 GluePosition { get; set; }

}