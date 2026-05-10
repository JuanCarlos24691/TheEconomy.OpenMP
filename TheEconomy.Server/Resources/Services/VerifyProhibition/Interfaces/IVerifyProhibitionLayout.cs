using SampSharp.Entities.SAMP;
using TheEconomy.Database.Entity.Account;
using TheEconomy.Database.Entity.Prohibitions;
using TheEconomy.Server.Resources.Services.VerifyProhibition.Components;

namespace TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;

public interface IVerifyProhibitionLayout
{
    public void Create(Player player, ProhibitionEntity prohibitionEntity, AccountEntity account);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);

    public VerifyProhibitionLayoutComponent GetVerifyProhibitionLayoutComponent(Player player);
}