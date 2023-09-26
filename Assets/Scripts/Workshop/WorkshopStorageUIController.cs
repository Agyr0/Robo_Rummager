using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Agyr.Workshop
{
    public class WorkshopStorageUIController : MonoBehaviour
    {
        private WorkshopManager workshopManager;
        private WorkshopStorage workshopStorage;


        #region Text Components
        [SerializeField]
        private Text _creditsText;
        [SerializeField]
        private Text _motherBoardText;
        [SerializeField]
        private Text _wiresText;
        [SerializeField]
        private Text _oilText;
        [SerializeField]
        private Text _metalScrapText;
        [SerializeField]
        private Text _sensorsText;
        [SerializeField]
        private Text _zCrystalText;
        [SerializeField]
        private Text _radioactiveWasteText;
        [SerializeField]
        private Text _blackMatterText;

        #endregion

        [Space(20)]

        #region Icons
        [SerializeField]
        private Image _creditsIcon;
        [SerializeField]
        private Image _motherBoardIcon;
        [SerializeField]
        private Image _wiresIcon;
        [SerializeField]
        private Image _oilIcon;
        [SerializeField]
        private Image _metalScrapIcon;
        [SerializeField]
        private Image _sensorsIcon;
        [SerializeField]
        private Image _zCrystalIcon;
        [SerializeField]
        private Image _radioactiveWasteIcon;
        [SerializeField]
        private Image _blackMatterIcon;
        #endregion



        private void Start()
        {
            workshopManager = WorkshopManager.Instance;
            workshopStorage = workshopManager.WorkshopStorage;

            #region Bind Properties
            RuntimeBindingExtensions.BindProperty(_creditsText, workshopStorage.CreditProp);
            RuntimeBindingExtensions.BindProperty(_motherBoardText, workshopStorage.MotherBoardProp);
            RuntimeBindingExtensions.BindProperty(_wiresText, workshopStorage.WireProp);
            RuntimeBindingExtensions.BindProperty(_oilText, workshopStorage.OilProp);
            RuntimeBindingExtensions.BindProperty(_metalScrapText, workshopStorage.MetalScrapProp);
            RuntimeBindingExtensions.BindProperty(_sensorsText, workshopStorage.SensorProp);
            RuntimeBindingExtensions.BindProperty(_zCrystalText, workshopStorage.ZCrystalProp);
            RuntimeBindingExtensions.BindProperty(_radioactiveWasteText, workshopStorage.RadioactiveWasteProp);
            RuntimeBindingExtensions.BindProperty(_blackMatterText, workshopStorage.BlackMatterProp);
            #endregion

            #region Assign Sprites
            _creditsIcon.sprite = workshopStorage.creditIcon;
            _motherBoardIcon.sprite = workshopStorage.motherBoardIcon;
            _wiresIcon.sprite = workshopStorage.wireIcon;
            _oilIcon.sprite = workshopStorage.oilIcon;
            _metalScrapIcon.sprite = workshopStorage.metalScrapIcon;
            _sensorsIcon.sprite = workshopStorage.sensorIcon;
            _zCrystalIcon.sprite = workshopStorage.zCrystalIcon;
            _radioactiveWasteIcon.sprite = workshopStorage.radioactiveWasteIcon;
            _blackMatterIcon.sprite = workshopStorage.blackMatterIcon;
            #endregion
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