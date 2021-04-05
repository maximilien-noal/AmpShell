namespace AmpShell.ViewModels
{
    using AmpShell.Core.DAL;
    using AmpShell.Core.DOSBox;
    using AmpShell.Core.Model;

    using ReactiveUI;

    using System;
    using System.Diagnostics;
    using System.Reactive;

    public class MainViewModel : ReactiveObject
    {
        private string _helpMessage = "";

        public string HelpMessage { get => _helpMessage; private set { this.RaiseAndSetIfChanged(ref _helpMessage, value, nameof(HelpMessage)); } }

        public ReactiveCommand<Unit, Process> RunDOSBox { get; }
    }
}