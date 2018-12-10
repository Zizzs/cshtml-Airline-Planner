using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AirlinePlanner;

namespace AirlinePlanner.Models
{
    public class JoinTableClass
    {   
        public static void SaveToJoinTable(int cityOne, int cityTwo, int flightId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO cities_flights(city_one_id, city_two_id, flight_id) VALUES (@cityOne, @cityTwo, @flightId);";
            cmd.Parameters.AddWithValue("@cityOne", cityOne);
            cmd.Parameters.AddWithValue("@cityTwo", cityTwo);
            cmd.Parameters.AddWithValue("@flightId", flightId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
    public class FlightClass
    {
        private int _id;
        private string _departureTime;
        private string _status;

        public FlightClass (string departureTime, string status)
        {
            _departureTime = departureTime;
            _status = status;
        }

        public FlightClass (int id, string departureTime, string status)
        {
            _id = id;
            _departureTime = departureTime;
            _status = status;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetDepartureTime()
        {
            return _departureTime;
        }

        public string GetStatus()
        {
            return _status;
        }

        public void FlightSave()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO flights(departureTime, status) VALUES (@departureTime, @status);";
            cmd.Parameters.AddWithValue("@departureTime", this._departureTime);
            cmd.Parameters.AddWithValue("@status", this._status);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<FlightClass> GetAll()
        {
            List<FlightClass> allFlights = new List<FlightClass>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM flights;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                DateTime departureTime = (DateTime) rdr.GetDateTime(1);
                string status = rdr.GetString(2);
                FlightClass newFlight = new FlightClass(id, departureTime.ToString("MM/dd/yyyy h:mm tt"), status);
                allFlights.Add(newFlight);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allFlights;
        }
    }
    public class CityClass
    {
        private int _id;
        private string _city;
        private string _state;

        public CityClass (string city, string state)
        {
            _city = city;
            _state = state;
        }

        public CityClass (int id, string city, string state)
        {
            _id = id;
            _city = city;
            _state = state;
        }

        public string GetCity()
        {
            return _city;
        }

        public string GetState()
        {
            return _state;
        }

        public int GetId()
        {
            return _id;
        }

        public void CitySave()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO cities(city, state) VALUES (@cityName, @stateName);";
            cmd.Parameters.AddWithValue("@cityName", this._city);
            cmd.Parameters.AddWithValue("@stateName", this._state);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<CityClass> GetAll()
        {
            List<CityClass> allCities = new List<CityClass>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cities;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string cityName = rdr.GetString(1);
                string state = rdr.GetString(2);
                CityClass newCity = new CityClass(id, cityName, state);
                allCities.Add(newCity);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCities;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM cities;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherCity)
        {
            if (!(otherCity is CityClass))
            {
                return false;
            }
            else
            {
                CityClass newCity = (CityClass) otherCity;
                bool nameEquality = (this.GetCity() == newCity.GetCity());
                return (nameEquality);
            }
        }

    }
}
