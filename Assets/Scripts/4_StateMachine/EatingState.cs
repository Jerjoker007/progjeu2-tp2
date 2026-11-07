using UnityEngine;

public class EatingState : BuildingState
{
    protected override void UpdateCharacter()
    {
        vitals.LowerHunger();
    }

    protected override void ManageStateChange()
    {
        if (!vitals.IsHungerBellowTarget) return;

        SetNewDestination();
        stateManager.ChangeCharacterState(CharacterNextState.Moving);
    }
}
