namespace PrototypeRavenDB
{
	public class MorceauRobot
	{
		public long Id { get; set; }
		public string Nom { get; set; }
		public string Description { get; set; }
		public double Largeur { get; set; }
		public SousMorceauRobot[] SousMorceaux { get; set; }
	}

	public class Patate
	{
		public long Id { get; set; }
		public long MorceauRobotId { get; set; }
	}
}