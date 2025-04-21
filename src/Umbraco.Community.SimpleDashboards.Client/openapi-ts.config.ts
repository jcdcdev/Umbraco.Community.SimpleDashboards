import { defineConfig, defaultPlugins } from '@hey-api/openapi-ts';

export default defineConfig({
    input: 'http://localhost:54813/umbraco/swagger/SimpleDashboards/swagger.json',
    plugins: [
        ...defaultPlugins,
        'legacy/fetch',
        '@hey-api/schemas',
        {
            dates: true,
            name: '@hey-api/transformers',
        },
        {
            enums: 'javascript',
            name: '@hey-api/typescript',
        },
        {
            name: '@hey-api/sdk',
            transformer: true,
        },
    ],
    output: {
        format: 'prettier',
        path: './src/api',
    }
});
