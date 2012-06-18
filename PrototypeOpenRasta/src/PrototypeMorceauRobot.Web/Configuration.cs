namespace PrototypeMorceauRobot.Web
{
	using System.Collections.Generic;
	using OpenRasta.Configuration;
	using PrototypeMorceauRobot.Web.Handlers;
	using PrototypeMorceauRobot.Web.Resources;

	public class Configuration : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
			{
				ResourceSpace.Has.ResourcesOfType<IEnumerable<MorceauRobot>>()
					.AtUri("/api/MorceauRobot")
					.HandledBy<MorceauRobotHandler>()
					.AsJsonDataContract();

				ResourceSpace.Has.ResourcesOfType<MorceauRobot>()
					.AtUri("/api/MorceauRobot/{id}")
					.HandledBy<MorceauRobotHandler>()
					.AsJsonDataContract();
			}
        }
    }
}