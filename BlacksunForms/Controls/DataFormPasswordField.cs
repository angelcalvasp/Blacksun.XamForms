﻿using Blacksun.XamForms.Enums;
using Blacksun.XamForms.Resources;
using Xamarin.Forms;

namespace Blacksun.XamForms.Controls
{
    public class DataFormPasswordField : ContentView
    {

        public static BindableProperty DataMemberBindingProperty =
        BindableProperty.Create<DataFormPasswordField, string>(ctrl => ctrl.DataMemberBinding,
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanging: (bindable, oldValue, newValue) =>
        {
            var ctrl = (DataFormPasswordField)bindable;
            ctrl.DataMemberBinding = newValue;
        });


        public string DataMemberBinding
        {
            get { return (string)GetValue(DataMemberBindingProperty); }
            set
            {
                SetValue(DataMemberBindingProperty, value);
                TextField.Text = value;
            }
        }

        private StackLayout Container = new StackLayout() { Spacing = AppLayouts.LabelPropertySpacing, Padding = 0 };

        public Label LabelField = new Label()
        {
            Font = AppFonts.FormLabelFont,
            TextColor = AppColors.FormLabelColor,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        public Entry TextField = new Entry()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            IsPassword = true
        };

        public Keyboard Keyboard
        {
            get { return TextField.Keyboard; }
            set { TextField.Keyboard = value; }
        }

        private LabelType _labelType = LabelType.Label;
        public LabelType LabelType
        {
            get { return _labelType; }
            set
            {
                _labelType = value;

                TextField.Placeholder = "";

                if (Container.Children.Contains(LabelField))
                    Container.Children.Remove(LabelField);

                switch (LabelType)
                {
                    case LabelType.None:

                        break;
                    case LabelType.Label:

                        Container.Children.Insert(0, LabelField);
                        break;
                    case LabelType.Watermark:
                        TextField.Placeholder = Label;
                        break;
                }
            }
        }

        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;

                TextField.Placeholder = "";
                LabelField.Text = "";

                if (Label != null)
                {
                    switch (LabelType)
                    {
                        case LabelType.None:

                            break;
                        case LabelType.Label:
                            LabelField.Text = value;
                            if (!Container.Children.Contains(LabelField))
                            {
                                Container.Children.Insert(0, LabelField);
                            }
                            break;
                        case LabelType.Watermark:
                            TextField.Placeholder = value;
                            break;
                    }
                }

            }
        }
        /*
        public string DataMemberBinding
        {
            get { return TextField.Text; }
            set
            {
                TextField.Text = value;
                OnPropertyChanged();
            }
        }
        */
        private string _dataMemberBindingPath;
        public string DataMemberBindingPath
        {
            get { return _dataMemberBindingPath; }
            set
            {
                _dataMemberBindingPath = value;
                TextField.SetBinding(Entry.TextProperty, new Binding(value, BindingMode.TwoWay));
            }
        }

        public DataFormPasswordField()
        {
            Container.Children.Add(LabelField);
            Container.Children.Add(TextField);
            Content = Container;
        }

        
    }
}
