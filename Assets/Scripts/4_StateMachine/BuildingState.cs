using UnityEngine;

public class BuildingState : BaseState
{
    void OnEnable()
    {
        character.MakeInvisible();
    }

    void OnDisable()
    {
        character.MakeVisible();
    }
}
