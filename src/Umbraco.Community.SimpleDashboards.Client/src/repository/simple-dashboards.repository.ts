import {UmbControllerBase} from "@umbraco-cms/backoffice/class-api";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse} from "../api";
import {SimpleDashboardsDataSource, ISimpleDashboardsDataSource} from "../datasource/simple-dashboards.data-source";

export class SimpleDashboardsRepository extends UmbControllerBase {
    #resource: ISimpleDashboardsDataSource;

    constructor(host: UmbControllerHost) {
        super(host);
        this.#resource = new SimpleDashboardsDataSource(host);
    }

    async render(alias: string): Promise<UmbDataSourceResponse<GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse>> {
        return await this.#resource.render(alias);
    }
}
