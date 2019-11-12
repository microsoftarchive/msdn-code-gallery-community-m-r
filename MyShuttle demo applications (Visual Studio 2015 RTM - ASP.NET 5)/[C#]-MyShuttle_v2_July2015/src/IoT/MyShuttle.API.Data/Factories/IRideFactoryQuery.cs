using MyShuttle.API.Data.Queries;

namespace MyShuttle.API.Data.Factories
{
    public interface IRideFactoryQuery
    {
        RidesBetweenDatesQuery GetRidesBetweenDates();
        RidesPerYearAndVehicleQuery GetRidesPerYearAndVehicle();
        RidesPerMonthYearVehicleQuery GetRidesPerMonthYearVehicleQuery();

    }
}