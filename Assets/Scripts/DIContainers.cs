using System.Collections;
using System.Collections.Generic;
using CryoDI;
using UnityEngine;

public class DIContainers : UnityStarter
{
    protected override void SetupContainer(CryoContainer container)
    {
        container.RegisterSingleton<EventEmitter>(LifeTime.Scene);
        container.RegisterSceneObject<TicTacToe>("Application", LifeTime.Scene);

    }
}
