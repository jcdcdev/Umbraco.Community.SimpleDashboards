// This file is auto-generated by @hey-api/openapi-ts

import type { CancelablePromise } from './core/CancelablePromise';
import { OpenAPI } from './core/OpenAPI';
import { request as __request } from './core/request';
import type { GetUmbracoSimpledashboardsApiV1RenderByDashboardData, GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse } from './types.gen';

/**
 * @param data The data for the request.
 * @param data.dashboard
 * @returns unknown OK
 * @throws ApiError
 */
export const getUmbracoSimpledashboardsApiV1RenderByDashboard = (data: GetUmbracoSimpledashboardsApiV1RenderByDashboardData): CancelablePromise<GetUmbracoSimpledashboardsApiV1RenderByDashboardResponse> => { return __request(OpenAPI, {
    method: 'GET',
    url: '/umbraco/simpledashboards/api/v1/render/{dashboard}',
    path: {
        dashboard: data.dashboard
    }
}); };