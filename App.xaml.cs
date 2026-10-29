using Microsoft.Extensions.DependencyInjection;

namespace ddelgadoT1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new View.vHome());
        }
    }
}