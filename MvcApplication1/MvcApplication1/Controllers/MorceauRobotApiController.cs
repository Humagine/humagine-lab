namespace MvcApplication1.Controllers
{
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Web.Http;

	public class MorceauRobot
	{
		public int Id { get; set; }
		public string Nom { get; set; }
		public string Description { get; set; }
		public double Longueur { get; set; }
	}

	public class MorceauRobotRepo
	{
		private static readonly Dictionary<int, MorceauRobot> morceauRobot;
		public static IDictionary<int, MorceauRobot> MorceauxRobot { get { return morceauRobot; } }
		public static int nextId = 0;
		
		public static int GetNextId()
		{
			return ++nextId;
		}

		static MorceauRobotRepo()
		{
			morceauRobot = new Dictionary<int, MorceauRobot>();
		}
	}

	public class MorceauRobotApiController : ApiController
	{
		public IEnumerable<MorceauRobot> Get()
		{
			return MorceauRobotRepo.MorceauxRobot.Values;
		}

		public MorceauRobot Get(int id)
		{
			return MorceauRobotRepo.MorceauxRobot[id];
		}

		public void Post(MorceauRobot morceauRobot)
		{
			morceauRobot.Id = MorceauRobotRepo.GetNextId();
			MorceauRobotRepo.MorceauxRobot[morceauRobot.Id] = morceauRobot;
		}

		public void Delete(int id)
		{
			MorceauRobotRepo.MorceauxRobot.Remove(id);
		}
	}
}