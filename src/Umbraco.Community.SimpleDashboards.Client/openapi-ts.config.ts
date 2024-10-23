import {defineConfig} from '@hey-api/openapi-ts';

export default defineConfig({
    input:
        'http://localhost:54813/umbraco/swagger/SimpleDashboards/swagger.json',
    output: {
        format: 'prettier',
        path: './src/api',
    },
    client: "legacy/fetch",
    types: {
        enums: 'typescript',
    },
});
