namespace AmpShell.WPF.Views
{
    using System;
    using System.Windows.Input;

#pragma warning disable CS0067

    public sealed class SimpleViewCommand : ICommand
    {
        private SimpleEventHandler handler;

        private bool isEnabled = true;

        public SimpleViewCommand(SimpleEventHandler handler)
        {
            this.handler = handler;
        }

        public delegate void SimpleEventHandler();

        public event EventHandler? CanExecuteChanged;

        public bool IsEnabled
        {
            get { return this.isEnabled; }
        }

        bool ICommand.CanExecute(object arg)
        {
            return this.IsEnabled;
        }

        void ICommand.Execute(object arg)
        {
            this.handler();
        }

        private void OnCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}