#include <open.mp>
#include <omp_network>

// Base information for adding a model to the game with the AddSimpleModel method
enum enumFileEntity {
    virtualWorld,
    baseId,
    newId,
    fileNameDff[34],
    fileNameTxd[34]
};

new fileEntity[][enumFileEntity] = {
    // Objects
    { -1, 19379, -1000, "images.dff", "images.txd" }
};

public OnGameModeInit() {
    for (new i = 0; i < sizeof(fileEntity); i++) {
        if (fileEntity[i][newId] >= -30000 && fileEntity[i][newId] <= -1000) {
            new FinalDirectoryDff[54], FinalDirectoryTxd[54];
            format(FinalDirectoryDff, 54, "objects/%d/%s", fileEntity[i][newId], fileEntity[i][fileNameDff]);
            format(FinalDirectoryTxd, 54, "objects/%d/%s", fileEntity[i][newId], fileEntity[i][fileNameTxd]);

            AddSimpleModel(fileEntity[i][virtualWorld], fileEntity[i][baseId], fileEntity[i][newId], FinalDirectoryDff, FinalDirectoryTxd);
        } else if (fileEntity[i][newId] >= 20000 && fileEntity[i][newId] <= 30000) {
            new FinalDirectoryDff[54], FinalDirectoryTxd[54];
            format(FinalDirectoryDff, 54, "skins/%d/%s", fileEntity[i][newId], fileEntity[i][fileNameDff]);
            format(FinalDirectoryTxd, 54, "skins/%d/%s", fileEntity[i][newId], fileEntity[i][fileNameTxd]);

            AddCharModel(fileEntity[i][baseId], fileEntity[i][newId], FinalDirectoryDff, FinalDirectoryTxd);
        }
    }
    return 1;
}

main() return 1;