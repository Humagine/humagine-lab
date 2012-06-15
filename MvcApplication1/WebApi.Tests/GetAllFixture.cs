namespace WebApi.Tests
{
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Web.Http;
	using MvcApplication1;
	using MvcApplication1.Controllers;
	using NUnit.Framework;

	[TestFixture]
	public class GetAllFixture
	{
		private HttpServer server;
		private HttpClient client;
		private HttpResponseMessage result;

		[SetUp]
		public void SetUp()
		{
			var httpConfiguration = new HttpConfiguration();
			httpConfiguration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}",
												  new { id = RouteConfig.GetOptional() });
			server = new HttpServer(httpConfiguration);
			client = new HttpClient(server);


			for (int i = 0; i < 10; ++i)
			{
				var id = MorceauRobotRepo.GetNextId();
				MorceauRobotRepo.MorceauxRobot[id] = new MorceauRobot
				                                     	{
				                                     		Id = id,
				                                     		Description = i.ToString(CultureInfo.InvariantCulture),
				                                     		Longueur = ((double) i)/2
				                                     	};
			}

			result = client.GetAsync("http://localhost/api/morceaurobotapi").Result;
		}

		[TearDown]
		public void TearDown()
		{
			MorceauRobotRepo.MorceauxRobot.Clear();
			MorceauRobotRepo.nextId = 0;
			result.Dispose();
			client.Dispose();
			server.Dispose();
		}

		[Test]
		public void Should_not_be_null()
		{
			Assert.IsNotNull(result);
		}

		[Test]
		public void StatusCode_should_be_OK()
		{
			Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
		}

		[Test]
		public void Content_is_of_type_ObjectContent()
		{
			Assert.IsInstanceOf<ObjectContent>(result.Content);
			Assert.IsInstanceOf<ObjectContent<IEnumerable<MorceauRobot>>>(result.Content);
		}

		[Test]
		public void Value_is_of_type_IEnumerable_of_MorceauRobot()
		{
			var content = (ObjectContent)result.Content;
			var value = (IEnumerable<MorceauRobot>) content.Value;
			Assert.IsNotEmpty(value);
			Assert.AreEqual(10, value.Count());
		}
	}
}
