namespace Code
{
    public enum EventType
    {
        // Global ---------------------------------------
        AnyInput,
        ContextChanging,
        Error,
        AppExitRequested,
        StartGameRequested,
        StartGame,
        // ----------------------------------------------
        
        // Gameplay Player --------------------------------
        PlayerHealthChanged,

        // Gameplay Inputs Vehicle ------------------------------
        VehicleThrottleChanged,
        VehicleSteeringChanged,
        // ----------------------------------------------
        
        // Gameplay Inputs Player ------------------------------
        PlayerAttack,
        
        //Loading ---------------------------------------
        CancelLoadingRequested
    }
}