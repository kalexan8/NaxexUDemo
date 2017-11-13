using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaxexUDemo.Controllers;
using NaxexUDemo.Data.Repositories;
using NaxexUDemo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class TestCourses
    {
        [TestMethod]
        public void TestGetAllCourses()
        {
            var mockCourses = new List<Course>();
            mockCourses.Add(new Course { CourseId=1, Title="Test Course 1", Description ="desc", CourseCapacity=20, Credits=4});
            mockCourses.Add(new Course { CourseId = 2, Title = "Test Course 2", Description = "desc", CourseCapacity = 20, Credits = 4 });
            mockCourses.Add(new Course { CourseId = 3, Title = "Test Course 3", Description = "desc", CourseCapacity = 20, Credits = 4 });

            var courseRepositoryMock = TestInitializer.MockCourseRepository;
            courseRepositoryMock.Setup
                  (x => x.GetCourses()).Returns(Task.FromResult(mockCourses));

            var response = TestInitializer.TestHttpClient.GetAsync("Courses").Result;

            var resp = response.Content.ReadAsStringAsync().Result;
            var responseData = JsonConvert.DeserializeObject<List<Course>>(resp);
            Assert.AreEqual(3, responseData.Count);
            Assert.AreEqual(mockCourses[0].CourseId, responseData[0].CourseId);
        }
    }
}
