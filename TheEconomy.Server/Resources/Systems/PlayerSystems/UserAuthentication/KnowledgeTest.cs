using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services;

namespace TheEconomy.Server.Resources.Systems.PlayerSystems.UserAuthentication
{
    public class KnowledgeTest(IDialogService dialogService, RegisterAccount registerAccount, ServerInformation serverInformation, Colors color) : ISystem
    {
        private int ResponseRate { get; set; }
        private int RightAnswers { get; set; }

        private readonly Dictionary<int, dynamic> knowledgeQuestions = new()
        {
            [0] = new
            {
                title = "In Character (IC)",
                description = $"¿Qué significa In Character (IC)? — Para responder debes haber leído los conceptos básicos en {serverInformation.WebSite}",
                questions = new[]
                {
                    new { id = 1, question = "Hablar sobre el clima en la vida real.", isCorrect = false },
                    new { id = 2, question = "Hablar sobre un evento en el juego.", isCorrect = true },
                    new { id = 3, question = "Hablar sobre un evento en la vida real.", isCorrect = false },
                    new { id = 4, question = "Hablar sobre las noticias en la vida real.", isCorrect = false }
                }
            },
            [1] = new
            {
                title = "Out of Character (OOC)",
                description = $"¿Qué significa Out of Character (OOC)? — Para responder debes haber leído los conceptos básicos en {serverInformation.WebSite}",
                questions = new[]
                {
                    new { id = 1, question = "Interactuar verbalmente con otro jugador dentro del rol.", isCorrect = false },
                    new { id = 2, question = "Usar información de un foro para beneficio en el juego.", isCorrect = false },
                    new { id = 3, question = "Pedir ayuda en el canal de ayuda.", isCorrect = true },
                    new { id = 4, question = "Hablar sobre las noticias en la vida real.", isCorrect = true },
                }
            },
            [2] = new
            {
                title = "MetaGaming (MG)",
                description = $"¿Qué significa MetaGaming (MG)? — Para responder debes haber leído los conceptos básicos en {serverInformation.WebSite}",
                questions = new[]
                {
                    new { id = 1, question = "Usar información de un stream de Twitch para beneficio en el juego.", isCorrect = true },
                    new { id = 2, question = "Pegarle a otro jugador con un bate.", isCorrect = false },
                    new { id = 3, question = "Hablar sobre el clima de la vida real en el juego.", isCorrect = false },
                    new { id = 4, question = "Usar información fuera del juego para tomar decisiones dentro del juego.", isCorrect = true },
                }
            },
            [3] = new
            {
                title = "PowerGaming (PG)",
                description = $"¿Qué significa PowerGaming (PG)? — Para responder debes haber leído los conceptos básicos en {serverInformation.WebSite}",
                questions = new[]
                {
                    new { id = 1, question = "Forzar a otro jugador a hacer algo sin darle la oportunidad de responder.", isCorrect = false },
                    new { id = 2, question = "Ignorar las heridas de tu personaje después de un accidente.", isCorrect = false },
                    new { id = 3, question = "Hacer cosas que tu personaje no podría hacer en la vida real.", isCorrect = true },
                    new { id = 4, question = "Todas las anteriores.", isCorrect = false },
                }
            },
            [4] = new
            {
                title = "Random Death Match (RDM)",
                description = $"¿Qué significa Random Death Match (RDM)? — Para responder debes haber leído los conceptos básicos en {serverInformation.WebSite}",
                questions = new[]
                {
                    new { id = 1, question = "Atacar a otro jugador sin participar en ningún rol previo.", isCorrect = true },
                    new { id = 2, question = "Actuar como si nada después de recibir un disparo.", isCorrect = false },
                    new { id = 3, question = "Amenazar a otro jugador durante un conflicto verbal.", isCorrect = false },
                    new { id = 4, question = "Atacar a otro jugador después de una persecución de coches.", isCorrect = false },
                }
            },
            [5] = new
            {
                title = "CarJack (CJ)",
                description = $"¿Qué significa CarJack (CJ)? — Para responder debes haber leído los conceptos básicos en {serverInformation.WebSite}",
                questions = new[]
                {
                    new { id = 1, question = "Rentar un vehículo.", isCorrect = false },
                    new { id = 2, question = "Darle las llaves de mi vehículo a un conocido.", isCorrect = false },
                    new { id = 3, question = "Robar un coche de la policía o a un individuo.", isCorrect = true },
                    new { id = 4, question = "Usar un coche robado para fines del rol.", isCorrect = false },
                }
            },
            [6] = new
            {
                title = "New Life Rule (NLR)",
                description = $"¿Qué significa New Life Rule (NLR)? — Para responder debes haber leído los conceptos básicos en {serverInformation.WebSite}",
                questions = new[]
                {
                    new { id = 1, question = "Recordar quién te mató y por qué.", isCorrect = false },
                    new { id = 2, question = "No recordar quién te mató y por qué.", isCorrect = true },
                    new { id = 3, question = "No interactuar con personas o eventos de tu vida anterior.", isCorrect = true },
                    new { id = 4, question = "Todas las anteriores.", isCorrect = false }
                }
            },
            [7] = new
            {
                title = "Revenge Killing (RK)",
                description = $"¿Qué significa Revenge Killing (RK)? — Para responder debes haber leído los conceptos básicos en {serverInformation.WebSite}",
                questions = new[]
                {
                    new { id = 1, question = "Matar a un jugador porque te robó.", isCorrect = false },
                    new { id = 2, question = "Matar a un jugador porque te insultó verbalmente.", isCorrect = false },
                    new { id = 3, question = "Matar a un jugador porque te mató en una vida anterior.", isCorrect = true },
                    new { id = 4, question = "Matar a un jugador porque te cae mal.", isCorrect = false }
                }
            },
            [8] = new
            {
                title = "Fail Roleplay (FRP)",
                description = $"¿Qué significa Fail Roleplay (FRP)? — Para responder debes haber leído los conceptos básicos en {serverInformation.WebSite}",
                questions = new[]
                {
                    new { id = 1, question = "Ignorar amenazas que ponen en riesgo tu vida.", isCorrect = true },
                    new { id = 2, question = "Actuar de manera poco realista en una situación seria.", isCorrect = true },
                    new { id = 3, question = "Usar un vehículo para evadir el rol obligatorio.", isCorrect = true },
                    new { id = 4, question = "Todas las anteriores.", isCorrect = true }
                }
            }
        };

        public async Task StartRoleTest(Player player)
        {
            if (ResponseRate < knowledgeQuestions.Count)
            {
                TablistDialog RoleTestDialog = new($"{color.GetHexadecimal("primaryColor")}#{ResponseRate + 1}): {knowledgeQuestions[ResponseRate].title}", "Responder", "", ["ID", $"{knowledgeQuestions[ResponseRate].description}", "Respuesta"]);

                foreach (var row in knowledgeQuestions[ResponseRate].questions)
                    RoleTestDialog.Add([Convert.ToString(row.id), row.question, row.isCorrect ? $"{color.GetHexadecimal("primaryGreen")}Correcta" : $"{color.GetHexadecimal("primaryRed")}Incorrecta"]);

                TablistDialogResponse response = await dialogService.ShowAsync(player, RoleTestDialog);

                if (response.Response == DialogResponse.RightButtonOrCancel)
                {
                    player.PlaySound(1085);
                    _ = StartRoleTest(player);
                }
                else if (response.Response == DialogResponse.LeftButton)
                {
                    if (knowledgeQuestions[ResponseRate].questions[Convert.ToInt32(response.ItemIndex)].isCorrect)
                    {
                        player.PlaySound(1058);
                        RightAnswers++;
                    }
                    else
                        player.PlaySound(1085);

                    ResponseRate++;
                    _ = StartRoleTest(player);
                }
            }
            else
            {
                int finalCorrectAnswers = RightAnswers;
                ResponseRate = 0;
                RightAnswers = 0;

                if (finalCorrectAnswers == knowledgeQuestions.Count)
                    registerAccount.StartRegistration(player);
                else
                {
                    player.SendClientMessage($"{color.GetHexadecimal("primaryRed")}¡Ups! No superaste la prueba de conocimiento. Por favor vuelve a intentarlo.");
                    _ = StartRoleTest(player);
                }
            }
        }
    }
}