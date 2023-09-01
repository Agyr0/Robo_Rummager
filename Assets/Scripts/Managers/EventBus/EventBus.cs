using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    private static readonly IDictionary<EventType, UnityEventBase> Events = new Dictionary<EventType, UnityEventBase>();

    #region Subscribe
    /// <summary>
    /// Subscribe to events.
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void Subscribe(EventType eventType, UnityAction listener)
    {
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent)thisEvent).AddListener(listener);
        }
        else
        {
            var newEvent = new UnityEvent();
            newEvent.AddListener(listener);
            Events.Add(eventType, newEvent);
        }
    }
    /// <summary>
    /// Subscribe to events with a generic. <br>Replace '<typeparamref name="T0"/>' with the type of parameter</br>
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void Subscribe<T0>(EventType eventType, UnityAction<T0> listener)
    {
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent<T0>)thisEvent).AddListener(listener);
        }
        else
        {
            var newEvent = new UnityEvent<T0>();
            newEvent.AddListener(listener);
            Events.Add(eventType, newEvent);
        }
    }

    /// <summary>
    /// Subscribe to events with two generics. <br>Replace '<typeparamref name="T0"/>' and '<typeparamref name="T1"/>' with the type of parameter</br>
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void Subscribe<T0, T1>(EventType eventType, UnityAction<T0, T1> listener)
    {
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent<T0, T1>)thisEvent).AddListener(listener);
        }
        else
        {
            var newEvent = new UnityEvent<T0, T1>();
            newEvent.AddListener(listener);
            Events.Add(eventType, newEvent);
        }
    }
    /// <summary>
    /// Subscribe to events with two generics. <br>Replace '<typeparamref name="T0"/>', '<typeparamref name="T1"/>' and '<typeparamref name="T2"/>' with the type of parameter</br>
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void Subscribe<T0, T1, T2>(EventType eventType, UnityAction<T0, T1, T2> listener)
    {
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent<T0, T1, T2>)thisEvent).AddListener(listener);
        }
        else
        {
            var newEvent = new UnityEvent<T0, T1, T2>();
            newEvent.AddListener(listener);
            Events.Add(eventType, newEvent);
        }
    }
    #endregion


    #region Unsubscribe
    /// <summary>
    /// Unsubscribe from events. 
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void Unsubscribe(EventType eventType, UnityAction listener)
    {

        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent)thisEvent).RemoveListener(listener);
        }
    }
    /// <summary>
    /// Unsubscribe from events that have a generic.  <br>Replace '<typeparamref name="T0"/>' with the type of parameter</br>
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void Unsubscribe<T0>(EventType eventType, UnityAction<T0> listener)
    {

        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent<T0>)thisEvent).RemoveListener(listener);
        }
    }
    /// <summary>
    /// Unsubscribe from events that have two generics.  <br>Replace '<typeparamref name="T0"/>' and '<typeparamref name="T1"/>' with the type of parameter</br>
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void Unsubscribe<T0, T1>(EventType eventType, UnityAction<T0, T1> listener)
    {

        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent<T0, T1>)thisEvent).RemoveListener(listener);
        }
    }
    /// <summary>
    /// Unsubscribe from events that have two generics.  <br>Replace '<typeparamref name="T0"/>', '<typeparamref name="T1"/>' and '<typeparamref name="T2"/>' with the type of parameter</br>
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void Unsubscribe<T0, T1, T2>(EventType eventType, UnityAction<T0, T1, T2> listener)
    {

        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent<T0, T1, T2>)thisEvent).RemoveListener(listener);
        }
    }
    #endregion


    #region Publish
    /// <summary>
    /// Don't publish events yourself. Use the EventTester found in "Tools/Programming/Event Tester".
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="parameter"></param>
    public static void Publish(EventType eventType)
    {
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent)thisEvent).Invoke();
        }
    }
    /// <summary>
    /// Don't publish events yourself. Use the EventTester found in "Tools/Programming/Event Tester".
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="parameter"></param>
    public static void Publish<T0>(EventType eventType, T0 parameter)
    {
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent<T0>)thisEvent).Invoke(parameter);
        }
    }
    /// <summary>
    /// Don't publish events yourself. Use the EventTester found in "Tools/Programming/Event Tester".
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="parameter"></param>
    public static void Publish<T0, T1>(EventType eventType, T0 parameter0, T1 parameter1)
    {
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent<T0, T1>)thisEvent).Invoke(parameter0, parameter1);
        }
    }
    /// <summary>
    /// Don't publish events yourself. Use the EventTester found in "Tools/Programming/Event Tester".
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="parameter"></param>
    public static void Publish<T0, T1, T2>(EventType eventType, T0 parameter0, T1 parameter1, T2 parameter2)
    {
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            ((UnityEvent<T0, T1, T2>)thisEvent).Invoke(parameter0, parameter1, parameter2);
        }
    }
    #endregion

}
