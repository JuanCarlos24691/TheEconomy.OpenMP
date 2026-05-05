using System.Collections.Generic;
using System.Threading.Tasks;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.KnowledgeTest.Components;
using TheEconomy.Server.Resources.KnowledgeTest.Data;
using TheEconomy.Server.Resources.KnowledgeTest.Interfaces;
using TheEconomy.Server.Resources.KnowledgeTest.Models;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;

namespace TheEconomy.Server.Resources.KnowledgeTest;

public class KnowledgeTest(IDialogService dialogService, IServerInformation serverInformation, IColors colors) : IKnowledgeTest
{
    private readonly List<RoleQuestion> questions = KnowledgeTestData.GetQuestions(serverInformation.WebSite);

    public async Task<bool> Start(Player player)
    {
        KnowledgeTestComponent KnowledgeTestComponent = player.GetComponent<KnowledgeTestComponent>() ?? player.AddComponent<KnowledgeTestComponent>();

        if (KnowledgeTestComponent.Index < questions.Count)
        {
            RoleQuestion currentQuestion = questions[KnowledgeTestComponent.Index];
            string title = $"{colors.GetHexadecimal("primarycolors")}#{KnowledgeTestComponent.Index + 1}): {currentQuestion.Title}";

            TablistDialog roleTestDialog = new(title, "Responder", "", ["ID", "Pregunta", "Respuesta"]);

            foreach (QuestionAnswer ans in currentQuestion.Answers)
            {
                roleTestDialog.Add([ans.UUID.ToString(), ans.Text, ans.IsCorrect ? $"{colors.GetHexadecimal("primaryGreen")}Correcta" : $"{colors.GetHexadecimal("primaryRed")}Incorrecta"]);
            }

            TablistDialogResponse response = await dialogService.ShowAsync(player, roleTestDialog);

            if (response.Response == DialogResponse.RightButtonOrCancel)
            {
                player.PlaySound(1085);
                return await Start(player); 
            }
            
            if (currentQuestion.Answers[response.ItemIndex].IsCorrect)
            {
                player.PlaySound(1058);
                KnowledgeTestComponent.Score++;
            }
            else
            {
                player.PlaySound(1085);
            }

            KnowledgeTestComponent.Index++;
            return await Start(player);
        }

        player.DestroyComponents<KnowledgeTestComponent>();

        return KnowledgeTestComponent.Score == questions.Count;
    }
}