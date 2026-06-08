import type {UmbEntryPointOnInit} from "@umbraco-cms/backoffice/extension-api";
import './components/simple-dashboard.ts';
import {SimpleDashboardsContext} from "./context/simple-dashboards.context.ts";
import {ManifestLocalizations} from "./lang/manifests.ts";

export const onInit: UmbEntryPointOnInit = (_host, extensionRegistry) => {
    extensionRegistry.registerMany(ManifestLocalizations);
    new SimpleDashboardsContext(_host);
};
