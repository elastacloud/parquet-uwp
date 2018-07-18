using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LogMagic;

namespace Parquet.Windows.Universal
{
   /// <summary>
   /// Provides application-specific behavior to supplement the default Application class.
   /// </summary>
   sealed partial class App : Application
   {
      /// <summary>
      /// Initializes the singleton application object.  This is the first line of authored code
      /// executed, and as such is the logical equivalent of main() or WinMain().
      /// </summary>
      public App()
      {
         this.InitializeComponent();
         this.Suspending += OnSuspending;

         //L.Config
         //   .WriteTo.AzureApplicationInsights("1fa7d71c-9660-46c5-9779-0b5aa068d09c");
      }

      public SolidColorBrush GetSolidColorBrush(string hex)
      {
         hex = hex.Replace("#", string.Empty);
         byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
         byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
         byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
         byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
         SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
         return myBrush;
      }

      /// <summary>
      /// Invoked when the application is launched normally by the end user.  Other entry points
      /// will be used such as when the application is launched to open a specific file.
      /// </summary>
      /// <param name="e">Details about the launch request and process.</param>
      protected override void OnLaunched(LaunchActivatedEventArgs e)
      {
         Frame rootFrame = Window.Current.Content as Frame;

         if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
         {
            ApplicationView.GetForCurrentView().Title = "Parquet Data Science Studio";
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            if (titleBar != null)
            {
               titleBar.ButtonBackgroundColor = GetSolidColorBrush("#FF0078D7").Color;
               titleBar.ButtonForegroundColor = Colors.White;
               titleBar.BackgroundColor = GetSolidColorBrush("#FF000000").Color;
               titleBar.ForegroundColor = Colors.White;
            }
         }


         // Do not repeat app initialization when the Window already has content,
         // just ensure that the window is active
         if (rootFrame == null)
         {
            // Create a Frame to act as the navigation context and navigate to the first page
            rootFrame = new Frame();

            rootFrame.NavigationFailed += OnNavigationFailed;

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
               //TODO: Load state from previously suspended application
            }

            // Place the frame in the current Window
            Window.Current.Content = rootFrame;
         }

         if (e.PrelaunchActivated == false)
         {
            if (rootFrame.Content == null)
            {
               // When the navigation stack isn't restored navigate to the first page,
               // configuring the new page by passing required information as a navigation
               // parameter
               rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
         }
      }

      /// <summary>
      /// Invoked when Navigation to a certain page fails
      /// </summary>
      /// <param name="sender">The Frame which failed navigation</param>
      /// <param name="e">Details about the navigation failure</param>
      void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
      {
         throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
      }

      /// <summary>
      /// Invoked when application execution is being suspended.  Application state is saved
      /// without knowing whether the application will be terminated or resumed with the contents
      /// of memory still intact.
      /// </summary>
      /// <param name="sender">The source of the suspend request.</param>
      /// <param name="e">Details about the suspend request.</param>
      private void OnSuspending(object sender, SuspendingEventArgs e)
      {
         SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
         //TODO: Save application state and stop any background activity
         deferral.Complete();
      }
   }
}