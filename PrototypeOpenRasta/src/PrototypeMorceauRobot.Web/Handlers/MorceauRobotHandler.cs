namespace PrototypeMorceauRobot.Web.Handlers
{
	using System.Collections.Generic;
	using System.Linq;
	using OpenRasta.Web;
	using PrototypeMorceauRobot.Web.Resources;

	public class MorceauRobotHandler
	{
		public IEnumerable<MorceauRobot> Get()
		{
			return MorceauRobotRepo.MorceauxRobot.Values.ToArray(); //sans ToArray(), le json est mal formatté... il y a une clé dictionary dans l'objet json
		}

		public OperationResult Get(int id)
		{
			if (!MorceauRobotRepo.MorceauxRobot.ContainsKey(id)) // sinon on a une erreur 500
			{
				return new OperationResult.NotFound();
			}

			return new OperationResult.OK(MorceauRobotRepo.MorceauxRobot[id]);
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