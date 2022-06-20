import { App, Plugin } from "vue";
import { AxiosInstance } from "axios";
import { initMockApis } from "../../../mocks/mock-service";
import MockAdapter from "axios-mock-adapter";

const createMockAdapter = (api: AxiosInstance): MockAdapter => {
    var adapter = new MockAdapter(api, { onNoMatch: "throwException", delayResponse: 1500 });
    return adapter;
};

export const mockApiAdapter: Plugin = {
    install(app: App, api: AxiosInstance) {
        initMockApis();
        app.config.globalProperties.$mockApi = createMockAdapter(api);
    },
};
