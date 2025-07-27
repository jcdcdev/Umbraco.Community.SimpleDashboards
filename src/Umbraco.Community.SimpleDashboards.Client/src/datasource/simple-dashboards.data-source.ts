import { UmbDataSourceResponse } from "@umbraco-cms/backoffice/repository";
import { GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse, SimpleDashboards } from "../api";
import { UmbControllerHost } from "@umbraco-cms/backoffice/controller-api";
import { tryExecute } from "@umbraco-cms/backoffice/resources";

export interface ISimpleDashboardsDataSource {
    render(alias: string): Promise<UmbDataSourceResponse<GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse>>;
}

export class SimpleDashboardsDataSource implements ISimpleDashboardsDataSource {
    #host: UmbControllerHost;

    constructor(host: UmbControllerHost) {
        this.#host = host;
    }

    async render(alias: string): Promise<UmbDataSourceResponse<GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse>> {
        const options = {
            path: {
                dashboard: alias,
            },
        };
        return await tryExecute(this.#host, SimpleDashboards.getUmbracoSimpledashboardsApiV1RenderByDashboard(options))
    }
}
