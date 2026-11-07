using UnityEngine;

public class WorkingState : BuildingState
{
    protected override void UpdateCharacter()
    {
        vitals.RaiseHunger();
        vitals.RaiseLoneliness();
        vitals.RaiseSleepiness();
    }

    protected override void ManageStateChange()
    {
        if (!vitals.IsHungerAboveThreshold && !vitals.IsLonelinessAboveThreshold && !vitals.IsSleepinessAboveThreshold) return;

        SetNewDestination();
        stateManager.ChangeCharacterState(CharacterNextState.Moving);
    }
}
