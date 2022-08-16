import * as signalR from "@microsoft/signalr";
const connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:8090/hub").build()
export { connection };

export async function initSignalR() {
    await connection.start();
}