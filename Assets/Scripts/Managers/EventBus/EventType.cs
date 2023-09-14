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

    #endregion

    #region Player HUD
    HUD_UP,
    HUD_DOWN,

    #endregion

    #region Player Inventory
    INVENTORY_TOGGLE,
    INVENTORY_ADDSLOT,
    INVENTORY_REMOVESLOT,
    INVENTORY_PICKUP,
    INVENTORY_SORTPICKUP,
    INVENTORY_ADDITEM,
    INVENTORY_ADDITEMCULL,
    INVENTORY_REMOVEITEM,
    INVENTORY_UPDATE,
    INVENTORY_SLOTUPDATED,
    INVENTORY_ITEMDROPPED,
    //INVENTORY_OPEN,
    //INVENTORY_CLOSE,

    #endregion

}
