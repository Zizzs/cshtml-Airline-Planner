using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirlinePlanner.Models;
using System;
using System.Collections.Generic;
 
namespace AirlinePlanner.Tests
{
    [TestClass]
    public class AirlinePlannerClassTest : IDisposable
    {
        public void Dispose()
        {
            CityClass.ClearAll();
        }

        public AirlinePlannerClassTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airline_planner_test;";
        }

        [TestMethod]
        public void GetAll_ReturnsEmptyListFromDatabase_CityClassList()
        {
            //Arrange
            List<CityClass> newList = new List<CityClass> { };

            //Act
            List<CityClass> result = CityClass.GetAll();

            //Assert
            CollectionAssert.AreEqual(newList, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_CityList()
        {
            //Arrange
            string name = "Washington";
            string state = "WA";
            CityClass city = new CityClass(name, state);

            //Act
            city.CitySave();
            List<CityClass> result = CityClass.GetAll();
            List<CityClass> testList = new List<CityClass>{city};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

    }
}
