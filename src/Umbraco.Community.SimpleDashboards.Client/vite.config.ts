import {defineConfig} from "vite";

export default defineConfig({
	build: {
		lib: {
			entry: ["src/index.ts"],
			formats: ["es"],
		},
		outDir: "../Umbraco.Community.SimpleDashboards/wwwroot/App_Plugins/Umbraco.Community.SimpleDashboards/dist/",
		sourcemap: true,
		rollupOptions: {
			external: [/^@umbraco/],
		},
	},
});
