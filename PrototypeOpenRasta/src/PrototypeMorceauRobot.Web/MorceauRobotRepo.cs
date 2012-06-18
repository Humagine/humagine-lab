namespace PrototypeMorceauRobot.Web
{
	using System.Collections.Generic;
	using PrototypeMorceauRobot.Web.Resources;

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

			var robot1 = new MorceauRobot
			             	{
			             		Id = GetNextId(),
			             		Nom = "nom1",
			             		Description = "desc1",
			             		Longueur = 1.0,
			             	};

			var robot2 = new MorceauRobot
			             	{
			             		Id = GetNextId(),
			             		Nom = "nom2",
			             		Description = "desc2",
			             		Longueur = 2.0,
			             	};

			morceauRobot.Add(robot1.Id, robot1);
			morceauRobot.Add(robot2.Id, robot2);
		}
	}
}