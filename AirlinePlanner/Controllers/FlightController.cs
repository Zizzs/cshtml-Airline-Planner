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
            Dictionary<string, object> model = new Dictionary<string, object>{};
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
            int flightId = flights[0].GetId();
            JoinTableClass.SaveToJoinTable(cityOne, cityTwo, flightId);
            return RedirectToAction("NewFlight");
        }

        [HttpGet("/flight/flights")]
        public ActionResult ShowFlights()
        {
            List<FlightClass> allFlights = FlightClass.GetAll();
            return View();
        }


    }
}
