namespace PrototypeRavenDB
{
	using System;
	using System.Linq;
	using Raven.Client.Document;

	internal class Program
	{
		private static void Main(string[] args)
		{
			using (var documentStore = new DocumentStore {Url = "http://localhost:8081/",})
			{
				documentStore.Initialize();

				var mcx = new MorceauRobot[10];
				for (int i = 0; i < 10; ++i)
				{
					var morceauRobot = new MorceauRobot
					                   	{
					                   		Description = "Description " + i,
					                   		Largeur = i*Math.PI,
					                   		Nom = "Nom " + i,
					                   	};

					var sousMorceaux = new SousMorceauRobot[i];

					for (int j = 0; j < i; ++j)
					{
						sousMorceaux[j] = new SousMorceauRobot {Nom = "Sous morceau " + i + "-" + j};
					}

					morceauRobot.SousMorceaux = sousMorceaux;
					mcx[i] = morceauRobot;
				}

				using (var session = documentStore.OpenSession())
				{
					// Operations against session
					foreach (var morceauRobot in mcx)
					{
						session.Store(morceauRobot);
						session.Store(new Patate
						              	{
						              		MorceauRobotId = morceauRobot.Id
						              	});
					}

					// Flush those changes
					session.SaveChanges();
				}

				using (var session = documentStore.OpenSession())
				{
					var result =
						(
							from morceau in session.Query<MorceauRobot>()
							where morceau.Id < 7 && morceau.Id > 4
							select morceau
						).ToArray();

					Console.WriteLine(result);
				}
			}
		}
	}
}