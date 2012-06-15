namespace WebApi.Tests
{
	using System.Globalization;
	using System.Net;
	using System.Net.Http;
	using System.Web.Http;
	using MvcApplication1;
	using MvcApplication1.Controllers;
	using NUnit.Framework;

	[TestFixture]
	public class PostFixture
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

			result = client.PostAsJsonAsync("http://localhost/api/morceaurobotapi",
			                                new MorceauRobot
			                                	{
													Description = "patate",
													Longueur = 1394,
													Nom = "flugelhorn"
			                                	}).Result;
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
		public void Content_is_null()
		{
			Assert.IsNull(result.Content);
		}

		[Test]
		public void MorceauRobot_was_created()
		{
			Assert.AreEqual(11, MorceauRobotRepo.MorceauxRobot.Count);
		}
	}
}