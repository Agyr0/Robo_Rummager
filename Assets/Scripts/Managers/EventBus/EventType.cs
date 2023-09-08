public enum EventType
{
    #region Game State
    GAME_START,
    GAME_OVER,
    LEVEL_START,
    LEVEL_OVER,

    #endregion

    #region Player Controller
    PLAYER_SPAWN,
    PLAYER_DEATH,

    #region PlayerInput
    PLAYER_START_SPRINT,
    PLAYER_STOP_SPRINT,
    PLAYER_DASH,
    #endregion

    #region Shooting
    PLAYER_SHOOT,
    WEAPON_SWITCH,
    DISPLAY_WEAPON,
    #endregion

    #endregion

    #region Testing Events

    TEST_EVENT_0,
    TEST_EVENT_1,
    TEST_EVENT_2

    #endregion
}
