import {UmbContextToken} from "@umbraco-cms/backoffice/context-api";
import {UmbControllerBase} from "@umbraco-cms/backoffice/class-api";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {GetRenderByDashboardResponse} from "../api";
import {SimpleDashboardsRepository} from "../repository/simple-dashboards.repository";

export const SIMPLE_DASHBOARDS_CONTEXT_TOKEN = new UmbContextToken<SimpleDashboardsContext>("SimpleDashboardsContext");

export class SimpleDashboardsContext extends UmbControllerBase {
    #repository: SimpleDashboardsRepository;

    constructor(host: UmbControllerHost) {
        super(host);
        this.provideContext(SIMPLE_DASHBOARDS_CONTEXT_TOKEN, this);
        this.#repository = new SimpleDashboardsRepository(this);
    }

    async render(alias: string): Promise<UmbDataSourceResponse<GetRenderByDashboardResponse>> {
        return await this.#repository.render(alias);
    }
}
