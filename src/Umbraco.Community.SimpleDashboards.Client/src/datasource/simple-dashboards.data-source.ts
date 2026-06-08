import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {GetRenderByDashboardResponse, SimpleDashboards} from "../api";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {tryExecute} from "@umbraco-cms/backoffice/resources";

export interface ISimpleDashboardsDataSource {
    render(alias: string): Promise<UmbDataSourceResponse<GetRenderByDashboardResponse>>;
}

export class SimpleDashboardsDataSource implements ISimpleDashboardsDataSource {
    #host: UmbControllerHost;

    constructor(host: UmbControllerHost) {
        this.#host = host;
    }

    async render(alias: string): Promise<UmbDataSourceResponse<GetRenderByDashboardResponse>> {
        const options = {
            path: {
                dashboard: alias,
            },
        };
        return await tryExecute(this.#host, SimpleDashboards.getRenderByDashboard(options))
    }
}
