import {defineConfig, defaultPlugins} from '@hey-api/openapi-ts';

export default defineConfig({
    input: 'http://localhost:38116/umbraco/openapi/SimpleDashboards.json',
    plugins: [
        {
            name: '@hey-api/sdk',
            operations: {
                containerName: {
                    name: 'SimpleDashboards',
                    casing: 'preserve',
                },
                strategy: 'byTags'
            }
        },
        {
            name: '@hey-api/client-fetch',
            runtimeConfigPath: './src/hey-api.ts',
            exportFromIndex: true,
            throwOnError: true,
        },
        '@hey-api/schemas',
        {
            enums: 'javascript',
            name: '@hey-api/typescript',
        },
    ],
    output: {
        path: './src/api',
    }
});
