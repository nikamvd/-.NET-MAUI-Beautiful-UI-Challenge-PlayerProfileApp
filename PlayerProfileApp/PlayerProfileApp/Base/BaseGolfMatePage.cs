using System;
namespace PlayerProfileApp.Base
{
    public interface ILazyNavigator
    {
        INavigation Navigation { get; set; }
    }

    public interface IMonitorAppearance
    {
        void OnAppearing();
        void OnDisappearing();
    }

    public class BaseGolfMatePage : ContentPage
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is ILazyNavigator lazyNavigator)
            {
                lazyNavigator.Navigation = Navigation;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IMonitorAppearance monitorAppearance)
            {
                monitorAppearance.OnAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            if (BindingContext is IMonitorAppearance monitorAppearance)
            {
                monitorAppearance.OnDisappearing();
            }

            base.OnDisappearing();
        }
    }
}

