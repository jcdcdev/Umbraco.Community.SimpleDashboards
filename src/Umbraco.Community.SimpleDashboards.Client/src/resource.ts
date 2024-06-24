import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {getUmbracoSimpledashboardsApiV1RenderByDashboard, GetUmbracoSimpledashboardsApiV1RenderByDashboardData, GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse} from "./api";
import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {tryExecuteAndNotify} from "@umbraco-cms/backoffice/resources";
import {UmbControllerBase} from "@umbraco-cms/backoffice/class-api";
import {UmbContextToken} from "@umbraco-cms/backoffice/context-api";

export const SIMPLE_DASHBOARS_CONTEXT_TOKEN =
    new UmbContextToken<SimpleDashboardsContext>("SimpleDashboardsContext");

export class SimpleDashboardsContext extends UmbControllerBase {
    #repository: SimpleDashboardsRepository;

    constructor(host: UmbControllerHost) {
        super(host);
        this.provideContext(SIMPLE_DASHBOARS_CONTEXT_TOKEN, this);
        this.#repository = new SimpleDashboardsRepository(this);
    }

    async render(alias: string): Promise<UmbDataSourceResponse<GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse>> {
        return await this.#repository.render(alias);
    }
}

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

interface ISimpleDashboardsDataSource {
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
