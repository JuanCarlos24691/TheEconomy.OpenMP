#include <open.mp>
#include <omp_network>

// Base information for adding a model to the game with the AddSimpleModel method
enum enumFileInformation {
    virtualWorld,
    baseId,
    newId,
    fileNameDff[34],
    fileNameTxd[34]
};

new fileInformation[][enumFileInformation] = {
    // Objects
    { -1, 19379, -1000, "images.dff", "images.txd" }
};

public OnGameModeInit() {
    for (new i = 0; i < sizeof(fileInformation); i++) {
        if (fileInformation[i][newId] >= -30000 && fileInformation[i][newId] <= -1000) {
            new FinalDirectoryDff[54], FinalDirectoryTxd[54];
            format(FinalDirectoryDff, 54, "objects/%d/%s", fileInformation[i][newId], fileInformation[i][fileNameDff]);
            format(FinalDirectoryTxd, 54, "objects/%d/%s", fileInformation[i][newId], fileInformation[i][fileNameTxd]);

            AddSimpleModel(fileInformation[i][virtualWorld], fileInformation[i][baseId], fileInformation[i][newId], FinalDirectoryDff, FinalDirectoryTxd);
        } else if (fileInformation[i][newId] >= 20000 && fileInformation[i][newId] <= 30000) {
            new FinalDirectoryDff[54], FinalDirectoryTxd[54];
            format(FinalDirectoryDff, 54, "skins/%d/%s", fileInformation[i][newId], fileInformation[i][fileNameDff]);
            format(FinalDirectoryTxd, 54, "skins/%d/%s", fileInformation[i][newId], fileInformation[i][fileNameTxd]);

            AddCharModel(fileInformation[i][baseId], fileInformation[i][newId], FinalDirectoryDff, FinalDirectoryTxd);
        }
    }
}

main() return 1;