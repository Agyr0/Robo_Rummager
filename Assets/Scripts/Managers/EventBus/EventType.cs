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
    //Togles the visibility of the players fanny pack UI,
    //defaulting to the the inventory screen.
    INVENTORY_TOGGLE,
    BULLETINBOARD_INTERACT,
    BULLETINBOARD_EXIT,

    #endregion

    #region Player Inventory
    //Enables an available inventory slot.
    INVENTORY_ADDSLOT,
    //Disables the last enabled inventory slot.
    INVENTORY_REMOVESLOT,
    //Adds the gameobject to an item reference list.
    INVENTORY_PICKUP,
    //Adds items from the reference list to the player inventory
    INVENTORY_SORTPICKUP,
    INVENTORY_ADDITEM,
    INVENTORY_ADDITEMCULL,
    INVENTORY_REMOVEITEM,
    INVENTORY_UPDATE,
    INVENTORY_SLOTUPDATED,
    INVENTORY_ITEMDROPPED,

    #endregion

    #region Bulletin Board
    BOARD_ADDCONTRACT,

    #endregion

}
