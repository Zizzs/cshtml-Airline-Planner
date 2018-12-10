using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AirlinePlanner;

namespace AirlinePlanner.Models
{
    public class JoinTableClass
    {   
        private int _id;
        private string _time;
        private string _status;
        private string _cityOne;
        private string _cityTwo;

        public JoinTableClass(int id, string time, string status, string cityOne, string cityTwo)
        {
            _id = id;
            _time = time;
            _status = status;
            _cityOne = cityOne;
            _cityTwo = cityTwo;
        }

        public string GetTime()
        {
            return _time;
        }

        public string GetStatus()
        {
            return _status;
        }

        public string GetCityOne()
        {
            return _cityOne;
        }

        public string GetCityTwo()
        {
            return _cityTwo;
        }

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

        public static List<JoinTableClass> GetJoinTable(int cityOneId)
        {
            {
                List<JoinTableClass> fullInfo = new List<JoinTableClass>{};
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT flights.*, cities_flights.city_one_id, cities_flights.city_two_id FROM
                    cities JOIN cities_flights ON (cities.id = cities_flights.city_one_id)
                        JOIN flights ON (cities_flights.flight_id = flights.id)
                    WHERE cities.id = " + cityOneId + ";";

                MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                while(rdr.Read())
                {
                    int id = rdr.GetInt32(0);
                    DateTime departureTime = (DateTime) rdr.GetDateTime(1);
                    string status = rdr.GetString(2);
                    string cityOne = CityClass.FindById(rdr.GetInt32(3)).GetCity();
                    string cityTwo = CityClass.FindById(rdr.GetInt32(4)).GetCity();
                    JoinTableClass fullFlightInfo = new JoinTableClass(id, departureTime.ToString("MM/dd/yyyy h:mm tt"), status, cityOne, cityTwo);
                    fullInfo.Add(fullFlightInfo);
                }
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
                return fullInfo;
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
            cmd.CommandText = @"INSERT INTO flights(time, status) VALUES (@departureTime, @status);";
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

        public static CityClass FindById(int id)
        {

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cities WHERE id = " + id + ";";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            int idz = 0;
            string city = "";
            string state = "";
            while(rdr.Read())
            {
                idz = rdr.GetInt32(0);
                city = rdr.GetString(1);
                state = rdr.GetString(2);
                
            }
            CityClass newCity = new CityClass(idz, city, state);
            conn.Close();
            if (conn !=null)
            {
                conn.Dispose();
            }
            return newCity;
        }

    }
}
