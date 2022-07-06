namespace AppUi.Window.DI
{
    public interface IDiContainer
    {
        TDependence Navigate<TDependence>(string navigationKey) where TDependence : class;
    }
}