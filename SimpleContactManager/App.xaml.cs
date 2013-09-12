using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
            Trace.TraceInformation( "--------------------------------------------" );
        }

        private static void OnDispatcherUnhandledException( object sender, DispatcherUnhandledExceptionEventArgs e )
        {
            Trace.TraceError( "Unhandled exception: " + e.Exception.ToString() + Environment.NewLine + e.Exception.StackTrace );
        }
    }
}