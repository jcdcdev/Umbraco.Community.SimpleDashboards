import {css, html, LitElement, nothing} from 'lit';
import {customElement, state} from 'lit/decorators.js';
import {UmbElementMixin} from "@umbraco-cms/backoffice/element-api";
import {UUITextStyles} from "@umbraco-cms/backoffice/external/uui";
import {SIMPLE_DASHBOARS_CONTEXT_TOKEN} from "./resource.ts";
import {unsafeHTML} from 'lit/directives/unsafe-html.js';

@customElement('simple-dashboard')
export class SimpleDashboard extends UmbElementMixin(LitElement) {

    @state()
    content: string | undefined;
    @state()
    loading: boolean = true;

    constructor() {
        super();
        const url = window.location.pathname;
        const urlArray = url.split('/');
        const lastSegment = urlArray[urlArray.length - 1];

        this.consumeContext(SIMPLE_DASHBOARS_CONTEXT_TOKEN, async (context) => {
            const response = await context.render(lastSegment);
            this.loading = false;
            this.content = response.data?.body;
        });
    }

    render() {
        if (this.loading) {
            return nothing;
        }

        return html`
            <div class="uui-text">
                ${this.content ? unsafeHTML(this.content) : html`<p>Dashboard not found</p>`}
            </div>
        `
    }

    static styles = [
        UUITextStyles,
        css`
            :host {
                display: flex;
                flex-direction: column;
                gap: var(--uui-size-4);
                padding: var(--uui-size-layout-1);
            }

            pre {
                font-family: monospace;
                background-color: var(--uui-color-background);
                padding: var(--uui-size-layout-1)
            }
        `
    ]
}

declare global {
    interface HTMLElementTagNameMap {
        'simple-dashboard': SimpleDashboard;
    }
}
