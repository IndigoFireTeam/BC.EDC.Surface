using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace BcSoft.EDC.Surface.Commands
{
    public class ExInvokeCommandAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ExInvokeCommandAction), null);
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ExInvokeCommandAction), null);

        private string m_CommandName;
        public string CommandName
        {
            get
            {
                base.ReadPreamble();
                return this.m_CommandName;
            }
            set
            {
                if (this.CommandName != value)
                {
                    base.WritePreamble();
                    this.m_CommandName = value;
                    base.WritePostscript();
                }
            }
        }

        public ICommand Command
        {
            get
            {
                return (ICommand)base.GetValue(ExInvokeCommandAction.CommandProperty);
            }
            set
            {
                base.SetValue(ExInvokeCommandAction.CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return base.GetValue(ExInvokeCommandAction.CommandParameterProperty);
            }
            set
            {
                base.SetValue(ExInvokeCommandAction.CommandParameterProperty, value);
            }
        }
         
        protected override void Invoke(object parameter)
        {
            if (base.AssociatedObject != null)
            {
                ICommand command = this.ResolveCommand();
                ExCommandParameter exParameter = new ExCommandParameter
                {
                    Sender = base.AssociatedObject,
                    Parameter = GetValue(CommandParameterProperty),
                    EventArgs = parameter as EventArgs

                };

                if (command != null && command.CanExecute(exParameter))
                {
                    command.Execute(exParameter);
                }
            }
        }
        private ICommand ResolveCommand()
        {
            ICommand result = null;
            if (this.Command != null)
            {
                result = this.Command;
            }
            else
            {
                if (base.AssociatedObject != null)
                {
                    Type type = base.AssociatedObject.GetType();
                    PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    PropertyInfo[] array = properties;
                    for (int i = 0; i < array.Length; i++)
                    {
                        PropertyInfo propertyInfo = array[i];
                        if (typeof(ICommand).IsAssignableFrom(propertyInfo.PropertyType) && string.Equals(propertyInfo.Name, this.CommandName, StringComparison.Ordinal))
                        {
                            result = (ICommand)propertyInfo.GetValue(base.AssociatedObject, null);
                        }
                    }
                }
            }
            return result;
        }
    }
}
