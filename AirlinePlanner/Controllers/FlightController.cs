using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AirlinePlanner.Models;

namespace AirlinePlanner.Controllers
{
    public class FlightController : Controller
    {
        [HttpGet("/flight/newcity")]
        public ActionResult NewCity()
        {
            List<CityClass> cityList = CityClass.GetAll();
            return View(cityList);
        }

        [HttpPost("/flight/newcity")]
        public ActionResult SaveCity(string cityName, string state)
        {
            CityClass city = new CityClass(cityName, state);
            city.CitySave();
            return RedirectToAction("NewCity");
        }

        [HttpGet("/flight/newflight")]
        public ActionResult NewFlight()
        {
            List<CityClass> allCities = CityClass.GetAll();
            List<FlightClass> allFlights = FlightClass.GetAll();
            Dictionary<string, object> model = new Dictionary<string, object> { };
            model.Add("cities", allCities);
            model.Add("flights", allFlights);
            return View(model);
        }

        [HttpPost("/flight/newflight")]
        public ActionResult SaveFlight(string departureTime, string status, int cityOne, int cityTwo)
        {
            FlightClass flight = new FlightClass(departureTime, status);
            flight.FlightSave();
            List<FlightClass> flights = FlightClass.GetAll();
            int flightId = flight.GetId();
            // int flightId = flights[0].GetId();
            // this /\ was grabbing the first flight in the list every time and setting all entries to 1
            JoinTableClass.SaveToJoinTable(cityOne, cityTwo, flightId);
            return RedirectToAction("NewFlight");
        }

        [HttpPost("/flight/search")]
        public ActionResult ShowFlights(int cityOneId)
        {
            List<JoinTableClass> totalFlightInfo = JoinTableClass.GetJoinTable(cityOneId);
            return View("ShowFlights", totalFlightInfo);
        }

    }
}
