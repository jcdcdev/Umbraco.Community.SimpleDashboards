import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {getUmbracoSimpledashboardsApiV1RenderByDashboard, GetUmbracoSimpledashboardsApiV1RenderByDashboardData, GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse} from "../api";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {tryExecuteAndNotify} from "@umbraco-cms/backoffice/resources";

export interface ISimpleDashboardsDataSource {
    render(alias: string): Promise<UmbDataSourceResponse<GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse>>;
}

export class SimpleDashboardsDataSource implements ISimpleDashboardsDataSource {

    #host: UmbControllerHost;

    constructor(host: UmbControllerHost) {
        this.#host = host;
    }

    async render(alias: string): Promise<UmbDataSourceResponse<GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse>> {
        const data: GetUmbracoSimpledashboardsApiV1RenderByDashboardData = {
            dashboard: alias,
        };
        return await tryExecuteAndNotify(this.#host, getUmbracoSimpledashboardsApiV1RenderByDashboard(data))
    }
}
