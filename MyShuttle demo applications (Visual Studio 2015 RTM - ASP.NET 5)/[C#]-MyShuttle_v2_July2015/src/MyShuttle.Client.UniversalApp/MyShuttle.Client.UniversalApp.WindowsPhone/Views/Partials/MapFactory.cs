namespace MyShuttle.Client.UniversalApp.Views.Partials
{
    // This is a workaround made for avoid an AccessViolationException with WP Maps Control.
    public static class MapFactory
    {
        private static VehiclesInMap _map;

        public static VehiclesInMap GetMap(object dataContext)
        {
            if (_map == null)
            {
                _map = new VehiclesInMap { DataContext = dataContext };
            }

            return _map;
        }
    }
}
