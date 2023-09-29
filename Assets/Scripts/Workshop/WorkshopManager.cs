using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agyr.Workshop
{
    public class WorkshopManager : Singleton<WorkshopManager>
    {
        [SerializeField]
        private WorkshopStorage workshopStorage;
        private WorkshopBench workshopBench;

        public WorkshopStorage WorkshopStorage
        {
            get { return workshopStorage; }
            set { workshopStorage = value; }
        }

        public WorkshopBench WorkshopBench
        {
            get { return workshopBench; }
            set { workshopBench = value; }
        }

    }








    #region Property Binding
    public interface IProperty<T> : IProperty
    {
        new event Action<T> ValueChanged;
        new T Value { get; }
    }

    public interface IProperty
    {
        event Action<object> ValueChanged;
        object Value { get; }
    }

    [Serializable]
    public class Property<T> : IProperty<T>
    {
        public event Action<T> ValueChanged;

        event Action<object> IProperty.ValueChanged
        {
            add => valueChanged += value;
            remove => valueChanged -= value;
        }

        [SerializeField]
        private T value;

        public T Value
        {
            get => value;

            set
            {
                if (EqualityComparer<T>.Default.Equals(this.value, value))
                {
                    return;
                }

                this.value = value;

                ValueChanged?.Invoke(value);
                valueChanged?.Invoke(value);
            }
        }

        object IProperty.Value => value;

        private Action<object> valueChanged;

        public Property(T value) => this.value = value;

        public static explicit operator Property<T>(T value) => new Property<T>(value);
        public static implicit operator T(Property<T> binding) => binding.value;
    }

    public static class RuntimeBindingExtensions
    {
        private static readonly Dictionary<Text, List<(IProperty property, Action<object> binding)>> propertyBindings = new Dictionary<Text, List<(IProperty property, Action<object> binding)>>();

        public static void BindProperty(this Text element, IProperty property)
        {
            if (!propertyBindings.TryGetValue(element, out var bindingsList))
            {
                bindingsList = new List<(IProperty, Action<object>)>();
                propertyBindings.Add(element, bindingsList);
            }

            Action<object> onPropertyValueChanged = OnPropertyValueChanged;
            bindingsList.Add((property, onPropertyValueChanged));

            property.ValueChanged += onPropertyValueChanged;

            OnPropertyValueChanged(property.Value);

            void OnPropertyValueChanged(object newValue)
            {
                element.text = newValue?.ToString() ?? "";
            }
        }

        public static void UnbindProperty(this Text element, IProperty property)
        {
            if (!propertyBindings.TryGetValue(element, out var bindingsList))
            {
                return;
            }

            for (int i = bindingsList.Count - 1; i >= 0; i--)
            {
                var propertyBinding = bindingsList[i];
                if (propertyBinding.property == property)
                {
                    propertyBinding.property.ValueChanged -= propertyBinding.binding;
                    bindingsList.RemoveAt(i);
                }
            }
        }

        public static void UnbindAllProperties(this Text element)
        {
            if (!propertyBindings.TryGetValue(element, out var bindingsList))
            {
                return;
            }

            foreach (var propertyBinding in bindingsList)
            {
                propertyBinding.property.ValueChanged -= propertyBinding.binding;
            }

            bindingsList.Clear();
        }
    }
    #endregion



}