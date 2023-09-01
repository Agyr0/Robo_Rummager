using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using System.Configuration;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class EventTester : EditorWindow
{
    EventType selectedEvent;

    public List<ParamTypes> eventParams;




    [MenuItem("Tools/Programming/Event Tester")]
    public static void ShowWindow()
    {
        GetWindow<EventTester>("Event Tester");
    }


    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(20);

        GUILayout.Label("Selected Event", EditorStyles.boldLabel);
        selectedEvent = (EventType)EditorGUILayout.EnumPopup("Event", selectedEvent);
        
        GUILayout.Space(20);
        EditorGUI.indentLevel++;
        GUILayout.Label("Parameters", EditorStyles.boldLabel);

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty("eventParams");

        EditorGUILayout.PropertyField(property, true);
        so.ApplyModifiedProperties();
        GUILayout.Space(20);

        foreach (ParamTypes param in eventParams)
        {
            RenderParamFields(param);
        }


        GUILayout.Space(50);
        

        GUILayout.Space(20);
        if (GUILayout.Button("Publish Event", GUILayout.Height(30), GUILayout.Width(position.width - 5)))
        {
            if(eventParams.Count <= 0)
                EventBus.Publish(selectedEvent);
            else if(eventParams.Count == 1)
            {
                switch (eventParams[0].type)
                {
                    case ParamTypes.Type.Bool:
                        EventBus.Publish<bool>(selectedEvent, eventParams[0].BoolParam);
                        break;
                    case ParamTypes.Type.Int:
                        EventBus.Publish<int>(selectedEvent, eventParams[0].IntParam);
                        break;
                    case ParamTypes.Type.Float:
                        EventBus.Publish<float>(selectedEvent, eventParams[0].FloatParam);
                        break;
                    case ParamTypes.Type.Vec2:
                        EventBus.Publish<Vector2>(selectedEvent, eventParams[0].Vec2Param);
                        break;
                    case ParamTypes.Type.Vec3:
                        EventBus.Publish<Vector3>(selectedEvent, eventParams[0].Vec3Param);
                        break;
                    case ParamTypes.Type.GameObj:
                        EventBus.Publish<GameObject>(selectedEvent, eventParams[0].GameObjectParam);
                        break;
                    case ParamTypes.Type.String:
                        EventBus.Publish<string>(selectedEvent, eventParams[0].StringParam);
                        break;
                    default:
                        break;
                }
            }
            else if (eventParams.Count == 2)
            {
                switch (eventParams[0].type)
                {
                    case ParamTypes.Type.Bool:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                EventBus.Publish<bool, bool>(selectedEvent, eventParams[0].BoolParam, eventParams[1].BoolParam);
                                break;
                            case ParamTypes.Type.Int:
                                EventBus.Publish<bool, int>(selectedEvent, eventParams[0].BoolParam, eventParams[1].IntParam);
                                break;
                            case ParamTypes.Type.Float:
                                EventBus.Publish<bool, float>(selectedEvent, eventParams[0].BoolParam, eventParams[1].FloatParam);
                                break;
                            case ParamTypes.Type.Vec2:
                                EventBus.Publish<bool, Vector2>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec2Param);
                                break;
                            case ParamTypes.Type.Vec3:
                                EventBus.Publish<bool, Vector3>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec3Param);
                                break;
                            case ParamTypes.Type.GameObj:
                                EventBus.Publish<bool, GameObject>(selectedEvent, eventParams[0].BoolParam, eventParams[1].GameObjectParam);
                                break;
                            case ParamTypes.Type.String:
                                EventBus.Publish<bool, string>(selectedEvent, eventParams[0].BoolParam, eventParams[1].StringParam);
                                break;
                            default:
                                break;
                        }
                        break;
                    case ParamTypes.Type.Int:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                EventBus.Publish<int, bool>(selectedEvent, eventParams[0].IntParam, eventParams[1].BoolParam);
                                break;
                            case ParamTypes.Type.Int:
                                EventBus.Publish<int, int>(selectedEvent, eventParams[0].IntParam, eventParams[1].IntParam);
                                break;
                            case ParamTypes.Type.Float:
                                EventBus.Publish<int, float>(selectedEvent, eventParams[0].IntParam, eventParams[1].FloatParam);
                                break;
                            case ParamTypes.Type.Vec2:
                                EventBus.Publish<int, Vector2>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec2Param);
                                break;
                            case ParamTypes.Type.Vec3:
                                EventBus.Publish<int, Vector3>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec3Param);
                                break;
                            case ParamTypes.Type.GameObj:
                                EventBus.Publish<int, GameObject>(selectedEvent, eventParams[0].IntParam, eventParams[1].GameObjectParam);
                                break;
                            case ParamTypes.Type.String:
                                EventBus.Publish<int, string>(selectedEvent, eventParams[0].IntParam, eventParams[1].StringParam);
                                break;
                            default:
                                break;
                        }
                        break;
                    case ParamTypes.Type.Float:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                EventBus.Publish<float, bool>(selectedEvent, eventParams[0].FloatParam, eventParams[1].BoolParam);
                                break;
                            case ParamTypes.Type.Int:
                                EventBus.Publish<float, int>(selectedEvent, eventParams[0].FloatParam, eventParams[1].IntParam);
                                break;
                            case ParamTypes.Type.Float:
                                EventBus.Publish<float, float>(selectedEvent, eventParams[0].FloatParam, eventParams[1].FloatParam);
                                break;
                            case ParamTypes.Type.Vec2:
                                EventBus.Publish<float, Vector2>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec2Param);
                                break;
                            case ParamTypes.Type.Vec3:
                                EventBus.Publish<float, Vector3>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec3Param);
                                break;
                            case ParamTypes.Type.GameObj:
                                EventBus.Publish<float, GameObject>(selectedEvent, eventParams[0].FloatParam, eventParams[1].GameObjectParam);
                                break;
                            case ParamTypes.Type.String:
                                EventBus.Publish<float, string>(selectedEvent, eventParams[0].FloatParam, eventParams[1].StringParam);
                                break;
                            default:
                                break;
                        }
                        break;
                    case ParamTypes.Type.Vec2:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                EventBus.Publish<Vector2, bool>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].BoolParam);
                                break;
                            case ParamTypes.Type.Int:
                                EventBus.Publish<Vector2, int>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].IntParam);
                                break;
                            case ParamTypes.Type.Float:
                                EventBus.Publish<Vector2, float>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].FloatParam);
                                break;
                            case ParamTypes.Type.Vec2:
                                EventBus.Publish<Vector2, Vector2>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec2Param);
                                break;
                            case ParamTypes.Type.Vec3:
                                EventBus.Publish<Vector2, Vector3>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec3Param);
                                break;
                            case ParamTypes.Type.GameObj:
                                EventBus.Publish<Vector2, GameObject>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].GameObjectParam);
                                break;
                            case ParamTypes.Type.String:
                                EventBus.Publish<Vector2, string>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].StringParam);
                                break;
                            default:
                                break;
                        }
                        break;
                    case ParamTypes.Type.Vec3:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                EventBus.Publish<Vector3, bool>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].BoolParam);
                                break;
                            case ParamTypes.Type.Int:
                                EventBus.Publish<Vector3, int>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].IntParam);
                                break;
                            case ParamTypes.Type.Float:
                                EventBus.Publish<Vector3, float>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].FloatParam);
                                break;
                            case ParamTypes.Type.Vec2:
                                EventBus.Publish<Vector3, Vector2>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec2Param);
                                break;
                            case ParamTypes.Type.Vec3:
                                EventBus.Publish<Vector3, Vector3>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec3Param);
                                break;
                            case ParamTypes.Type.GameObj:
                                EventBus.Publish<Vector3, GameObject>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].GameObjectParam);
                                break;
                            case ParamTypes.Type.String:
                                EventBus.Publish<Vector3, string>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].StringParam);
                                break;
                            default:
                                break;
                        }
                        break;
                    case ParamTypes.Type.GameObj:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                EventBus.Publish<GameObject, bool>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].BoolParam);
                                break;
                            case ParamTypes.Type.Int:
                                EventBus.Publish<GameObject, int>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].IntParam);
                                break;
                            case ParamTypes.Type.Float:
                                EventBus.Publish<GameObject, float>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].FloatParam);
                                break;
                            case ParamTypes.Type.Vec2:
                                EventBus.Publish<GameObject, Vector2>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec2Param);
                                break;
                            case ParamTypes.Type.Vec3:
                                EventBus.Publish<GameObject, Vector3>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec3Param);
                                break;
                            case ParamTypes.Type.GameObj:
                                EventBus.Publish<GameObject, GameObject>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].GameObjectParam);
                                break;
                            case ParamTypes.Type.String:
                                EventBus.Publish<GameObject, string>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].StringParam);
                                break;
                            default:
                                break;
                        }
                        break;
                    case ParamTypes.Type.String:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                EventBus.Publish<string, bool>(selectedEvent, eventParams[0].StringParam, eventParams[1].BoolParam);
                                break;
                            case ParamTypes.Type.Int:
                                EventBus.Publish<string, int>(selectedEvent, eventParams[0].StringParam, eventParams[1].IntParam);
                                break;
                            case ParamTypes.Type.Float:
                                EventBus.Publish<string, float>(selectedEvent, eventParams[0].StringParam, eventParams[1].FloatParam);
                                break;
                            case ParamTypes.Type.Vec2:
                                EventBus.Publish<string, Vector2>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec2Param);
                                break;
                            case ParamTypes.Type.Vec3:
                                EventBus.Publish<string, Vector3>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec3Param);
                                break;
                            case ParamTypes.Type.GameObj:
                                EventBus.Publish<string, GameObject>(selectedEvent, eventParams[0].StringParam, eventParams[1].GameObjectParam);
                                break;
                            case ParamTypes.Type.String:
                                EventBus.Publish<string, string>(selectedEvent, eventParams[0].StringParam, eventParams[1].StringParam);
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
            else if (eventParams.Count == 3)
            {
                switch (eventParams[0].type)
                {
                    case ParamTypes.Type.Bool:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<bool, bool, bool>(selectedEvent, eventParams[0].BoolParam, eventParams[1].BoolParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<bool, bool, int>(selectedEvent, eventParams[0].BoolParam, eventParams[1].BoolParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<bool, bool, float>(selectedEvent, eventParams[0].BoolParam, eventParams[1].BoolParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<bool, bool, Vector2>(selectedEvent, eventParams[0].BoolParam, eventParams[1].BoolParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<bool, bool, Vector3>(selectedEvent, eventParams[0].BoolParam, eventParams[1].BoolParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<bool, bool, GameObject>(selectedEvent, eventParams[0].BoolParam, eventParams[1].BoolParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<bool, bool, string>(selectedEvent, eventParams[0].BoolParam, eventParams[1].BoolParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Int:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<bool, int, bool>(selectedEvent, eventParams[0].BoolParam, eventParams[1].IntParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<bool, int, int>(selectedEvent, eventParams[0].BoolParam, eventParams[1].IntParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<bool, int, float>(selectedEvent, eventParams[0].BoolParam, eventParams[1].IntParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<bool, int, Vector2>(selectedEvent, eventParams[0].BoolParam, eventParams[1].IntParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<bool, int, Vector3>(selectedEvent, eventParams[0].BoolParam, eventParams[1].IntParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<bool, int, GameObject>(selectedEvent, eventParams[0].BoolParam, eventParams[1].IntParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<bool, int, string>(selectedEvent, eventParams[0].BoolParam, eventParams[1].IntParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Float:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<bool, float, bool>(selectedEvent, eventParams[0].BoolParam, eventParams[1].FloatParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<bool, float, int>(selectedEvent, eventParams[0].BoolParam, eventParams[1].FloatParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<bool, float, float>(selectedEvent, eventParams[0].BoolParam, eventParams[1].FloatParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<bool, float, Vector2>(selectedEvent, eventParams[0].BoolParam, eventParams[1].FloatParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<bool, float, Vector3>(selectedEvent, eventParams[0].BoolParam, eventParams[1].FloatParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<bool, float, GameObject>(selectedEvent, eventParams[0].BoolParam, eventParams[1].FloatParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<bool, float, string>(selectedEvent, eventParams[0].BoolParam, eventParams[1].FloatParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec2:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<bool, Vector2, bool>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec2Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<bool, Vector2, int>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec2Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<bool, Vector2, float>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec2Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<bool, Vector2, Vector2>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec2Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<bool, Vector2, Vector3>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec2Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<bool, Vector2, GameObject>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec2Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<bool, Vector2, string>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec2Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec3:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<bool, Vector3, bool>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec3Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<bool, Vector3, int>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec3Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<bool, Vector3, float>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec3Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<bool, Vector3, Vector2>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec3Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<bool, Vector3, Vector3>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec3Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<bool, Vector3, GameObject>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec3Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<bool, Vector3, string>(selectedEvent, eventParams[0].BoolParam, eventParams[1].Vec3Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.GameObj:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<bool, GameObject, bool>(selectedEvent, eventParams[0].BoolParam, eventParams[1].GameObjectParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<bool, GameObject, int>(selectedEvent, eventParams[0].BoolParam, eventParams[1].GameObjectParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<bool, GameObject, float>(selectedEvent, eventParams[0].BoolParam, eventParams[1].GameObjectParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<bool, GameObject, Vector2>(selectedEvent, eventParams[0].BoolParam, eventParams[1].GameObjectParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<bool, GameObject, Vector3>(selectedEvent, eventParams[0].BoolParam, eventParams[1].GameObjectParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<bool, GameObject, GameObject>(selectedEvent, eventParams[0].BoolParam, eventParams[1].GameObjectParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<bool, GameObject, string>(selectedEvent, eventParams[0].BoolParam, eventParams[1].GameObjectParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.String:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<bool, string, bool>(selectedEvent, eventParams[0].BoolParam, eventParams[1].StringParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<bool, string, int>(selectedEvent, eventParams[0].BoolParam, eventParams[1].StringParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<bool, string, float>(selectedEvent, eventParams[0].BoolParam, eventParams[1].StringParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<bool, string, Vector2>(selectedEvent, eventParams[0].BoolParam, eventParams[1].StringParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<bool, string, Vector3>(selectedEvent, eventParams[0].BoolParam, eventParams[1].StringParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<bool, string, GameObject>(selectedEvent, eventParams[0].BoolParam, eventParams[1].StringParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<bool, string, string>(selectedEvent, eventParams[0].BoolParam, eventParams[1].StringParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                    case ParamTypes.Type.Int:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<int, bool, bool>(selectedEvent, eventParams[0].IntParam, eventParams[1].BoolParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<int, bool, int>(selectedEvent, eventParams[0].IntParam, eventParams[1].BoolParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<int, bool, float>(selectedEvent, eventParams[0].IntParam, eventParams[1].BoolParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<int, bool, Vector2>(selectedEvent, eventParams[0].IntParam, eventParams[1].BoolParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<int, bool, Vector3>(selectedEvent, eventParams[0].IntParam, eventParams[1].BoolParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<int, bool, GameObject>(selectedEvent, eventParams[0].IntParam, eventParams[1].BoolParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<int, bool, string>(selectedEvent, eventParams[0].IntParam, eventParams[1].BoolParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Int:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<int, int, bool>(selectedEvent, eventParams[0].IntParam, eventParams[1].IntParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<int, int, int>(selectedEvent, eventParams[0].IntParam, eventParams[1].IntParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<int, int, float>(selectedEvent, eventParams[0].IntParam, eventParams[1].IntParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<int, int, Vector2>(selectedEvent, eventParams[0].IntParam, eventParams[1].IntParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<int, int, Vector3>(selectedEvent, eventParams[0].IntParam, eventParams[1].IntParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<int, int, GameObject>(selectedEvent, eventParams[0].IntParam, eventParams[1].IntParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<int, int, string>(selectedEvent, eventParams[0].IntParam, eventParams[1].IntParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Float:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<int, float, bool>(selectedEvent, eventParams[0].IntParam, eventParams[1].FloatParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<int, float, int>(selectedEvent, eventParams[0].IntParam, eventParams[1].FloatParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<int, float, float>(selectedEvent, eventParams[0].IntParam, eventParams[1].FloatParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<int, float, Vector2>(selectedEvent, eventParams[0].IntParam, eventParams[1].FloatParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<int, float, Vector3>(selectedEvent, eventParams[0].IntParam, eventParams[1].FloatParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<int, float, GameObject>(selectedEvent, eventParams[0].IntParam, eventParams[1].FloatParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<int, float, string>(selectedEvent, eventParams[0].IntParam, eventParams[1].FloatParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec2:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<int, Vector2, bool>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec2Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<int, Vector2, int>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec2Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<int, Vector2, float>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec2Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<int, Vector2, Vector2>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec2Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<int, Vector2, Vector3>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec2Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<int, Vector2, GameObject>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec2Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<int, Vector2, string>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec2Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec3:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<int, Vector3, bool>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec3Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<int, Vector3, int>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec3Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<int, Vector3, float>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec3Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<int, Vector3, Vector2>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec3Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<int, Vector3, Vector3>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec3Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<int, Vector3, GameObject>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec3Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<int, Vector3, string>(selectedEvent, eventParams[0].IntParam, eventParams[1].Vec3Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.GameObj:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<int, GameObject, bool>(selectedEvent, eventParams[0].IntParam, eventParams[1].GameObjectParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<int, GameObject, int>(selectedEvent, eventParams[0].IntParam, eventParams[1].GameObjectParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<int, GameObject, float>(selectedEvent, eventParams[0].IntParam, eventParams[1].GameObjectParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<int, GameObject, Vector2>(selectedEvent, eventParams[0].IntParam, eventParams[1].GameObjectParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<int, GameObject, Vector3>(selectedEvent, eventParams[0].IntParam, eventParams[1].GameObjectParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<int, GameObject, GameObject>(selectedEvent, eventParams[0].IntParam, eventParams[1].GameObjectParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<int, GameObject, string>(selectedEvent, eventParams[0].IntParam, eventParams[1].GameObjectParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.String:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<int, string, bool>(selectedEvent, eventParams[0].IntParam, eventParams[1].StringParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<int, string, int>(selectedEvent, eventParams[0].IntParam, eventParams[1].StringParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<int, string, float>(selectedEvent, eventParams[0].IntParam, eventParams[1].StringParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<int, string, Vector2>(selectedEvent, eventParams[0].IntParam, eventParams[1].StringParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<int, string, Vector3>(selectedEvent, eventParams[0].IntParam, eventParams[1].StringParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<int, string, GameObject>(selectedEvent, eventParams[0].IntParam, eventParams[1].StringParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<int, string, string>(selectedEvent, eventParams[0].IntParam, eventParams[1].StringParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                    case ParamTypes.Type.Float:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<float, bool, bool>(selectedEvent, eventParams[0].FloatParam, eventParams[1].BoolParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<float, bool, int>(selectedEvent, eventParams[0].FloatParam, eventParams[1].BoolParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<float, bool, float>(selectedEvent, eventParams[0].FloatParam, eventParams[1].BoolParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<float, bool, Vector2>(selectedEvent, eventParams[0].FloatParam, eventParams[1].BoolParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<float, bool, Vector3>(selectedEvent, eventParams[0].FloatParam, eventParams[1].BoolParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<float, bool, GameObject>(selectedEvent, eventParams[0].FloatParam, eventParams[1].BoolParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<float, bool, string>(selectedEvent, eventParams[0].FloatParam, eventParams[1].BoolParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Int:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<float, int, bool>(selectedEvent, eventParams[0].FloatParam, eventParams[1].IntParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<float, int, int>(selectedEvent, eventParams[0].FloatParam, eventParams[1].IntParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<float, int, float>(selectedEvent, eventParams[0].FloatParam, eventParams[1].IntParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<float, int, Vector2>(selectedEvent, eventParams[0].FloatParam, eventParams[1].IntParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<float, int, Vector3>(selectedEvent, eventParams[0].FloatParam, eventParams[1].IntParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<float, int, GameObject>(selectedEvent, eventParams[0].FloatParam, eventParams[1].IntParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<float, int, string>(selectedEvent, eventParams[0].FloatParam, eventParams[1].IntParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Float:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<float, float, bool>(selectedEvent, eventParams[0].FloatParam, eventParams[1].FloatParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<float, float, int>(selectedEvent, eventParams[0].FloatParam, eventParams[1].FloatParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<float, float, float>(selectedEvent, eventParams[0].FloatParam, eventParams[1].FloatParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<float, float, Vector2>(selectedEvent, eventParams[0].FloatParam, eventParams[1].FloatParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<float, float, Vector3>(selectedEvent, eventParams[0].FloatParam, eventParams[1].FloatParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<float, float, GameObject>(selectedEvent, eventParams[0].FloatParam, eventParams[1].FloatParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<float, float, string>(selectedEvent, eventParams[0].FloatParam, eventParams[1].FloatParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec2:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<float, Vector2, bool>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec2Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<float, Vector2, int>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec2Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<float, Vector2, float>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec2Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<float, Vector2, Vector2>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec2Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<float, Vector2, Vector3>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec2Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<float, Vector2, GameObject>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec2Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<float, Vector2, string>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec2Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec3:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<float, Vector3, bool>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec3Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<float, Vector3, int>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec3Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<float, Vector3, float>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec3Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<float, Vector3, Vector2>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec3Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<float, Vector3, Vector3>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec3Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<float, Vector3, GameObject>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec3Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<float, Vector3, string>(selectedEvent, eventParams[0].FloatParam, eventParams[1].Vec3Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.GameObj:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<float, GameObject, bool>(selectedEvent, eventParams[0].FloatParam, eventParams[1].GameObjectParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<float, GameObject, int>(selectedEvent, eventParams[0].FloatParam, eventParams[1].GameObjectParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<float, GameObject, float>(selectedEvent, eventParams[0].FloatParam, eventParams[1].GameObjectParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<float, GameObject, Vector2>(selectedEvent, eventParams[0].FloatParam, eventParams[1].GameObjectParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<float, GameObject, Vector3>(selectedEvent, eventParams[0].FloatParam, eventParams[1].GameObjectParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<float, GameObject, GameObject>(selectedEvent, eventParams[0].FloatParam, eventParams[1].GameObjectParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<float, GameObject, string>(selectedEvent, eventParams[0].FloatParam, eventParams[1].GameObjectParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.String:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<float, string, bool>(selectedEvent, eventParams[0].FloatParam, eventParams[1].StringParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<float, string, int>(selectedEvent, eventParams[0].FloatParam, eventParams[1].StringParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<float, string, float>(selectedEvent, eventParams[0].FloatParam, eventParams[1].StringParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<float, string, Vector2>(selectedEvent, eventParams[0].FloatParam, eventParams[1].StringParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<float, string, Vector3>(selectedEvent, eventParams[0].FloatParam, eventParams[1].StringParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<float, string, GameObject>(selectedEvent, eventParams[0].FloatParam, eventParams[1].StringParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<float, string, string>(selectedEvent, eventParams[0].FloatParam, eventParams[1].StringParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                    case ParamTypes.Type.Vec2:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector2, bool, bool>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].BoolParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector2, bool, int>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].BoolParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector2, bool, float>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].BoolParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector2, bool, Vector2>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].BoolParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector2, bool, Vector3>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].BoolParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector2, bool, GameObject>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].BoolParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector2, bool, string>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].BoolParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Int:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector2, int, bool>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].IntParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector2, int, int>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].IntParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector2, int, float>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].IntParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector2, int, Vector2>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].IntParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector2, int, Vector3>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].IntParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector2, int, GameObject>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].IntParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector2, int, string>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].IntParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Float:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector2, float, bool>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].FloatParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector2, float, int>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].FloatParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector2, float, float>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].FloatParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector2, float, Vector2>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].FloatParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector2, float, Vector3>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].FloatParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector2, float, GameObject>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].FloatParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector2, float, string>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].FloatParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec2:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector2, Vector2, bool>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec2Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector2, Vector2, int>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec2Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector2, Vector2, float>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec2Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector2, Vector2, Vector2>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec2Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector2, Vector2, Vector3>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec2Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector2, Vector2, GameObject>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec2Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector2, Vector2, string>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec2Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec3:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector2, Vector3, bool>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec3Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector2, Vector3, int>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec3Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector2, Vector3, float>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec3Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector2, Vector3, Vector2>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec3Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector2, Vector3, Vector3>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec3Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector2, Vector3, GameObject>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec3Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector2, Vector3, string>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].Vec3Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.GameObj:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector2, GameObject, bool>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].GameObjectParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector2, GameObject, int>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].GameObjectParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector2, GameObject, float>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].GameObjectParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector2, GameObject, Vector2>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].GameObjectParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector2, GameObject, Vector3>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].GameObjectParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector2, GameObject, GameObject>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].GameObjectParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector2, GameObject, string>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].GameObjectParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.String:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector2, string, bool>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].StringParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector2, string, int>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].StringParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector2, string, float>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].StringParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector2, string, Vector2>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].StringParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector2, string, Vector3>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].StringParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector2, string, GameObject>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].StringParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector2, string, string>(selectedEvent, eventParams[0].Vec2Param, eventParams[1].StringParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                    case ParamTypes.Type.Vec3:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector3, bool, bool>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].BoolParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector3, bool, int>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].BoolParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector3, bool, float>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].BoolParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector3, bool, Vector2>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].BoolParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector3, bool, Vector3>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].BoolParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector3, bool, GameObject>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].BoolParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector3, bool, string>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].BoolParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Int:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector3, int, bool>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].IntParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector3, int, int>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].IntParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector3, int, float>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].IntParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector3, int, Vector2>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].IntParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector3, int, Vector3>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].IntParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector3, int, GameObject>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].IntParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector3, int, string>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].IntParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Float:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector3, float, bool>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].FloatParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector3, float, int>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].FloatParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector3, float, float>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].FloatParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector3, float, Vector2>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].FloatParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector3, float, Vector3>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].FloatParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector3, float, GameObject>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].FloatParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector3, float, string>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].FloatParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec2:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector3, Vector2, bool>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec2Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector3, Vector2, int>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec2Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector3, Vector2, float>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec2Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector3, Vector2, Vector2>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec2Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector3, Vector2, Vector3>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec2Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector3, Vector2, GameObject>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec2Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector3, Vector2, string>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec2Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec3:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector3, Vector3, bool>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec3Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector3, Vector3, int>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec3Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector3, Vector3, float>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec3Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector3, Vector3, Vector2>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec3Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector3, Vector3, Vector3>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec3Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector3, Vector3, GameObject>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec3Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector3, Vector3, string>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].Vec3Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.GameObj:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector3, GameObject, bool>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].GameObjectParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector3, GameObject, int>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].GameObjectParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector3, GameObject, float>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].GameObjectParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector3, GameObject, Vector2>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].GameObjectParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector3, GameObject, Vector3>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].GameObjectParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector3, GameObject, GameObject>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].GameObjectParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector3, GameObject, string>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].GameObjectParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.String:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<Vector3, string, bool>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].StringParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<Vector3, string, int>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].StringParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<Vector3, string, float>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].StringParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<Vector3, string, Vector2>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].StringParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<Vector3, string, Vector3>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].StringParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<Vector3, string, GameObject>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].StringParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<Vector3, string, string>(selectedEvent, eventParams[0].Vec3Param, eventParams[1].StringParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                    case ParamTypes.Type.GameObj:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<GameObject, bool, bool>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].BoolParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<GameObject, bool, int>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].BoolParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<GameObject, bool, float>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].BoolParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<GameObject, bool, Vector2>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].BoolParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<GameObject, bool, Vector3>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].BoolParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<GameObject, bool, GameObject>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].BoolParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<GameObject, bool, string>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].BoolParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Int:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<GameObject, int, bool>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].IntParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<GameObject, int, int>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].IntParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<GameObject, int, float>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].IntParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<GameObject, int, Vector2>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].IntParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<GameObject, int, Vector3>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].IntParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<GameObject, int, GameObject>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].IntParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<GameObject, int, string>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].IntParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Float:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<GameObject, float, bool>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].FloatParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<GameObject, float, int>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].FloatParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<GameObject, float, float>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].FloatParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<GameObject, float, Vector2>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].FloatParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<GameObject, float, Vector3>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].FloatParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<GameObject, float, GameObject>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].FloatParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<GameObject, float, string>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].FloatParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec2:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<GameObject, Vector2, bool>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec2Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<GameObject, Vector2, int>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec2Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<GameObject, Vector2, float>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec2Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<GameObject, Vector2, Vector2>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec2Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<GameObject, Vector2, Vector3>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec2Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<GameObject, Vector2, GameObject>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec2Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<GameObject, Vector2, string>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec2Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec3:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<GameObject, Vector3, bool>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec3Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<GameObject, Vector3, int>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec3Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<GameObject, Vector3, float>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec3Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<GameObject, Vector3, Vector2>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec3Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<GameObject, Vector3, Vector3>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec3Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<GameObject, Vector3, GameObject>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec3Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<GameObject, Vector3, string>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].Vec3Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.GameObj:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<GameObject, GameObject, bool>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].GameObjectParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<GameObject, GameObject, int>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].GameObjectParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<GameObject, GameObject, float>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].GameObjectParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<GameObject, GameObject, Vector2>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].GameObjectParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<GameObject, GameObject, Vector3>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].GameObjectParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<GameObject, GameObject, GameObject>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].GameObjectParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<GameObject, GameObject, string>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].GameObjectParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.String:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<GameObject, string, bool>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].StringParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<GameObject, string, int>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].StringParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<GameObject, string, float>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].StringParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<GameObject, string, Vector2>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].StringParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<GameObject, string, Vector3>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].StringParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<GameObject, string, GameObject>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].StringParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<GameObject, string, string>(selectedEvent, eventParams[0].GameObjectParam, eventParams[1].StringParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                    case ParamTypes.Type.String:
                        switch (eventParams[1].type)
                        {
                            case ParamTypes.Type.Bool:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<string, bool, bool>(selectedEvent, eventParams[0].StringParam, eventParams[1].BoolParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<string, bool, int>(selectedEvent, eventParams[0].StringParam, eventParams[1].BoolParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<string, bool, float>(selectedEvent, eventParams[0].StringParam, eventParams[1].BoolParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<string, bool, Vector2>(selectedEvent, eventParams[0].StringParam, eventParams[1].BoolParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<string, bool, Vector3>(selectedEvent, eventParams[0].StringParam, eventParams[1].BoolParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<string, bool, GameObject>(selectedEvent, eventParams[0].StringParam, eventParams[1].BoolParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<string, bool, string>(selectedEvent, eventParams[0].StringParam, eventParams[1].BoolParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Int:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<string, int, bool>(selectedEvent, eventParams[0].StringParam, eventParams[1].IntParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<string, int, int>(selectedEvent, eventParams[0].StringParam, eventParams[1].IntParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<string, int, float>(selectedEvent, eventParams[0].StringParam, eventParams[1].IntParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<string, int, Vector2>(selectedEvent, eventParams[0].StringParam, eventParams[1].IntParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<string, int, Vector3>(selectedEvent, eventParams[0].StringParam, eventParams[1].IntParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<string, int, GameObject>(selectedEvent, eventParams[0].StringParam, eventParams[1].IntParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<string, int, string>(selectedEvent, eventParams[0].StringParam, eventParams[1].IntParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Float:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<string, float, bool>(selectedEvent, eventParams[0].StringParam, eventParams[1].FloatParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<string, float, int>(selectedEvent, eventParams[0].StringParam, eventParams[1].FloatParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<string, float, float>(selectedEvent, eventParams[0].StringParam, eventParams[1].FloatParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<string, float, Vector2>(selectedEvent, eventParams[0].StringParam, eventParams[1].FloatParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<string, float, Vector3>(selectedEvent, eventParams[0].StringParam, eventParams[1].FloatParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<string, float, GameObject>(selectedEvent, eventParams[0].StringParam, eventParams[1].FloatParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<string, float, string>(selectedEvent, eventParams[0].StringParam, eventParams[1].FloatParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec2:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<string, Vector2, bool>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec2Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<string, Vector2, int>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec2Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<string, Vector2, float>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec2Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<string, Vector2, Vector2>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec2Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<string, Vector2, Vector3>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec2Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<string, Vector2, GameObject>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec2Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<string, Vector2, string>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec2Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.Vec3:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<string, Vector3, bool>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec3Param, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<string, Vector3, int>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec3Param, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<string, Vector3, float>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec3Param, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<string, Vector3, Vector2>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec3Param, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<string, Vector3, Vector3>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec3Param, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<string, Vector3, GameObject>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec3Param, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<string, Vector3, string>(selectedEvent, eventParams[0].StringParam, eventParams[1].Vec3Param, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.GameObj:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<string, GameObject, bool>(selectedEvent, eventParams[0].StringParam, eventParams[1].GameObjectParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<string, GameObject, int>(selectedEvent, eventParams[0].StringParam, eventParams[1].GameObjectParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<string, GameObject, float>(selectedEvent, eventParams[0].StringParam, eventParams[1].GameObjectParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<string, GameObject, Vector2>(selectedEvent, eventParams[0].StringParam, eventParams[1].GameObjectParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<string, GameObject, Vector3>(selectedEvent, eventParams[0].StringParam, eventParams[1].GameObjectParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<string, GameObject, GameObject>(selectedEvent, eventParams[0].StringParam, eventParams[1].GameObjectParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<string, GameObject, string>(selectedEvent, eventParams[0].StringParam, eventParams[1].GameObjectParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ParamTypes.Type.String:
                                switch (eventParams[2].type)
                                {
                                    case ParamTypes.Type.Bool:
                                        EventBus.Publish<string, string, bool>(selectedEvent, eventParams[0].StringParam, eventParams[1].StringParam, eventParams[2].BoolParam);
                                        break;
                                    case ParamTypes.Type.Int:
                                        EventBus.Publish<string, string, int>(selectedEvent, eventParams[0].StringParam, eventParams[1].StringParam, eventParams[2].IntParam);
                                        break;
                                    case ParamTypes.Type.Float:
                                        EventBus.Publish<string, string, float>(selectedEvent, eventParams[0].StringParam, eventParams[1].StringParam, eventParams[2].FloatParam);
                                        break;
                                    case ParamTypes.Type.Vec2:
                                        EventBus.Publish<string, string, Vector2>(selectedEvent, eventParams[0].StringParam, eventParams[1].StringParam, eventParams[2].Vec2Param);
                                        break;
                                    case ParamTypes.Type.Vec3:
                                        EventBus.Publish<string, string, Vector3>(selectedEvent, eventParams[0].StringParam, eventParams[1].StringParam, eventParams[2].Vec3Param);
                                        break;
                                    case ParamTypes.Type.GameObj:
                                        EventBus.Publish<string, string, GameObject>(selectedEvent, eventParams[0].StringParam, eventParams[1].StringParam, eventParams[2].GameObjectParam);
                                        break;
                                    case ParamTypes.Type.String:
                                        EventBus.Publish<string, string, string>(selectedEvent, eventParams[0].StringParam, eventParams[1].StringParam, eventParams[2].StringParam);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }


            Debug.Log("Published event " + selectedEvent);
        }

        GUILayout.EndVertical();
    }


    private void RenderParamFields(ParamTypes param)
    {
        param.type = (ParamTypes.Type)EditorGUILayout.EnumPopup("Param Type", param.type);

        switch (param.type)
        {
            case ParamTypes.Type.Bool:
                param.BoolParam = EditorGUILayout.Toggle("Bool Param", param.BoolParam);
                break;
            case ParamTypes.Type.Int:
                param.IntParam = EditorGUILayout.IntField("Int Param", param.IntParam);
                break;
            case ParamTypes.Type.Float:
                param.FloatParam = EditorGUILayout.FloatField("Float Param", param.FloatParam);
                break;
            case ParamTypes.Type.Vec2:
                param.Vec2Param = EditorGUILayout.Vector2Field("Vec2 Param", param.Vec2Param);
                break;
            case ParamTypes.Type.Vec3:
                param.Vec3Param = EditorGUILayout.Vector3Field("Vec3 Param", param.Vec3Param);
                break;
            case ParamTypes.Type.GameObj:
                param.GameObjectParam = EditorGUILayout.ObjectField("GameObj Param", param.GameObjectParam, typeof(GameObject), true) as GameObject;
                break;
            case ParamTypes.Type.String:
                param.StringParam = EditorGUILayout.TextField("String Param", param.StringParam);
                break;
        }
        GUILayout.Space(20);

    }
}

[System.Serializable]
public class ParamTypes
{
    public enum Type
    {
        Bool,
        Int,
        Float,
        Vec2,
        Vec3,
        GameObj,
        String

    }

    [HideInInspector]
    public Type type;
    [HideInInspector]
    public bool BoolParam;
    [HideInInspector]
    public int IntParam;
    [HideInInspector]
    public float FloatParam;
    [HideInInspector]
    public Vector2 Vec2Param;
    [HideInInspector]
    public Vector3 Vec3Param;
    [HideInInspector]
    public GameObject GameObjectParam;
    [HideInInspector]
    public string StringParam;


}

