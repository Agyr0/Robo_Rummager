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
    TOGGLE_SCANNER,
    SEND_DETECTION_SPHERE,
    #endregion

    #region Shooting
    PLAYER_SHOOT,
    WEAPON_SWITCH,
    DISPLAY_WEAPON,
    PLAYER_RELOAD,
    LOW_AMMO,
    SWING_WRENCH,
    PUNCH_HANDS,
    #endregion

    #endregion

    #region Player HUD
    //Togles the visibility of the players fanny pack UI,
    //defaulting to the the inventory screen.
    INVENTORY_TOGGLE,
    BULLETINBOARD_INTERACT,
    TOGGLE_INTERACT_HOVER,
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
    PLAYER_CONTRACTUPDATE,
    PLAYER_ADDCONTRACT,

    #endregion

    #region Bulletin Board
    BOARD_ADDCONTRACT,
    BOARD_CONTRACTUPDATE,
    CONTRACTACCEPTED,
    #endregion

    #region Workshop
    REFRESH_RESOURCES,

    #endregion

    #region Testing Events

    TEST_EVENT_0,
    TEST_EVENT_1,
    TEST_EVENT_2

    #endregion

}