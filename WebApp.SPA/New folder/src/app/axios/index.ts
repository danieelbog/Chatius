import axios from "axios";

//global axios api
const api = createInstance(location.origin + "/init-appData");
export { api };

function createInstance(baseURL: string) {
    var instance = axios.create({
        baseURL,
        withCredentials: true,
        headers: {
            "Content-Type": "application/json",
        },
    });

    const token = localStorage.getItem("user-token");
    if (token) instance.defaults.headers.common["Authorization"] = token;

    //global interceptor for unauthorized calls
    instance.interceptors.response.use(undefined, function (error: any) {
        return new Promise(function (resolve, reject) {
            const originalRequest = error.config;
            console.log("api error", originalRequest);
            if (error.response.status === 401 && !originalRequest._retry) {
                //add logic
            }
            reject(error);
        });
    });
    return instance;
}




