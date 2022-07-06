namespace AppUi.Window.DI
{
    public interface IDiContainer
    {
        TDependence Navigate<TDependence>(string navigationKey) where TDependence : class;
        TDependence Navigate<TDependence>(string navigationKey, object arg) where TDependence : class;
    }
}