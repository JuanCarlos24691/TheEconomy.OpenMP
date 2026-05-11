using System.Collections.Generic;
using TheEconomy.Server.Resources.KnowledgeTest.Models;

namespace TheEconomy.Server.Resources.KnowledgeTest.Data;

public static class KnowledgeTestData
{
    public static List<RoleQuestion> GetQuestions(string webSite)
    {
        string footer = $" — Para responder debes haber leído los conceptos básicos en {webSite}";

        return
        [
            new() {
                Title = "In Character (IC)",
                Description = $"¿Qué significa In Character (IC)?{footer}",
                Answers =
                [
                    new QuestionAnswer { UUID = 1, Text = "Hablar sobre el clima en la vida real.", IsCorrect = false },
                    new QuestionAnswer { UUID = 2, Text = "Hablar sobre un evento en el juego.", IsCorrect = true },
                    new QuestionAnswer { UUID = 3, Text = "Hablar sobre un evento en la vida real.", IsCorrect = false },
                    new QuestionAnswer { UUID = 4, Text = "Hablar sobre las noticias en la vida real.", IsCorrect = false }
                ]
            },
            new()
            {
                Title = "Out of Character (OOC)",
                Description = $"¿Qué significa Out of Character (OOC)?{footer}",
                Answers =
                [
                    new QuestionAnswer { UUID = 1, Text = "Interactuar verbalmente con otro jugador dentro del rol.", IsCorrect = false },
                    new QuestionAnswer { UUID = 2, Text = "Usar información de un foro para beneficio en el juego.", IsCorrect = false },
                    new QuestionAnswer { UUID = 3, Text = "Pedir ayuda en el canal de ayuda.", IsCorrect = true },
                    new QuestionAnswer { UUID = 4, Text = "Hablar sobre las noticias en la vida real.", IsCorrect = true }
                ]
            },
            new()
            {
                Title = "MetaGaming (MG)",
                Description = $"¿Qué significa MetaGaming (MG)?{footer}",
                Answers =
                [
                    new QuestionAnswer { UUID = 1, Text = "Usar información de un stream de Twitch para beneficio en el juego.", IsCorrect = true },
                    new QuestionAnswer { UUID = 2, Text = "Pegarle a otro jugador con un bate.", IsCorrect = false },
                    new QuestionAnswer { UUID = 3, Text = "Hablar sobre el clima de la vida real en el juego.", IsCorrect = false },
                    new QuestionAnswer { UUID = 4, Text = "Usar información fuera del juego para tomar decisiones dentro del juego.", IsCorrect = true }
                ]
            },
            new()
            {
                Title = "PowerGaming (PG)",
                Description = $"¿Qué significa PowerGaming (PG)?{footer}",
                Answers =
                [
                    new QuestionAnswer { UUID = 1, Text = "Forzar a otro jugador a hacer algo sin darle la oportunidad de responder.", IsCorrect = false },
                    new QuestionAnswer { UUID = 2, Text = "Ignorar las heridas de tu personaje después de un accidente.", IsCorrect = false },
                    new QuestionAnswer { UUID = 3, Text = "Hacer cosas que tu personaje no podría hacer en la vida real.", IsCorrect = true },
                    new QuestionAnswer { UUID = 4, Text = "Todas las anteriores.", IsCorrect = false }
                ]
            },
            new()
            {
                Title = "Random Death Match (RDM)",
                Description = $"¿Qué significa Random Death Match (RDM)?{footer}",
                Answers =
                [
                    new QuestionAnswer { UUID = 1, Text = "Atacar a otro jugador sin participar en ningún rol previo.", IsCorrect = true },
                    new QuestionAnswer { UUID = 2, Text = "Actuar como si nada después de recibir un disparo.", IsCorrect = false },
                    new QuestionAnswer { UUID = 3, Text = "Amenazar a otro jugador durante un conflicto verbal.", IsCorrect = false },
                    new QuestionAnswer { UUID = 4, Text = "Atacar a otro jugador después de una persecución de coches.", IsCorrect = false }
                ]
            },
            new()
            {
                Title = "CarJack (CJ)",
                Description = $"¿Qué significa CarJack (CJ)?{footer}",
                Answers =
                [
                    new QuestionAnswer { UUID = 1, Text = "Rentar un vehículo.", IsCorrect = false },
                    new QuestionAnswer { UUID = 2, Text = "Darle las llaves de mi vehículo a un conocido.", IsCorrect = false },
                    new QuestionAnswer { UUID = 3, Text = "Robar un coche de la policía o a un individuo.", IsCorrect = true },
                    new QuestionAnswer { UUID = 4, Text = "Usar un coche robado para fines del rol.", IsCorrect = false }
                ]
            },
            new()
            {
                Title = "New Life Rule (NLR)",
                Description = $"¿Qué significa New Life Rule (NLR)?{footer}",
                Answers =
                [
                    new QuestionAnswer { UUID = 1, Text = "Recordar quién te mató y por qué.", IsCorrect = false },
                    new QuestionAnswer { UUID = 2, Text = "No recordar quién te mató y por qué.", IsCorrect = true },
                    new QuestionAnswer { UUID = 3, Text = "No interactuar con personas o eventos de tu vida anterior.", IsCorrect = true },
                    new QuestionAnswer { UUID = 4, Text = "Todas las anteriores.", IsCorrect = false }
                ]
            },
            new()
            {
                Title = "Revenge Killing (RK)",
                Description = $"¿Qué significa Revenge Killing (RK)?{footer}",
                Answers =
                [
                    new QuestionAnswer { UUID = 1, Text = "Matar a un jugador porque te robó.", IsCorrect = false },
                    new QuestionAnswer { UUID = 2, Text = "Matar a un jugador porque te insultó verbalmente.", IsCorrect = false },
                    new QuestionAnswer { UUID = 3, Text = "Matar a un jugador porque te mató en una vida anterior.", IsCorrect = true },
                    new QuestionAnswer { UUID = 4, Text = "Matar a un jugador porque te cae mal.", IsCorrect = false }
                ]
            },
            new()
            {
                Title = "Fail Roleplay (FRP)",
                Description = $"¿Qué significa Fail Roleplay (FRP)?{footer}",
                Answers =
                [
                    new QuestionAnswer { UUID = 1, Text = "Ignorar amenazas que ponen en riesgo tu vida.", IsCorrect = true },
                    new QuestionAnswer { UUID = 2, Text = "Actuar de manera poco realista en una situación seria.", IsCorrect = true },
                    new QuestionAnswer { UUID = 3, Text = "Usar un vehículo para evadir el rol obligatorio.", IsCorrect = true },
                    new QuestionAnswer { UUID = 4, Text = "Todas las anteriores.", IsCorrect = true }
                ]
            }
        ];
    }
}