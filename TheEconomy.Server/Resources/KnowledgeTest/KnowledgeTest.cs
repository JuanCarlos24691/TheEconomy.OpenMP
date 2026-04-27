using System.Collections.Generic;
using System.Threading.Tasks;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.KnowledgeTest.Components;
using TheEconomy.Server.Resources.KnowledgeTest.Data;
using TheEconomy.Server.Resources.KnowledgeTest.Models;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;

namespace TheEconomy.Server.Resources.KnowledgeTest;

public class KnowledgeTest(IDialogService dialogService, IServerInformation serverInformation, IColors colors)
{
    private readonly List<RoleQuestion> questions = KnowledgeTestData.GetQuestions(serverInformation.WebSite);

    public async Task<bool> Start(Player player)
    {
        PlayerTestState playerTestState = player.GetComponent<PlayerTestState>() ?? player.AddComponent<PlayerTestState>();

        if (playerTestState.Index < questions.Count)
        {
            RoleQuestion currentQuestion = questions[playerTestState.Index];
            string title = $"{colors.GetHexadecimal("primarycolors")}#{playerTestState.Index + 1}): {currentQuestion.Title}";

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
                playerTestState.Score++;
            }
            else
            {
                player.PlaySound(1085);
            }

            playerTestState.Index++;
            return await Start(player);
        }

        player.DestroyComponents<PlayerTestState>();

        return playerTestState.Score == questions.Count;
    }
}