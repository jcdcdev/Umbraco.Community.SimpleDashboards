import {ManifestLocalization} from "@umbraco-cms/backoffice/extension-registry";

export const ManifestLocalizations: Array<ManifestLocalization> = [
    {
        type: 'localization',
        alias: 'simple-dashboards.lang.enus',
        name: 'English (US)',
        weight: 0,
        meta: {
            culture: 'en-us'
        },
        js: () => import('./en-us')
    },
    {
        type: 'localization',
        alias: 'simple-dashboards.lang.engb',
        name: 'English (UK)',
        weight: 0,
        meta: {
            culture: 'en-gb'
        },
        js: () => import('./en-us')
    },
]
