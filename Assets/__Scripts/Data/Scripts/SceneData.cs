using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SceneData : MonoBehaviour
{
    [Header("Camera")]
    public CameraController CameraController;

    [Header("MonoEntities")]
    public List<MonoEntity> MonoEntities;
    public List<MonoEntity> Conveyors;

    public PrefabFactory PrefabFactory;
}
